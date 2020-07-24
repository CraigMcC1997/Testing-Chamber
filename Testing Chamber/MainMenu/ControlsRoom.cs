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
    class ControlsRoom : IRoom
    {
        //private variables
        private WindowWidth m_windowwidth;
        private int m_windowheight;
        private ContentManager m_content;
        private GraphicsDeviceManager m_graphics;

        //controls texture
        private Texture2D image;

        //sound effects
        SoundEffect Sound_MouseClick;

        public ControlsRoom(ContentManager Content, WindowWidth windowwidth, int windowheight, GraphicsDeviceManager graphics)
        {
            //Asigns the variables
            m_content = Content;
            m_windowwidth = windowwidth;
            m_windowheight = windowheight;
            m_graphics = graphics;

            //loads the image
            image = Content.Load<Texture2D>("Textures/Controls Page");

            //loads the sound effect
            Sound_MouseClick = Content.Load<SoundEffect>("Sound Files/Click");
        }

        public void Update(GameTime gameTime, Player player)
        {
            //stops the player appearing on screen
            player.position = new Vector2(-m_windowwidth.Val, 0);

            //Going back to the main menu from options menu
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                if (LoadNextRoom != null)
                {
                    //play sound
                    Sound_MouseClick.Play();

                    //load specific room
                    LoadNextRoom(new MenuRoom(m_content, m_windowwidth, m_windowheight, m_graphics));
                }
            }
        }

        public event Action<IRoom> LoadNextRoom;

        public void Draw(SpriteBatch spriteBatch)
        {
            //draw the image
            spriteBatch.Draw(image, Vector2.Zero, Color.White);
        }
    }
}