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
    public class KeyboardMenuController : IKeyboardController
    {
        private Dictionary<Keys, ICommand> _keyboardMappings = new();
        private Keys[] _lastPressedKeys = new Keys[0];

        public void RegisterCommand(Keys key, ICommand command)
        {
            _keyboardMappings[key] = command;
        }

        public void Update()
        {
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

            IEnumerable<Keys> justPressedKeys = pressedKeys.Except(_lastPressedKeys);

            foreach (Keys key in justPressedKeys)
            {
                TryExecuteKey(key);
            }
            _lastPressedKeys = pressedKeys;
        }

        private void TryExecuteKey(Keys key)
        {
            _keyboardMappings.TryGetValue(key, out ICommand command);
            command?.Execute();
        }
    }
}
