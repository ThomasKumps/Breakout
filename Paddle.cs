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
    public enum Direction 
    {
        Left = 1,
        Right = 2
    }
    public class Paddle
    {
        public Vector2 Speed, Bounds;
        public Rectangle Instance;
        public int CurrentSpeed = 0;

        private bool _IsMoving;
        public bool IsMoving
        {
            get { return _IsMoving; }
            set 
            {
                if (!value)
                    CurrentSpeed = 0;

                _IsMoving = value;
            }
        }

        private Rectangle _rectangleLeftHalf, _rectangleRightHalf;
        public Rectangle InstanceLeftHalf 
        {
            get 
            {
                _rectangleLeftHalf.Width = Instance.Width / 2;
                _rectangleLeftHalf.Height = Instance.Height;
                _rectangleLeftHalf.X = Instance.X;
                _rectangleLeftHalf.Y = Instance.Y;

                return _rectangleLeftHalf; 
            }
            set 
            {
                _rectangleLeftHalf = value;
            }
        }

        public Rectangle InstanceRightHalf 
        {
            get 
            {
                _rectangleRightHalf.Width = Instance.Width / 2;
                _rectangleRightHalf.Height = Instance.Height;
                _rectangleRightHalf.X = Instance.X + _rectangleRightHalf.Width;
                _rectangleRightHalf.Y = Instance.Y;

                return _rectangleRightHalf; 
            }
            set
            {
                _rectangleRightHalf = value;
            }
        }

        private Texture2D _texture;
        public Texture2D Texture
        {
            get
            {
                return _texture;
            }
            set
            {
                Bounds.X -= value.Width;
                Bounds.Y -= value.Height;

                Instance.Width = value.Width;
                Instance.Height = value.Height;

                _texture = value;
            }

        }

        public Paddle(int boundsX, int boundsY) 
        {
            Bounds.X = boundsX;
            Bounds.Y = boundsY;

            Instance.X = boundsX / 8;
            Instance.Y = boundsY - boundsY / 8;
            Speed = new Vector2(10, 0);
        }
        private void Move(Direction direction)
        {
            this.IsMoving = true;

            if (direction == Direction.Left && Instance.X >= 0)
            {
                Instance.X -= (int)Speed.X;
                CurrentSpeed = -(int)Speed.X;
            }
            else if (direction == Direction.Right && Instance.X <= Bounds.X) 
            {
                Instance.X += (int)Speed.X;
                CurrentSpeed = (int)Speed.X;
            }
                
        }

        public void Update(GameTime gameTime) 
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) 
            {
                Move(Direction.Left);
            }  
            else if (Keyboard.GetState().IsKeyDown(Keys.Right)) 
            {
                Move(Direction.Right);
            }
            else 
            {
                this.IsMoving = false;
            }               
        }
    }
}
