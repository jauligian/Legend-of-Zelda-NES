using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSE3902.Interfaces;
using System.Security.Cryptography;

namespace CSE3902.Controllers
{
    public class GamePadMenuController : IGamePadController
    {
        private Dictionary<Buttons, ICommand> _buttonMappings = new();
        private IEnumerable<Buttons> _lastPressedButtons = new Buttons[0];

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

            IEnumerable<Buttons> justPressedButtons = pressedButtons.Except(_lastPressedButtons);

            foreach (Buttons button in justPressedButtons)
            {
                TryExecuteButton(button);
            }
            _lastPressedButtons = pressedButtons;
        }

        private void TryExecuteButton(Buttons button)
        {
            _buttonMappings.TryGetValue(button, out ICommand command);
            command?.Execute();
        }
    }
}
