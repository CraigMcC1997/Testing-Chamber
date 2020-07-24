using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Testing_Chamber
{
    class MV_Speech : Sprite
    {
        //private variables
        private WindowWidth m_windowwidth;
        private int m_windowheight;
        private ContentManager m_content;

        //sound effects
        private SoundEffectInstance Sound_MouseClick;
        private bool m_hasPlayed = false;

        //group of textures with a maximum number
        private Texture2D[] texMVTextBox = new Texture2D[8];

        //stops any newspaper from drawing
        int currentSpeech = -1;

        public MV_Speech(ContentManager Content, WindowWidth windowwidth, int windowheight)
        {
            //Asigns the variables
            m_content = Content;
            m_windowwidth = windowwidth;
            m_windowheight = windowheight;

            //loads sound effect
            Sound_MouseClick = Content.Load<SoundEffect>("Sound Files/Click").CreateInstance();

            //take the generic file name and add a number to it to find a specific one in the list
            for (int i = 0; i < texMVTextBox.Count(); i++)
            {
                texMVTextBox[i] = Content.Load<Texture2D>("Speeches/Textbox" + (i + 1));
            }
        }

        //when the player picks up the news paper in each level
        public void StartTalking(int whichSpeech)
        {
            currentSpeech = whichSpeech;
        }

        protected override Vector2 PerformMove(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            //stops drawing the article
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                currentSpeech = -1;

                if (Sound_MouseClick.State != SoundState.Playing && !m_hasPlayed)
                {
                    Sound_MouseClick.Play();
                    m_hasPlayed = true;
                }
            }
        }

        //draw
        new public void Draw(SpriteBatch spriteBatch)
        {
            //if viewing paper, draw newspaper article
            if (currentSpeech > -1)
            {
                spriteBatch.Draw(texMVTextBox[currentSpeech], 
                    new Vector2(m_windowwidth.Val /2 - texMVTextBox[currentSpeech].Width / 2,
                    m_windowheight /2 - texMVTextBox[currentSpeech].Height / 2), Color.White);
            }
        }
    }
}
