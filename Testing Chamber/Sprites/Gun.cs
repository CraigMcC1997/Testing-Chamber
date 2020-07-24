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
    class Gun : Sprite
    {
        private ContentManager m_content;
        private WindowWidth m_WindowWidth;
        private int m_WindowHeight;

        //texture
        public Texture2D texGun;

        public Gun(ContentManager Content, WindowWidth WindowWidth, int WindowHeight)
        {
            m_content = Content;
            m_WindowWidth = WindowWidth;
            m_WindowHeight = WindowHeight;

            //Texture
            texGun = Content.Load<Texture2D>("Textures/Gun");
        }

        protected override Vector2 PerformMove(GameTime gameTime)
        {
            throw new Exception();
        }

        //draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texGun, position, Color.White);
        }
    }
}
