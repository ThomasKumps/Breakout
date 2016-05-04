using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Breakout
{
    public class UIManager
    {
        private SpriteFont font1, font2;
        private Level level;
        private Score score;
        private bool gameOver;
        private int _bgWidth, _bgHeight;
        private Texture2D ballTexture;

        public UIManager(int bgWidth, int bgHeight) 
        {
            this._bgHeight = bgHeight;
            this._bgWidth = bgWidth;
        }

        public void Initialize(ContentManager content) 
        {
            font1 = content.Load<SpriteFont>("Buxton Sketch");
            font2 = content.Load<SpriteFont>("Buxton SketchSmaller");
            ballTexture = content.Load<Texture2D>("Ball");
        }

        public void Update(GameTime gameTime, GameManager gameManager) 
        {
            this.level = gameManager.Level;
            this.score = gameManager.Score;
            this.gameOver = gameManager.GameOver;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            if (!gameOver)
            {
                spriteBatch.DrawString(font1, "Level " + level.Value
                    , new Vector2(800, 50), Color.Red);
                spriteBatch.DrawString(font2, "Balls: " + System.Environment.NewLine +
                   "Score: " + score.Value, new Vector2(800, 130), Color.Blue);
                for (int i = 0; i < level.Balls; i++) 
                {
                    spriteBatch.Draw(ballTexture, new Rectangle(900 + i*30, 140, ballTexture.Width, ballTexture.Height), Color.White);
                }
                    

                if (score.ScoreTimer.Count > 0) 
                {
                    foreach (float timer in score.ScoreTimer)
                        spriteBatch.DrawString(font2, "10 points!", new Vector2(800, 200 + score.ScoreTimer.BinarySearch(timer) * 10 + 120 * timer), Color.Yellow);
                }
            }
            else
            {
                spriteBatch.DrawString(font1, "Game Over", new Vector2(_bgWidth/2-100, _bgHeight/2-100), Color.Red);
            }
            if (level.BallLost)
                spriteBatch.DrawString(font1, "Ball lost", new Vector2(_bgWidth * level.BallTimer / 4, _bgHeight*level.BallTimer / 3), Color.Green);
        }
    }
}
