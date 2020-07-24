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
    class MenuRoom : IRoom
    {
        //private variables
        private WindowWidth m_windowwidth;
        private int m_windowheight;
        private ContentManager m_content;
        private GraphicsDeviceManager m_graphics;

        //Classes
        ButtonStartNewGame m_StartNewGame;
        ButtonControls m_Controls;
        ButtonExit m_Exit;

        //title
        Texture2D texTitle;

        //animations
        AnimatedSkull anim_Skull;

        //sound effects
        SoundEffect Sound_MouseClick;

        public MenuRoom(ContentManager Content, WindowWidth windowwidth, 
            int windowheight, GraphicsDeviceManager graphics)
        {
            //Asigns the variables
            m_content = Content;
            m_windowwidth = windowwidth;
            m_windowheight = windowheight;
            m_graphics = graphics;

            //loads sound effect
            Sound_MouseClick = Content.Load<SoundEffect>("Sound Files/Click");

            //textures
            var texStartNewGame = Content.Load<Texture2D>("Textures/Start");
            var texControls = Content.Load<Texture2D>("Textures/Controls");
            var texExitGame = Content.Load<Texture2D>("Textures/Exit");
            var SkullAnimation = Content.Load<Texture2D>("Textures/Skull");
            texTitle = Content.Load<Texture2D>("Textures/Title");

            //animated skull
            anim_Skull = new AnimatedSkull(SkullAnimation);
            anim_Skull.Position = new Vector2(0, m_windowheight - SkullAnimation.Height);

            //Start a new game
            m_StartNewGame = new ButtonStartNewGame();
            m_StartNewGame.LoadContent(texStartNewGame, 
                new Vector2(m_windowwidth.Val / 2 - texStartNewGame.Width / 2,
                m_windowheight - texExitGame.Height * 3 - 100));

            //Load the options
            m_Controls = new ButtonControls();
            m_Controls.LoadContent(texControls, 
                new Vector2(m_windowwidth.Val / 2 - texStartNewGame.Width / 2,
                m_windowheight - texExitGame.Height *2 - 50));

            //Exits the game
            m_Exit = new ButtonExit();
            m_Exit.LoadContent(texExitGame, 
                new Vector2(m_windowwidth.Val / 2 - texExitGame.Width / 2,
                m_windowheight - texExitGame.Height));
        }

        public void Update(GameTime gameTime, Player player)
        {
            //Stops player moving on screen during the main menu
            player.position = new Vector2(-player.Texture.Width, 0);

            //Defines mouse state
            var mouseState = Mouse.GetState();

            //detects mouse X and Y
            var mouseX = (int)mouseState.Position.X;
            var mouseY = (int)mouseState.Position.Y;

            //if START is chosen load the player into the start of the game
            if (m_StartNewGame.BoundingBox.Contains(mouseX, mouseY) 
                && mouseState.LeftButton == ButtonState.Pressed)
            {
                if (LoadNextRoom != null)
                {
                    Sound_MouseClick.Play();
                    LoadNextRoom(new LoadingScreen(m_content, m_windowwidth, m_windowheight, m_graphics));
                }
            }

            //if CONTROLS is chosen load player into the controls menu screen
            if (m_Controls.BoundingBox.Contains(mouseX, mouseY) 
                && mouseState.LeftButton == ButtonState.Pressed)
            {
                if (LoadNextRoom != null)
                {
                    //play sound
                    Sound_MouseClick.Play();

                    //load room
                    LoadNextRoom(new ControlsRoom(m_content, m_windowwidth, m_windowheight, m_graphics));
                }
            }

            //if EXIT is chosen close game
            if (m_Exit.BoundingBox.Contains(mouseX, mouseY)
                && mouseState.LeftButton == ButtonState.Pressed)
            {
                Sound_MouseClick.Play();
                Exit();
            }

            //DEVELOPMENT CODE
            if (Keyboard.GetState().IsKeyDown(Keys.Insert))
            {
                if (LoadNextRoom != null)
                {
                    LoadNextRoom(new Testing_room3(m_content, m_windowwidth, m_windowheight, player));
                }
            }

            //plays animations
            anim_Skull.PlayAnimation("Skull");

            //updates the animations
            anim_Skull.Update(gameTime);
        }

        //events
        public event Action<IRoom> LoadNextRoom;
        public event Action Exit;

        public void Draw(SpriteBatch spriteBatch)
        {
            //draws the textures
            m_StartNewGame.Draw(spriteBatch);
            m_Controls.Draw(spriteBatch);
            m_Exit.Draw(spriteBatch);
            anim_Skull.Draw(spriteBatch);

            //draws the title
            spriteBatch.Draw(texTitle, 
                new Vector2(m_windowwidth.Val /2 - texTitle.Width /2, m_windowheight /5), 
                Color.White);
        }
    }
}