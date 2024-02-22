using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSE3902.Controllers;

public class GamePadController : IGamePadController
{
    private readonly Dictionary<Buttons, ICommand> _buttonMappings = new();
    private IEnumerable<Buttons> _lastPressedButtons =  Array.Empty<Buttons>();
    private int _comboCounterA = 0;
    private int _comboCounterB = 0;
    private int _comboCounterC = 0;
    private int _comboTimer = 0;

    private readonly Buttons[] _movementButtons =
        { Buttons.LeftThumbstickUp, Buttons.LeftThumbstickRight, Buttons.LeftThumbstickLeft, Buttons.LeftThumbstickDown };
    private readonly Buttons[] _naviButtons =
            { Buttons.RightThumbstickUp, Buttons.RightThumbstickRight, Buttons.RightThumbstickDown, Buttons.RightThumbstickLeft };
    private readonly Buttons[] _comboSequenceA =
        { Buttons.LeftThumbstickLeft, Buttons.LeftThumbstickDown, Buttons.LeftThumbstickRight, Buttons.Y };
    private readonly Buttons[] _comboSequenceB =
        { Buttons.LeftThumbstickUp, Buttons.LeftThumbstickUp, Buttons.LeftThumbstickDown, Buttons.LeftThumbstickDown, Buttons.LeftThumbstickLeft, 
            Buttons.LeftThumbstickRight, Buttons.LeftThumbstickLeft, Buttons.LeftThumbstickRight, Buttons.Y };
    private readonly Buttons[] _comboSequenceC =
        { Buttons.LeftThumbstickUp, Buttons.LeftThumbstickRight, Buttons.LeftThumbstickDown, Buttons.LeftThumbstickLeft, Buttons.Y };

    private Buttons _lastMovementButton = Buttons.LeftThumbstickUp; //Arbitrary
    private Buttons _lastNaviButton = Buttons.RightThumbstickUp; //Arbitrary
    public void RegisterCommand(Buttons button, ICommand command)
    {
        _buttonMappings[button] = command;
    }

    public void Update()
    {
        GamePadState gps = GamePad.GetState(0);
        IEnumerable<Buttons> pressedButtons = Array.Empty<Buttons>();

        foreach (Buttons button in _buttonMappings.Keys)
        {
            if (gps.IsButtonDown(button))
            {
                pressedButtons = pressedButtons.Append(button);
            }
        }

        if (gps.IsButtonDown(Buttons.Y)) pressedButtons = pressedButtons.Append(Buttons.Y);

        CheckCombos(pressedButtons);
        //Methods discovered thanks to:
        //https://www.tutorialspoint.com/Intersection-of-two-arrays-in-Chash
        //https://stackoverflow.com/questions/5058609/how-to-perform-set-subtraction-on-arrays-in-c
        IEnumerable<Buttons> heldMovementButtons = pressedButtons.Intersect(_lastPressedButtons).Intersect(_movementButtons);
        IEnumerable<Buttons> heldNaviButtons = pressedButtons.Intersect(_lastPressedButtons).Intersect(_naviButtons);
        IEnumerable<Buttons> justPressedButtons = pressedButtons.Except(_lastPressedButtons);
        bool noMovementButtonsExecutedThisUpdate = true;
        bool noNaviButtonsExecutedThisUpdate = true;

        //CheckCombos(pressedButtons);
        foreach (Buttons button in justPressedButtons)
        {
            if (!IsMovementButton(button) && !IsNaviButton(button)) TryExecuteButton(button);
            else if (noMovementButtonsExecutedThisUpdate && !IsNaviButton(button))
            {
                TryExecuteButton(button);
                _lastMovementButton = button;
                noMovementButtonsExecutedThisUpdate = false;
            }
            else if (noNaviButtonsExecutedThisUpdate && !IsMovementButton(button))
            {
                TryExecuteButton(button);
                _lastNaviButton = button;
                noNaviButtonsExecutedThisUpdate = false;
            }
        }

        if (noMovementButtonsExecutedThisUpdate)
        {
            if (pressedButtons.Contains(_lastMovementButton))
            {
                TryExecuteButton(_lastMovementButton);
            }
            else
                foreach (Buttons button in heldMovementButtons)
                {
                    TryExecuteButton(button);
                    _lastMovementButton = button;
                    break;
                }
        }

        if (noNaviButtonsExecutedThisUpdate)
        {
            if (pressedButtons.Contains(_lastNaviButton))
            {
                TryExecuteButton(_lastNaviButton);
            }
            else
                foreach (Buttons button in heldNaviButtons)
                {
                    TryExecuteButton(button);
                    _lastNaviButton = button;
                    break;
                }
        }

        _lastPressedButtons = pressedButtons;
    }

    private bool IsMovementButton(Buttons button)
    {
        return _movementButtons.Contains(button);
    }

    private bool IsNaviButton(Buttons button)
    {
        return _naviButtons.Contains(button);
    }
    private void TryExecuteButton(Buttons button)
    {
        _buttonMappings.TryGetValue(button, out ICommand command);
        command?.Execute();
    }

    private void CheckCombos(IEnumerable<Buttons> buttons)
    {
        if (_comboTimer > 0) _comboTimer--;
        if (buttons.Contains(_comboSequenceA[_comboCounterA]))
        {
            _comboCounterA++;
            _comboTimer = 20;
            if(_comboCounterA == _comboSequenceA.Length)
            {
                TryExecuteButton(Buttons.LeftStick);
                _comboCounterA = 0;
            }
        }
        else if (_comboTimer == 0)
        {
            _comboCounterA = 0;
        }
        
        if (buttons.Contains(_comboSequenceB[_comboCounterB]))
        {
            _comboCounterB++;
            _comboTimer = 40;
            if (_comboCounterB == _comboSequenceB.Length)
            {
                TryExecuteButton(Buttons.BigButton);
                _comboCounterB = 0;
            }
        }
        else if (_comboTimer == 0)
        {
            _comboCounterB = 0;
        }

        if (buttons.Contains(_comboSequenceC[_comboCounterC]))
        {
            _comboCounterC++;
            _comboTimer = 20;
            if (_comboCounterC == _comboSequenceC.Length)
            {
                TryExecuteButton(Buttons.RightStick);
                _comboCounterC = 0;
            }
        }
        else if (_comboTimer == 0)
        {
            _comboCounterC = 0;
        }
    }
}