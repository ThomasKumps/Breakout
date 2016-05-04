using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Breakout
{
    public class Background
    {
        public Texture2D Texture;
        public int Width, Height;
        public Background(int width, int height) 
        {
            Width = width;
            Height = height;
        }
    }
}
