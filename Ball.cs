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
    public class Ball
    {
        

        public Vector2 Speed, Bounds;
        public Rectangle Instance;
        private bool isMoving = false;

        public Ball(int boundsX, int boundsY)
        {
            Instance.X = 640;
            Instance.Y = 600;
            //Speed = new Vector2(7,-7);
            Speed = new Vector2(0, 0);
            Bounds.X = boundsX;
            Bounds.Y = boundsY;
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
                Instance.Width = value.Width;
                Instance.Height = value.Height;

                Bounds.X -= value.Width;
                Bounds.Y -= value.Height;

                _texture = value;
            }

        }

        public void UnloadContent() 
        {
            SpawnNewBall();
        }

        public void Update(Paddle paddle, GameTime gameTime, List<Block> blokken, GameManager gameManager) 
        {
            this.Move();

            CheckBoundaries(gameManager.Level, paddle);
            PaddleCollisionBounce(paddle);
            BlockCollisionBounce(blokken);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !isMoving && gameManager.KeyHitTimer >= 1)
                StartMoving();
        }

        public void Move() 
        {
            Instance.X += (int)Speed.X;
            Instance.Y += (int)Speed.Y;                             
        }

        private void CheckBoundaries(Level level, Paddle paddle) 
        {
            if (Instance.X >= Bounds.X || Instance.X <= 0) 
            {              
                Speed.X *= -1;
            }

            if (Instance.Y <= 0) 
            {
                Speed.Y *= -1;
            }

            if (Instance.Y >= Bounds.Y) 
            {
                level.Balls--;
                SpawnNewBall(paddle);
            }
                

            Instance.X = (int)MathHelper.Clamp(Instance.X, 1, Bounds.X-1);
        }

        private void BlockCollisionBounce(List<Block> blokken) 
        {
            int[] delta = new int[4];
            
            foreach (Block blok in blokken) 
            {
                if (this.Instance.Intersects(blok.Instance))
                {
                    delta[0] = Math.Abs(this.Instance.Left - blok.Instance.Right);
                    delta[1] = Math.Abs(this.Instance.Right - blok.Instance.Left);
                    delta[2] = Math.Abs(this.Instance.Bottom - blok.Instance.Top);
                    delta[3] = Math.Abs(this.Instance.Top - blok.Instance.Bottom);

                    int waardeDelta = delta[0];
                    int deltaKleinste = 0;

                    for (int i = 0; i < 4; i++) 
                    {
                        if (waardeDelta > delta[i])
                        {
                            waardeDelta = delta[i];
                            deltaKleinste = i;
                        }
                            
                    }

                    switch (deltaKleinste) 
                    {
                        case 0:
                            Speed.X = Math.Abs(Speed.X);
                            break;
                        case 1:
                            Speed.X = -Math.Abs(Speed.X);
                            break;
                        case 2:
                            Speed.Y = -Math.Abs(Speed.Y);
                            break;
                        case 3:
                            Speed.Y = Math.Abs(Speed.Y);
                            break;
                    }
                        
                }
            }

        }

        private void PaddleCollisionBounce(Paddle paddle) 
        {
            if (paddle.Instance.Intersects(this.Instance))
                Speed.Y *= -1;

            if (paddle.InstanceLeftHalf.Intersects(this.Instance))
                Speed.X = -((paddle.InstanceLeftHalf.Right - this.Instance.Center.X) / 8);

            if (paddle.InstanceRightHalf.Intersects(this.Instance))
                Speed.X = (this.Instance.Center.X - paddle.InstanceRightHalf.Left) / 8;
        }

        private void SpawnNewBall() 
        {
            this.Speed = new Vector2(0, 0);
            this.Instance.Y = 600;
            this.Instance.X = 600;
            this.isMoving = false;
        }

        private void SpawnNewBall(Paddle paddle) 
        {
            this.Speed = new Vector2(0, 0);
            this.Instance.Y = 600;
            this.Instance.X = paddle.Instance.X;
            this.isMoving = false;
        }

        private void StartMoving() 
        {
            isMoving = true;
            this.Speed = new Vector2(7, -7);
        }
    }
}
