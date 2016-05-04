using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Breakout
{
    public class Score
    {
        public int Value;
        public List<float> ScoreTimer;

        public Score() 
        {
            this.Initialize();
        }

        public void Initialize() 
        {
            Value = 0;
            ScoreTimer = new List<float>();
        }

        public void Update(GameTime gameTime, int currentBlockCount, int prevBlockCount) 
        {
            for (int i = 0; i < ScoreTimer.Count; i++) 
            {
                ScoreTimer[i] += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (ScoreTimer[i] > 2)
                    ScoreTimer.RemoveAt(i);
            }

            CalculateDifference(currentBlockCount, prevBlockCount);
        }

        public void Increase() 
        {
            Value+=10;
            ScoreTimer.Add(0f);
        }

        private void CalculateDifference(int currentBlockCount, int prevBlockCount)
        {
            int difference;

            difference = prevBlockCount - currentBlockCount;

            for (int i = 0; i < difference; i++)
            {
                this.Increase();
            }
        }
    }
}
