using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Testing_Chamber
{
    class Arrow : Sprite
    {
        private WindowWidth m_ScreenWidth;

        //texture
        public Arrow(WindowWidth width)
        {
            m_ScreenWidth = width;
        }

        //moving arrows
        private bool MoveLeft;
        private const int Velocity = 40;

        protected override Vector2 PerformMove(GameTime gameTime)
        {
            Vector2 velocity = Vector2.Zero;

            if (position.X >= m_ScreenWidth.Val - Texture.Width)
            {
                MoveLeft = false;
            }

            if (position.X <= 0)
            {
                MoveLeft = true;
            }

            //move left
            if(MoveLeft)
            {
                velocity = new Vector2(Velocity, 0);
            }

            //move right
            if(!MoveLeft)
            {
                velocity = new Vector2(-Velocity, 0);
            }

            return velocity;
        }
    }
}
