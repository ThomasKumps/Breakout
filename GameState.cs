using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout
{
    public class GameState
    {
        public bool GameOver, GameInProgress, GamePaused, SceneGame;
        public int Level;

        public GameState() 
        {
            this.GameOver = true;
            this.GameInProgress = false;
            this.GamePaused = true;
            this.SceneGame = false;
        }
    }
}
