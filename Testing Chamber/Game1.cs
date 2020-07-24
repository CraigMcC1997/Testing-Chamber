using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Testing_Chamber
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //current room
        IRoom m_currentRoom;

        //Song for game
        Song Theme;

        //Loads in the classes
        Player player;
        Background m_Background;
        
        //States the window size (uneditable)
        private readonly WindowWidth m_WindowWidth = new WindowWidth(1500);
        private readonly int m_WindowHeight = 900;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            //Displays mouse on screen
            this.IsMouseVisible = true;

            //screen size
            graphics.PreferredBackBufferWidth = m_WindowWidth.Val;
            graphics.PreferredBackBufferHeight = m_WindowHeight;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loads the class info into the game
            player = new Player(Content, m_WindowWidth, m_WindowHeight);
            m_Background = new Background(Content, m_WindowWidth, m_WindowHeight);

            //Gives the player a texture and a starting position on screen
            player.LoadContent(Content.Load<Texture2D>("Textures/Player"), Vector2.Zero);

            //background info
            m_Background.LoadContent(m_Background.Texture, Vector2.Zero);

            //choses which room loads first
            m_currentRoom = new MenuRoom(Content, m_WindowWidth, m_WindowHeight, graphics);
            (m_currentRoom as MenuRoom).Exit += Exit;
            m_currentRoom.LoadNextRoom += LoadNextRoom;

            //Song for game
            Theme = Content.Load<Song>("Sound Files/Theme");
            MediaPlayer.Play(Theme);
        }

        //Loads the next room
        private void LoadNextRoom(IRoom nextRoom)
        {
            m_currentRoom = nextRoom;
            m_currentRoom.LoadNextRoom += LoadNextRoom;

            //allows player to exit the main menu with the exit button
            if (m_currentRoom is MenuRoom)
            {
                (m_currentRoom as MenuRoom).Exit += Exit;
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Moves the player
            player.Move(gameTime);

            //Updates the player
            player.Update(gameTime);

            //allows the player to move when room loads
            player.GiveLife();

            //Updates the current room
            m_currentRoom.Update(gameTime, player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            //draws the background before anything else
            m_Background.Draw(spriteBatch);

            //Draws each object in each room
            m_currentRoom.Draw(spriteBatch);

            //Draws everything in the class with a texture
            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}