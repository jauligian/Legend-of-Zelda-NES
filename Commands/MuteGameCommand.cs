using CSE3902.Interfaces;
using CSE3902.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3902.Commands
{
    public class MuteGameCommand : ICommand
    {
        private Game1 _game;
        public MuteGameCommand(Game1 game)
        {
            _game = game;
        }
        public void Execute()
        {
            SoundFactory.Instance.ChangeMuted();
        }
    }
}
