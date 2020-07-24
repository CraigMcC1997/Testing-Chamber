using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Testing_Chamber
{
    class Button : Sprite
    {
        private ContentManager m_content;
        private WindowWidth m_ScreenWidth;

        private bool moveRight = true;
        private Vector2 velocity;

        public Texture2D texButton;

        private SoundEffect m_OpenDoor;

        public Button(ContentManager Content, WindowWidth ScreenWidth)
        {
            m_content = Content;
            m_ScreenWidth = ScreenWidth;

            texButton = Content.Load<Texture2D>("Button");

            m_OpenDoor = Content.Load<SoundEffect>("Sound Files/Open");
        }

        //Play sound
        public void PlaySound()
        {
            m_OpenDoor.Play();
        }

        protected override Vector2 PerformMove(GameTime gameTime)
        {
            if (position.X > m_ScreenWidth.Val - Texture.Width)
            {
                moveRight = false;
            }
            
            if (position.X < 0)
            {
                moveRight = true;
            }

            if (moveRight)
            {
                velocity = new Vector2(10,0);
            }

            if (!moveRight)
            {
                velocity = new Vector2(-10,0);
            }

            return velocity;
        }

        //draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texButton, position, Color.White);
        }
    }
}
