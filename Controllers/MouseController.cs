using CSE3902.Interfaces;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CSE3902.Controllers
{
    public class MouseController
    {
        private bool _leftHasBeenPressed = false;
        private bool _rightHasBeenPressed = false;
        private readonly List<ICommand> _leftControllerMappings = new();
        private readonly List<ICommand> _rightControllerMappings = new();

        public void Update()
        {
            MouseState ms = Mouse.GetState();
            if (ms.X >= 0 && ms.X < 800
                             && ms.Y >= 0 && ms.Y < 800) //Magic Numbers that can be fixed that make it so you have to click on the application in order for it to cycle.
            {

                if (ms.LeftButton == ButtonState.Pressed)
                {
                    _leftHasBeenPressed = true;
                }

                if (ms.LeftButton == ButtonState.Released && _leftHasBeenPressed)
                {
                    _leftHasBeenPressed = false;
                    foreach (ICommand command in _leftControllerMappings)
                    {
                        command.Execute();
                    }
                }

                if (ms.RightButton == ButtonState.Pressed)
                {
                    _rightHasBeenPressed = true;
                }

                if (ms.RightButton == ButtonState.Released && _rightHasBeenPressed)
                {
                    _rightHasBeenPressed = false;
                    foreach (ICommand command in _rightControllerMappings)
                    {
                        command.Execute();
                    }
                }
            }
        }
        public void RegisterLeftCommand(ICommand command)
        {
            _leftControllerMappings.Add(command);
        }

        public void RegisterRightCommand(ICommand command)
        {
            _rightControllerMappings.Add(command);
        }

    }
}