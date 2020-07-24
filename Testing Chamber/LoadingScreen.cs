using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Testing_Chamber
{
    class LoadingScreen : IRoom
    {
        private ContentManager m_Content;
        private WindowWidth m_WindowWidth;
        private int m_WindowHeight;
        private GraphicsDeviceManager m_Graphics;

        //Loads the font
        SpriteFont Arial;

        //3D model
        private Model m_model;

        //position for 3D model
        private Vector3[] position = new Vector3[2];

        //used for moving the 3D model
        private float angle;

        //text to be displayed after the opening speech
        private string Loading = "Loading...";

        public LoadingScreen(ContentManager Content,
            WindowWidth WindowWidth, int WindowHeight, GraphicsDeviceManager graphics)
        {
            m_Content = Content;
            m_WindowWidth = WindowWidth;
            m_WindowHeight = WindowHeight;
            m_Graphics = graphics;

            //Loads the font from the font sheet
            Arial = m_Content.Load<SpriteFont>("Font");

            //loads the 3D model
            m_model = m_Content.Load<Model>("3D Asset/UntexturedSphere");

            //gives the 3D model a position on screen
            position[0] = new Vector3(0, 0, 0);

            //sets a default angle
            angle = 0;
        }

        //Creates a helper function that allows us to easily draw models
        private void DrawModel(Model model, Vector3 position)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // The world matrix can be used to position, rotate
                    // or resize (scale) the model. Identity means that
                    // the model is unrotated, drawn at the origin, and
                    // its size is unchanged from the loaded content file.
                    effect.World = Matrix.CreateTranslation(position) * Matrix.CreateRotationZ(angle);

                    // Move the camera 10 units away from the origin:
                    var cameraPosition = new Vector3(0, 0, 10);
                    // Tell the camera to look at the origin:
                    var cameraLookAtVector = Vector3.Zero;
                    // Tell the camera that positive Z is up
                    var cameraUpVector = Vector3.UnitY;

                    // turn on the lighting subsystem.
                    effect.LightingEnabled = true;

                    // a blue light
                    effect.DirectionalLight0.DiffuseColor = new Vector3(0.0f, 0, 0.5f);

                    // coming along the z-axis
                    effect.DirectionalLight0.Direction = new Vector3(0, 0, -15);

                    // with red highlights
                    effect.DirectionalLight0.SpecularColor = new Vector3(1, 0, 0);

                    effect.View = Matrix.CreateLookAt(
                     cameraPosition, cameraLookAtVector, cameraUpVector);

                    // We want the aspect ratio of our display to match
                    // the entire screen's aspect ratio:
                    float aspectRatio =
                        m_Graphics.PreferredBackBufferWidth / (float)m_Graphics.PreferredBackBufferHeight;

                    // Field of view measures how wide of a view our camera has.
                    // Increasing this value means it has a wider view, making everything
                    // on screen smaller. This is conceptually the same as "zooming out".
                    // It also 
                    float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;

                    // Anything closer than this will not be drawn (will be clipped)
                    float nearClipPlane = 1;

                    // Anything further than this will not be drawn (will be clipped)
                    float farClipPlane = 200;

                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
                }
                mesh.Draw();
            }
        }

        public void Update(GameTime gameTime, Player player)
        {
            //stops player appearing on screen
            player.position = new Vector2(-m_WindowWidth.Val, 0);

            //timer based on time game has been loaded
            int Timer = gameTime.TotalGameTime.Seconds;

            //how fast the 3D object spins
            angle += 0.01f;

            //Load game after a set amount of time
            if (Timer >= 10)
            {
                if (m_loadNextRoom != null)
                {
                    m_loadNextRoom(new Opening_Scene(m_Content, m_WindowWidth, m_WindowHeight, player, m_Graphics));
                }
            }
        }

        //Loading the next possible rooms
        private Action<IRoom> m_loadNextRoom;

        //adds to the value to allow the rooms to change
        public event Action<IRoom> LoadNextRoom
        {
            add { m_loadNextRoom += value; }
            remove { m_loadNextRoom -= value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            
            // Important piece of code that enables depth buffer for 3d objects
            m_Graphics.GraphicsDevice.DepthStencilState = new DepthStencilState()
            {
                DepthBufferEnable = true
            };

            //draws the 3D model
            DrawModel(m_model, position[0]);

            //measures length of text
            Vector2 textSize = Arial.MeasureString(Loading);

            spriteBatch.Begin();

            //draws loading text
            spriteBatch.DrawString(Arial, Loading,
                new Vector2(m_WindowWidth.Val / 2, m_WindowHeight / 2),
                Color.White,
                0,
                textSize / 2,
                1,
                SpriteEffects.None,
                0);
        }
    }
}