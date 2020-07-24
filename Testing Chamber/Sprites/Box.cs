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
    class Box : Sprite
    {
        private ContentManager m_content;
        private WindowWidth m_WindowWidth;
        private int m_WindowHeight;

        //for use when moving box
        public Vector2 velocity;

        //spawn position for box
        public Vector2 SpawnPos;

        //Sound effects
        SoundEffect Wrong;

        public Box(ContentManager Content, WindowWidth WindowWidth, int WindowHeight)
        {
            m_content = Content;
            m_WindowWidth = WindowWidth;
            m_WindowHeight = WindowHeight;

            //spawn pos for box
            SpawnPos = new Vector2(m_WindowWidth.Val / 2, m_WindowHeight / 2);

            //Loads the sound file for from the content pipeline
            Wrong = Content.Load<SoundEffect>("Sound Files/Wrong");
        }

        protected override Vector2 PerformMove(GameTime gameTime)
        {
            if (position.X <= 0)
            {
                position = SpawnPos;
                velocity = Vector2.Zero;
                Wrong.Play();
            }

            if (position.Y <= 0)
            {
                position = SpawnPos;
                velocity = Vector2.Zero;
                Wrong.Play();
            }

            if (position.X >= (m_WindowWidth.Val - Texture.Width))
            {
                position = SpawnPos;
                velocity = Vector2.Zero;
                Wrong.Play();
            }

            if (position.Y >= (m_WindowHeight - Texture.Height))
            {
                position = SpawnPos;
                velocity = Vector2.Zero;
                Wrong.Play();
            }
            return velocity;
        }
    }
}
