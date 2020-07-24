using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Testing_Chamber
{
    class Newspaper : Sprite
    {
        private ContentManager m_content;

        //textures
        public Texture2D texNewspaper;
        private Texture2D[] texNPArticle = new Texture2D[8];

        //stops any newspaper from drawing
        int currentNewspaper = -1;

        public Newspaper(ContentManager Content)
        {
            m_content = Content;

            //Loads the news paper image
            texNewspaper = Content.Load<Texture2D>("Newspaper");

            //take the generic file name and add a number to it to find a specific one in the list
            for (int i = 0; i < texNPArticle.Count(); i++)
            {
                texNPArticle[i] = Content.Load<Texture2D>("NewsArticles/NA" + (i+1));
            }
        }

        protected override Vector2 PerformMove(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        //when the player picks up the news paper in each level
        public void PickUpPaper(int whichpaper)
        {
            currentNewspaper = whichpaper;
        }

        public void Update(GameTime gameTime)
        {
            //stops drawing the article
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                currentNewspaper = -1;
            }
        }

        //draw
        new public void Draw(SpriteBatch spriteBatch)
        {
            //draws the newspaper 
            spriteBatch.Draw(texNewspaper, position, Color.White);

            //if viewing paper, draw newspaper article
            if (currentNewspaper > -1)
            {
                spriteBatch.Draw(texNPArticle[currentNewspaper], Vector2.Zero, Color.White);
            }
        }
    }
}
