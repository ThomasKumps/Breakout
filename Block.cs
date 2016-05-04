using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Breakout
{
    public class Block
    {
        public Texture2D Texture;
        public Rectangle Instance;

        public void Initialize(ContentManager content, String texturePath, Vector2 position) 
        {
            this.Texture = content.Load<Texture2D>(texturePath);
            this.Instance = new Rectangle((int)position.X,(int)position.Y, Texture.Width,Texture.Height);
        }

        public void Update(GameTime gameTime, Ball bal) 
        {
        
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(Texture, Instance, Color.White);
        }

        public bool CheckCollision(Ball bal) 
        {
            if (bal.Instance.Intersects(this.Instance))
                return true;
            else
                return false;          
        }
    }
}
