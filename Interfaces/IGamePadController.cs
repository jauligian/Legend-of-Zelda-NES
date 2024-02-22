using CSE3902.Shared;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3902.Interfaces
{
    public interface IGamePadController
    {
        public void RegisterCommand(Buttons button, ICommand command);
        public void Update();
    }
}
