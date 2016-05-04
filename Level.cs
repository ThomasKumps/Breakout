using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Breakout
{
    public class Level
    {
        public byte Value;
        public bool BallLost = false;
        public float BallTimer;
        
        private byte _balls;
        
        public byte Balls 
        {
            get { return _balls; }
            set
            {
                if (value < _balls)
                    BallLose();

                _balls = value;
            }
        }

        public Level() 
        {
            Initialize();
        }

        public void Initialize() 
        {
            this.Value = 0;
            this.Balls = 3;
        }

        public void Initialize(bool sceneGame) 
        {
            if (sceneGame)
                this.Value--;
            else
                this.Value = 0;

            this.Balls = 3;
        }

        public void Update(GameTime gameTime) 
        {
            if (BallLost) 
            {
                BallTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (BallTimer >= 2) 
                {
                    BallLost = false;
                    BallTimer = 0;
                }
            }              
        }

        public void NextLevel() 
        {
            this.Value++;
            this.Balls = 3;
        }

        private void BallLose() 
        {
            BallLost = true;
            BallTimer = 0;
        }
    }
}
