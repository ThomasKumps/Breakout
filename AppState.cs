using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Breakout
{
    public class AppState
    {
        public MenuState menuState;
        public GameState gameState;
        public AppState() 
        {
            menuState = new MenuState();
            gameState = new GameState();
        }
    }
}
