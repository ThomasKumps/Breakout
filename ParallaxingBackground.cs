using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Breakout
{
    public class ParallaxingBackground
    {
        public Texture2D Texture;
        private Vector2[] positions;
        private int intSpeed, intWidth, intHeight;

        public void Initialize(ContentManager content,String texturePath, int screenWidth, int screenHeight, int speed) 
        {

            intWidth = screenWidth;
            intHeight = screenHeight;
            intSpeed = speed;

            Texture = content.Load<Texture2D>(texturePath);

            positions = new Vector2[screenWidth / Texture.Width + 2];

            for (int i = 0; i < positions.Length; i++) 
            {
                positions[i] = new Vector2(i * Texture.Width, 0);
            }

        }

        public void Update(GameTime gameTime, int speed) 
        {
            intSpeed = speed;

            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].X += intSpeed;
                // if the speed has the background moving to the left

                if (intSpeed <= 0)
                {
                    //check if texture is out of view, then put texture at end of screen
                    if (positions[i].X <= -Texture.Width)
                    {
                        positions[i].X = Texture.Width * (positions.Length - 1);
                    }
                }
                else
                {
                    if (positions[i].X >= Texture.Width * (positions.Length - 1))
                    {
                        positions[i].X = -Texture.Width;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            for (int i = 0; i < positions.Length; i++)
            {
                Rectangle rectBg = new Rectangle((int)positions[i].X, (int)positions[i].Y, intWidth, intHeight);
                spriteBatch.Draw(Texture, rectBg, Color.White);
            }
        }
    }
}
