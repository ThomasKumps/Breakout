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
    public class GameManager
    {
        public Level Level;
        public Score Score;
        public int CurrBlockCount;
        public float KeyHitTimer;
        public bool GameOver;
        private SpriteFont font1, font2;
        public bool SceneGame = false;

        public GameManager() 
        {
            Level = new Level();
            Score = new Score();
        }
        public void Initialize(int newblockCount, ContentManager content) 
        {
            this.CurrBlockCount = newblockCount;
            this.GameOver = false;
            this.KeyHitTimer = 0f;
            font1 = content.Load<SpriteFont>("Buxton Sketch");
            font2 = content.Load<SpriteFont>("Buxton SketchSmaller");           
        }
        public void Update(GameTime gameTime, int newBlockCount) 
        {
            KeyHitTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            

            Level.Update(gameTime);
            Score.Update(gameTime, newBlockCount, CurrBlockCount);
            //update global variable 
            CurrBlockCount = newBlockCount;

            if (Level.Balls <= 0)
                GameOver = true;


            if(GameOver && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                Level.Initialize(SceneGame);
                Score.Initialize();
                GameOver = false;
                KeyHitTimer = 0;
            }
        }

        
    }
}
