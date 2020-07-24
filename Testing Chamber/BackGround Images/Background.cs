using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Testing_Chamber
{
    class Background : Sprite
    {
        //Private Variables
        private ContentManager m_content;
        private WindowWidth m_ScreenWidth;
        private int m_ScreenHeight;

        //texture info
        private Texture2D tex;

        public Background(ContentManager Content, WindowWidth ScreenWidth, int ScreenHeight)
        {
            m_content = Content;
            m_ScreenWidth = ScreenWidth;
            m_ScreenHeight = ScreenHeight;

            //loads in the texture from the pipeline
            tex = Content.Load<Texture2D>("Textures/Background");
        }

        protected override Vector2 PerformMove(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        //draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            //draws the texture
            spriteBatch.Draw(tex, position, Color.White);
        }
    }
}

