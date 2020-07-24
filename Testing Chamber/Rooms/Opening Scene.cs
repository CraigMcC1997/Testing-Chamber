using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Testing_Chamber
{
    class Opening_Scene : IRoom
    {
        //Info to allow the next room to load
        ContentManager m_content;
        WindowWidth m_windowwidth;
        int m_windowheight;
        private GraphicsDeviceManager m_graphics;

        //classes
        SpeechBox m_SpeechBox;
        Textbox m_TextBox;
        BackSpace m_BackSpace;
   
        //animation
        AnimatedBlood anim_Blood;

        //Loads the font
        SpriteFont Font;

        //timer
        int timer;

        //bool
        private bool IsSpeechOver = false;

        //array of the dialog
        private string[] dialogs = new string[]
            {
                "Welcome",
                "You're probably wondering",
                "where you are?",
                "Well you're in a government funded",
                "testing lab",
                "unfortunately I can't confirm where",
                "that information has to stay private for...",
                "...safety reasons",
                "You see we have a problem on our hands",
                "there's a threat to humanity as we know it",
                "and right now",
                "you're our last hope to stop it",
                "I wish I could tell you more",
                "but safety and all that mumbo jumbo",
                "All we need you to do is",
                "complete some tests for us",
                "if you complete the tests and you...",
                "survive...",
                "...well let's just say",
                "it would be preferable",
                "Now, if you're ready",
                "we may begin the tests"
            };

        //text to be displayed after the opening speech
        private string PressEnter = "Press Enter";

        public Opening_Scene(ContentManager content, WindowWidth windowwidth, int windowheight, Player player, GraphicsDeviceManager graphics)
        {
            //Asigns the variables
            m_content = content;
            m_windowwidth = windowwidth;
            m_windowheight = windowheight;
            m_graphics = graphics;

            //sets the spawn location
            player.position = player.GenericSPWN;

            //Loads the font from the font sheet
            Font = content.Load<SpriteFont>("Font");

            //Textures
            var texSpeechBox = content.Load<Texture2D>("Textures/TextBox");
            var texTextBox = content.Load<Texture2D>("Textures/GENERICTextBox");
            var texBackSpace = content.Load<Texture2D>("Textures/BackSpace");

            //animations
            var BloodAnimation = content.Load<Texture2D>("Textures/Blood");

            //animated blood
            anim_Blood = new AnimatedBlood(BloodAnimation);
            anim_Blood.Position = new Vector2(m_windowwidth.Val - BloodAnimation.Width / 6, 0);

            //speech box
            m_SpeechBox = new SpeechBox();
            m_SpeechBox.LoadContent(texSpeechBox, 
                new Vector2(m_windowwidth.Val /2 - texSpeechBox.Width /2, m_windowheight /2 - texSpeechBox.Height /2));

            //speech box
            m_TextBox = new Textbox();
            m_TextBox.LoadContent(texTextBox,
                new Vector2(m_windowwidth.Val / 2 - texTextBox.Width / 2, m_windowheight / 2 - texTextBox.Height / 2));

            m_BackSpace = new BackSpace();
            m_BackSpace.LoadContent(texBackSpace,
                new Vector2(0, m_windowheight - texBackSpace.Height));
        }

        public void Update(GameTime gameTime, Player player)
        {
            //allows the player to move
            player.GiveLife();

            //timer starts when game starts
            timer++;

            //if timer count is higher than amount of lines of text to display then load the first level
            if (timer >= dialogs.Count() * 100)
            {
                IsSpeechOver = true;
            }

            //stops player walking over the speech box
            if (player.BoundingBox.Intersects(m_SpeechBox.BoundingBox) && !IsSpeechOver)
            {
                player.ResetPos();
            }

            //if player presses enter start game
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && IsSpeechOver)
            {
                if (LoadNextRoom != null)
                {
                    LoadNextRoom(new Testing_room(m_content, m_windowwidth, m_windowheight, player));
                }
            }

            //if player presses backspace; return to game
            if (Keyboard.GetState().IsKeyDown(Keys.Back) && IsSpeechOver)
            {
                if (LoadNextRoom != null)
                {
                    LoadNextRoom(new MenuRoom(m_content, m_windowwidth, m_windowheight, m_graphics));
                }
            }


            //plays animations
            anim_Blood.PlayAnimation("Blood");

            //updates the animations
            anim_Blood.Update(gameTime);
        }

        public event Action<IRoom> LoadNextRoom;

        public void Draw(SpriteBatch spriteBatch)
        {
            //draws the animation
            anim_Blood.Draw(spriteBatch);

            //since timer goes up in 100's divide it down to create single digits
            int line = timer / 100;

            //if speech is not over keep drawing it
            if (!IsSpeechOver)
            {
                //Draws the text box
                m_SpeechBox.Draw(spriteBatch);

                //measures length of text
                Vector2 textSize = Font.MeasureString(dialogs[line]);

                //draws whatever is in the dialog array
                spriteBatch.DrawString(Font, dialogs[line],
                    new Vector2(m_SpeechBox.position.X + m_SpeechBox.Texture.Width / 2, m_windowheight / 2 + 30),
                    Color.Black,
                    0,
                    textSize / 2,
                    1,
                    SpriteEffects.None,
                    0);
            }

            //if speech is over, display new text
            if (IsSpeechOver)
            {
                //draws the backspace indicator 
                m_BackSpace.Draw(spriteBatch);

                //draws the empty textbox
                m_TextBox.Draw(spriteBatch);

                //measures length of text
                Vector2 textSize = Font.MeasureString(PressEnter);

                //draws whatever is in the dialog array
                spriteBatch.DrawString(Font, PressEnter,
                    new Vector2(m_TextBox.position.X + m_TextBox.Texture.Width / 2, m_windowheight / 2),
                    Color.Green,
                    0,
                    textSize / 2,
                    1,
                    SpriteEffects.None,
                    0);
            }
        }
    }
}
