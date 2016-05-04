using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Breakout
{
    public class ScreenManager
    {
        GameScreen gameScreen;
        MenuScreen menuScreen;
        ContentManager sideContent;
        AppState appState;

        public ScreenManager(int width, int height) 
        {
            gameScreen = new GameScreen(width,height);
            menuScreen = new MenuScreen(width, height);
            appState = new AppState();
        }

        public void Update(GameTime gameTime) 
        {
            if(appState.gameState.GamePaused)
                menuScreen.Update(gameTime, appState);
            if (!appState.gameState.GamePaused)
                gameScreen.Update(gameTime, appState);

            if (appState.menuState.NewGame) 
            {
                gameScreen.LoadContent(sideContent, appState);
                appState.menuState.NewGame = false;
            }
        }

        public void LoadContent(ContentManager Content) 
        {
            this.sideContent = Content;

            gameScreen.LoadContent(Content, appState);

            menuScreen.LoadContent(Content,"Load/Menu/MainMenu.xml");
        }

        public void Draw(SpriteBatch spritebatch) 
        {
            if(appState.gameState.GamePaused)
                menuScreen.Draw(spritebatch);
            else
                gameScreen.Draw(spritebatch);

        }
    }
}
