using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class GameScreen
    {
        Background background;
        ParallaxingBackground bgLayer1, bgLayer2;
        Ball bal;
        Paddle paddle;
        BlockManager blockManager;
        GameManager gameManager;
        UIManager UImanager;
        ContentManager contentSide;
        public GameScreen(int width, int height) 
        {
            background = new Background(width,height);
            bgLayer1 = new ParallaxingBackground();
            bgLayer2 = new ParallaxingBackground();

            blockManager = new BlockManager();
            gameManager = new GameManager();
            UImanager = new UIManager(width,height);

            bal = new Ball(background.Width,background.Height);
            paddle = new Paddle(background.Width, background.Height);
        }

        public void Update(GameTime gameTime, AppState appState) 
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
                appState.gameState.GamePaused = true;

            if (!gameManager.GameOver) 
            {
                if (gameManager.CurrBlockCount == 0)
                    LoadNewLevel(gameManager.Level.Value);

                paddle.Update(gameTime);
                bal.Update(paddle, gameTime, blockManager.Blocks, gameManager);

                bgLayer1.Update(gameTime, paddle.CurrentSpeed / 4);
                bgLayer2.Update(gameTime, (int)bal.Speed.X / 2);

                blockManager.Update(gameTime, bal);
            }
            else 
            {
                blockManager.UnloadContent();
            }           
 
            gameManager.Update(gameTime, blockManager.Blocks.Count);
            UImanager.Update(gameTime, gameManager);
        }

        public void LoadContent(ContentManager Content, AppState appState)
        {
            this.contentSide = Content;

            bal = new Ball(background.Width, background.Height);
            paddle = new Paddle(background.Width, background.Height);

            bal.Texture = Content.Load<Texture2D>("Ball");
            paddle.Texture = Content.Load<Texture2D>("barBreakout");

            LoadNewLevel(appState);
        }
        public void Draw(SpriteBatch spritebatch) 
        {
            spritebatch.Draw(background.Texture, new Rectangle(0, 0, background.Texture.Width, background.Texture.Height), Color.White);
            bgLayer1.Draw(spritebatch);
            bgLayer2.Draw(spritebatch);

            if (!gameManager.GameOver) 
            {
                blockManager.Draw(spritebatch);

                spritebatch.Draw(bal.Texture, new Rectangle((int)bal.Instance.X, (int)bal.Instance.Y, bal.Texture.Width, bal.Texture.Height), Color.White);
                spritebatch.Draw(paddle.Texture, new Rectangle((int)paddle.Instance.X, (int)paddle.Instance.Y, paddle.Instance.Width, paddle.Instance.Height), Color.White);
            }

            UImanager.Draw(spritebatch);
        }

        private void LoadNewLevel(AppState appState) 
        {
            gameManager.Level.Value = (byte)appState.gameState.Level;
            gameManager.Level.NextLevel();
            gameManager.SceneGame = appState.gameState.SceneGame;

            background.Texture = contentSide.Load<Texture2D>("Background/Level" + gameManager.Level.Value + "Texture");
            bgLayer1.Initialize(contentSide, "ParallaxBackground/Level" + gameManager.Level.Value + "TextureBgLayer1", background.Width, background.Height, 0);
            bgLayer2.Initialize(contentSide, "ParallaxBackground/Level" + gameManager.Level.Value + "TextureBgLayer2", background.Width, background.Height, 0);
            blockManager.Initialize(contentSide, background.Width, background.Height, gameManager.Level);
            gameManager.Initialize(blockManager.Blocks.Count, contentSide);
            UImanager.Initialize(contentSide);
        }

        private void LoadNewLevel(int level)
        {
            gameManager.Level.Value = (byte)level;
            gameManager.Level.NextLevel();

            background.Texture = contentSide.Load<Texture2D>("Background/Level" + gameManager.Level.Value + "Texture");
            bgLayer1.Initialize(contentSide, "ParallaxBackground/Level" + gameManager.Level.Value + "TextureBgLayer1", background.Width, background.Height, 0);
            bgLayer2.Initialize(contentSide, "ParallaxBackground/Level" + gameManager.Level.Value + "TextureBgLayer2", background.Width, background.Height, 0);
            blockManager.Initialize(contentSide, background.Width, background.Height, gameManager.Level);
            gameManager.Initialize(blockManager.Blocks.Count, contentSide);
            UImanager.Initialize(contentSide);
        }
    }
}
