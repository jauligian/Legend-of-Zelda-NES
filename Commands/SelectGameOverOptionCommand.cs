using CSE3902.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3902.Commands
{
    public class SelectGameOverOptionCommand : ICommand
    {
        private Game1 _game;
        public SelectGameOverOptionCommand(Game1 game)
        {
            _game = game;
        }
        public void Execute()
        {
            if (_game.GameLose)
            {
                if (_game.GameOverScreen.Pos == 0)
                {
                    _game.ContinueGame = true;
                    _game.Reset();
                }
                else if (_game.GameOverScreen.Pos == 2)
                {
                    _game.Reset();
                }
            }
        }
    }
}
