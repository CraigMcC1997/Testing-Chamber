using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Testing_Chamber
{
    class Player : Sprite
    {
        private ContentManager m_content;
        private WindowWidth m_ScreenWidth;
        private int m_ScreenHeight;

        //Give player health
        private int Health = 100;
        private int m_FullHealth = 100;
        public Vector2 velocity = Vector2.Zero;

        //generic spawn location
        public Vector2 GenericSPWN;

        public Player(ContentManager Content, WindowWidth m_windowwidth, int m_windowheight)
        {
            m_content = Content;
            m_ScreenWidth = m_windowwidth;
            m_ScreenHeight = m_windowheight;

            //Gives the player a generic spawn that can be used in multiple levels
            GenericSPWN = new Vector2(m_windowwidth.Val / 2, m_windowheight - 200);
        }

        //Moves the player using the W,A,S and D keys
        protected override Vector2 PerformMove(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            //if player is still alive
            if (Health > 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    velocity = new Vector2(0, -10);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    velocity = new Vector2(0, 10);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    velocity = new Vector2(-10, 0);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    velocity = new Vector2(10, 0);
                }
            }
            return velocity;
        }

        //player dying
        public void KillPlayer()
        {
            Health = 0;
            //play death sound
            //switch to death animation
        }

        //brings the player back to life
        public void GiveLife()
        {
            Health = m_FullHealth;
        }

        public void Update(GameTime gametime)
        {
            //stops the player leaving the screen
            if (position.X <= 0)
            {
                ResetPos();
            }

            if (position.Y <= 0)
            {
                ResetPos();
            }

            if (position.X >= (m_ScreenWidth.Val - Texture.Width))
            {
                ResetPos();
            }

            if (position.Y >= (m_ScreenHeight - Texture.Height))
            {
                ResetPos();
            }
        }
    }
}
