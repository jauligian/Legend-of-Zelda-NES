using CSE3902.Interfaces;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSE3902.Controllers;

public class KeyboardController : IKeyboardController
{
    private readonly Dictionary<Keys, ICommand> _keyboardMappings = new();
    private Keys[] _lastPressedKeys = Array.Empty<Keys>();
    private int _comboCounterA = 0;
    private int _comboCounterB = 0;
    private int _comboCounterC = 0;
    private int _comboTimer = 0;

    private readonly Keys[] _movementKeys =
        { Keys.W, Keys.A, Keys.S, Keys.D };
    private readonly Keys[] _naviKeys =
            { Keys.Up, Keys.Down, Keys.Left, Keys.Right };
    private readonly Keys[] _comboSequenceA =
        { Keys.A, Keys.S, Keys.D, Keys.Space };
    private readonly Keys[] _comboSequenceB =
        { Keys.W, Keys.W, Keys.S, Keys.S, Keys.A, Keys.D, Keys.A, Keys.D, Keys.Space };
    private readonly Keys[] _comboSequenceC =
        { Keys.W, Keys.D, Keys.S, Keys.A };

    private Keys _lastMovementKey = Keys.W; //Arbitrary
    private Keys _lastNaviKey = Keys.Up; //Arbitrary
    private bool _firstCall = true;
    public void RegisterCommand(Keys key, ICommand command)
    {
        _keyboardMappings[key] = command;
    }

    public void Update()
    {
        if (_firstCall)
        {
            Keys[] preventedStartingKeys = { Keys.R };
            _lastPressedKeys = preventedStartingKeys;
            _firstCall = false;
        }
        Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
        //Methods discovered thanks to:
        //https://www.tutorialspoint.com/Intersection-of-two-arrays-in-Chash
        //https://stackoverflow.com/questions/5058609/how-to-perform-set-subtraction-on-arrays-in-c
        IEnumerable<Keys> heldMovementKeys = pressedKeys.Intersect(_lastPressedKeys).Intersect(_movementKeys);
        IEnumerable<Keys> heldNaviKeys = pressedKeys.Intersect(_lastPressedKeys).Intersect(_naviKeys);
        IEnumerable<Keys> justPressedKeys = pressedKeys.Except(_lastPressedKeys);
        bool noMovementKeysExecutedThisUpdate = true;
        bool noNaviKeysExecutedThisUpdate = true;

        CheckCombos(pressedKeys);
        foreach (Keys key in justPressedKeys)
        {
            if (!IsMovementKey(key) && !IsNaviKey(key)) TryExecuteKey(key);
            else if (noMovementKeysExecutedThisUpdate && !IsNaviKey(key))
            {
                TryExecuteKey(key);
                _lastMovementKey = key;
                noMovementKeysExecutedThisUpdate = false;
            }
            else if (noNaviKeysExecutedThisUpdate && !IsMovementKey(key))
            {
                TryExecuteKey(key);
                _lastNaviKey = key;
                noNaviKeysExecutedThisUpdate = false;
            }
        }

        if (noMovementKeysExecutedThisUpdate)
        {
            if (pressedKeys.Contains(_lastMovementKey))
            {
                TryExecuteKey(_lastMovementKey);
            }
            else
                foreach (Keys key in heldMovementKeys)
                {
                    TryExecuteKey(key);
                    _lastMovementKey = key;
                    break;
                }
        }

        if (noNaviKeysExecutedThisUpdate)
        {
            if (pressedKeys.Contains(_lastNaviKey))
            {
                TryExecuteKey(_lastNaviKey);
            }
            else
                foreach (Keys key in heldNaviKeys)
                {
                    TryExecuteKey(key);
                    _lastNaviKey = key;
                    break;
                }
        }

        _lastPressedKeys = pressedKeys;
    }

    private bool IsMovementKey(Keys key)
    {
        return _movementKeys.Contains(key);
    }

    private bool IsNaviKey(Keys key)
    {
        return _naviKeys.Contains(key);
    }

    private void TryExecuteKey(Keys key)
    {
        _keyboardMappings.TryGetValue(key, out ICommand command);
        command?.Execute();
    }

    private void CheckCombos(Keys[] keys)
    {
        if (_comboTimer > 0) _comboTimer--;
        if (keys.Contains(_comboSequenceA[_comboCounterA]) && keys.Length == 1)
        {
            _comboCounterA++;
            _comboTimer = 20;
            if(_comboCounterA == _comboSequenceA.Length)
            {
                TryExecuteKey(Keys.Kana);
                _comboCounterA = 0;
            }
        }
        else if (_comboTimer == 0)
        {
            _comboCounterA = 0;
        }
        
        if (keys.Contains(_comboSequenceB[_comboCounterB]) && keys.Length == 1)
        {
            _comboCounterB++;
            _comboTimer = 20;
            if (_comboCounterB == _comboSequenceB.Length)
            {
                TryExecuteKey(Keys.Kanji);
                _comboCounterB = 0;
            }
        }
        else if (_comboTimer == 0)
        {
            _comboCounterB = 0;
        }

        if (keys.Contains(_comboSequenceC[_comboCounterC]) && keys.Length == 1)
        {
            _comboCounterC++;
            _comboTimer = 20;
            if (_comboCounterC == _comboSequenceC.Length)
            {
                TryExecuteKey(Keys.ChatPadGreen);
                _comboCounterC = 0;
            }
        }
        else if (_comboTimer == 0)
        {
            _comboCounterC = 0;
        }
    }
}