using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Testing_Chamber
{
    class Testing_room6 : IRoom
    {
        //Info to allow the next room to load
        ContentManager m_content;
        WindowWidth m_windowwidth;
        int m_windowheight;

        //textures
        List<Textbox> m_textBoxes;
        Newspaper m_Newspaper;
        MV_Speech m_Speech;

        //bools
        private bool CorrectAnswer = false;
        private bool WrongAnswer = false;

        //Sound effects
        SoundEffect Wrong, Right;

        //Loads the font
        SpriteFont Arial;

        //constructor FOR ROOM6
        public Testing_room6(ContentManager Content, WindowWidth windowwidth, int windowheight, Player player)
        {
            //Asigns the variables
            m_content = Content;
            m_windowwidth = windowwidth;
            m_windowheight = windowheight;

            //sets the spawn location
            player.position = player.GenericSPWN;

            //Loads the sound file for from the content pipeline
            Wrong = Content.Load<SoundEffect>("Sound Files/Wrong");
            Right = Content.Load<SoundEffect>("Sound Files/Correct");

            //Loads the font from the font sheet
            Arial = Content.Load<SpriteFont>("Font");

            //textures
            m_textBoxes = new List<Textbox>();
            var texTextBox = Content.Load<Texture2D>("TextBox");

            //textbox left
            var textbox = new Textbox();
            textbox.OnCollision += WrongAnswerCollision;
            textbox.LoadContent(texTextBox,
                new Vector2(0, m_windowheight /2 - texTextBox.Height /2));
            m_textBoxes.Add(textbox);

            //textbox middle
            textbox = new Textbox();
            textbox.OnCollision += CorrectAnswerCollision;
            textbox.LoadContent(texTextBox, 
                new Vector2(m_windowwidth.Val /2- texTextBox.Width /2, m_windowheight / 2 - texTextBox.Height / 2));
            m_textBoxes.Add(textbox);

            //textbox right
            textbox = new Textbox();
            textbox.OnCollision += WrongAnswerCollision;
            textbox.LoadContent(texTextBox, 
                new Vector2(m_windowwidth.Val - texTextBox.Width, m_windowheight / 2 - texTextBox.Height / 2));
            m_textBoxes.Add(textbox);

            //textbox question left
            textbox = new Textbox();
            textbox.LoadContent(texTextBox, 
                new Vector2(m_windowwidth.Val /2 - texTextBox.Width /6, m_windowheight / 2 - texTextBox.Height *2.5f));
            m_textBoxes.Add(textbox);

            //textbox question right
            textbox = new Textbox();
            textbox.LoadContent(texTextBox, 
                new Vector2(m_windowwidth.Val / 2- texTextBox.Width, m_windowheight / 2 - texTextBox.Height *2.5f));
            m_textBoxes.Add(textbox);

            //Newspaper
            m_Newspaper = new Newspaper(Content);
            m_Newspaper.LoadContent(m_Newspaper.texNewspaper, 
                new Vector2(0, m_windowheight - m_Newspaper.texNewspaper.Height));

            //Loads the speech class
            m_Speech = new MV_Speech(Content, m_windowwidth, m_windowheight);

            //Chooses which speech to start each level with
            m_Speech.StartTalking(5);
        }

        //moving to next room
        private void CorrectAnswerCollision()
        {
            CorrectAnswer = true;
            Right.Play();
        }

        //moving the player back two rooms
        private void WrongAnswerCollision()
        {
            WrongAnswer = true;
            Wrong.Play();
        }

        public event Action<IRoom> LoadNextRoom;

        public void Update(GameTime gameTime, Player player)
        {
            //stops the player moving through the textures
            foreach (var textbox in m_textBoxes)
            {
                if (player.BoundingBox.Intersects(textbox.BoundingBox))
                {
                    textbox.OnCollision();
                    player.ResetPos();
                }
            }

            //Loading player into the next room if they get the answer right
            if (CorrectAnswer)
            {
                if (LoadNextRoom != null)
                {
                    LoadNextRoom(new Testing_room7(m_content, m_windowwidth, m_windowheight, player));
                }
            }

            //Moves player two levels back if they get the answer wrong
            if (WrongAnswer)
            {
                if (LoadNextRoom != null)
                {
                    LoadNextRoom(new Testing_room4(m_content, m_windowwidth, m_windowheight, player));
                }
            }

            //Player picking up the newspaper
            if (player.BoundingBox.Intersects(m_Newspaper.BoundingBox))
            {
                m_Newspaper.PickUpPaper(5);
            }

            //updates the classes
            m_Newspaper.Update(gameTime);
            m_Speech.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //draws every texture in the list
            foreach(var textbox in m_textBoxes)
            {
                textbox.Draw(spriteBatch);
            }

            //Question
            spriteBatch.DrawString(Arial, "How many rooms have you completed so far?", new Vector2(350, 180), Color.White);

            //Answers
            spriteBatch.DrawString(Arial, "6", new Vector2(200, m_windowheight /2 - 20), Color.White);
            spriteBatch.DrawString(Arial, "5", new Vector2(m_windowwidth.Val /2, m_windowheight / 2 - 20), Color.White);
            spriteBatch.DrawString(Arial, "8", new Vector2(m_windowwidth.Val - 200, m_windowheight / 2 - 20), Color.White);

            //textures
            m_Newspaper.Draw(spriteBatch);
            m_Speech.Draw(spriteBatch);
        }
    }
}
