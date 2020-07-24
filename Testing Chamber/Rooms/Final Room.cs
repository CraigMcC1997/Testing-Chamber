using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Testing_Chamber
{
    class Final_Room : IRoom
    {
        //Info to allow the next room to load
        private ContentManager m_Content;
        private WindowWidth m_WindowWidth;
        private int m_WindowHeight;
        private GraphicsDeviceManager m_Graphics;

        //timer
        int timer;

        //textures
        private Texture2D BlackOut;

        //if speech has finished or not
        private bool IsSpeechOver = false;
        private bool ShouldRestartDisplay = false;

        //Loads the font
        SpriteFont Font;

        //animations
        Animated_Ending m_Anim_End, m_Anim_End2;

        //array of the first dialog
        private string[] dialogs = new string[]
            {
                "",
                "",
                "Well I must congratulate you",
                "You are the first person to complete our tests",
                "so congratulations",
                "now I think it's time you learned the truth",
                "about this facility",
                "and why you're actually here",
                "...",
                "We are not the government",
                "nor is this government funded",
                "in fact, it's quite the opposite",
                "you see we didn't want to create",
                "an army to FIGHT the apocalypse",
                "we wanted to create an army to BE the apocalypse",
                "...",
                "I apologise for all the lies",
                "but we needed the perfect subject",
                "and it's you, all we need to do now is begin cloning",
                "Now, if you're ready",
                "we may begin the cloning"
            };

        //array of the second dialog
        private string[] dialogs2 = new string[]
        {
            "Oh no a power cut...",
            "guess we'll have to continue this...",
            "Next time... ;)",
        };

        //array of the second dialog
        private string Restart = "Press ENTER to return to menu";

        //constructor for final room
        public Final_Room(ContentManager Content, WindowWidth windowWidth, int windowHeight, GraphicsDeviceManager graphics)
        {
            //Asigns the variables
            m_Content = Content;
            m_WindowWidth = windowWidth;
            m_WindowHeight = windowHeight;
            m_Graphics = graphics;

            //Loads the font from the font sheet
            Font = m_Content.Load<SpriteFont>("Font");

            BlackOut = m_Content.Load<Texture2D>("Textures/Black Out");

            //spritesheets
            var EndAnimation = m_Content.Load<Texture2D>("Textures/Final Scene");
            var TalkAnimation = m_Content.Load<Texture2D>("Textures/Talking");

            //animated ending
            m_Anim_End = new Animated_Ending(EndAnimation);
            m_Anim_End.Position = new Vector2(m_WindowWidth.Val /2 - EndAnimation.Width /28, windowHeight /2 - EndAnimation.Height /2);

            //animated ending
            m_Anim_End2 = new Animated_Ending(TalkAnimation);
            m_Anim_End2.Position = new Vector2(m_WindowWidth.Val / 2 - TalkAnimation.Width / 16, windowHeight / 2 - TalkAnimation.Height / 2);
        }

        public void Update(GameTime gameTime, Player player)
        {
            //Player is moved off screen and kept there
            player.position = new Vector2(-m_WindowWidth.Val, 0);

            //plays animation
            m_Anim_End.PlayAnimation("End");
            m_Anim_End2.PlayAnimation("Talk");

            //updates animation
            m_Anim_End.Update(gameTime);
            m_Anim_End2.Update(gameTime);

            //timer starts when game starts
            timer++;

            //if timer count is higher than amount of lines of text to display then set to true
            if (timer >= dialogs.Count() * 100)
            {
                IsSpeechOver = true;
                timer = 0;
            }

            //Going back to the main menu from options menu
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (IsSpeechOver)
                {
                    if (LoadNextRoom != null)
                    {
                        LoadNextRoom(new MenuRoom(m_Content, m_WindowWidth, m_WindowHeight, m_Graphics));
                    }
                }
            }
        }

        public event Action<IRoom> LoadNextRoom;

        public void Draw(SpriteBatch spriteBatch)
        {
          
            //since timer goes up in 100's divide it down to create single digits
            int line = timer / 100;

            if (timer < 200)
            {
                //draws animation
                m_Anim_End.Draw(spriteBatch);
            }
            else
            {
                m_Anim_End2.Draw(spriteBatch);
            }

            //if speech is not over keep drawing it
            if (!IsSpeechOver)
            {
                //measures length of text
                Vector2 textSize = Font.MeasureString(dialogs[line]);

                //draws whatever is in the dialog array
                spriteBatch.DrawString(Font, dialogs[line],
                    new Vector2(m_WindowWidth.Val /2, m_WindowHeight / 5),
                    Color.Black,
                    0,
                    textSize / 2,
                    1,
                    SpriteEffects.None,
                    0);
            }

            //after main speech
            if (IsSpeechOver)
            {
                //something
                spriteBatch.Draw(BlackOut, Vector2.Zero, Color.Black);

                if (line < 3)
                {
                    //measures length of text
                    Vector2 textSize = Font.MeasureString(dialogs2[line]);

                    //draws whatever is in the dialog array
                    spriteBatch.DrawString(Font, dialogs2[line],
                        new Vector2(m_WindowWidth.Val / 2, m_WindowHeight / 2 - 100),
                        Color.White,
                        0,
                        textSize / 2,
                        1,
                        SpriteEffects.None,
                        0);
                }

                if (line > 3)
                {
                    ShouldRestartDisplay = true;
                }
            }

            if (ShouldRestartDisplay)
            {
                //measures length of text
                Vector2 textSize = Font.MeasureString(Restart);

                //draws whatever is in the dialog array
                spriteBatch.DrawString(Font, Restart,
                    new Vector2(m_WindowWidth.Val / 2, m_WindowHeight / 2 - 100),
                    Color.Yellow,
                    0,
                    textSize / 2,
                    1,
                    SpriteEffects.None,
                    0);
            }
        }
    }
}
