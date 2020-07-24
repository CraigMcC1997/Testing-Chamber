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
    class Testing_room7 : IRoom
    {
        //Info to allow the next room to load
        ContentManager m_content;
        WindowWidth m_windowwidth;
        int m_windowheight;
        private GraphicsDeviceManager m_Graphics;

        //starting number of explosions
        const int InitialNum = 0;

        //Random explosions support
        Random rand = new Random();
        public Texture2D m_sprite;

        // spawning support
        const int TOTAL_SPAWN_DELAY_MILLISECONDS = 300;
        int elapsedSpawnDelayMilliseconds = 0;

        //Classes
        List<Explosion> m_Explosions = new List<Explosion>();
        Button m_Button;
        Newspaper m_Newspaper;
        MV_Speech m_Speech;

        //Loads the font
        SpriteFont Arial;

        //constructor FOR ROOM2
        public Testing_room7(ContentManager Content, WindowWidth windowwidth, int windowheight, Player player)
        {
            //Asigns the variables
            m_content = Content;
            m_windowwidth = windowwidth;
            m_windowheight = windowheight;

            //Loads the font from the font sheet
            Arial = Content.Load<SpriteFont>("Font");

            //sets the spawn location
            player.position = player.GenericSPWN;

            //load explosions sprites for efficiency
            m_sprite = Content.Load<Texture2D>("Textures/Explosion");

            //create initial game objects
            for (int i = 0; i < InitialNum; ++i)
            {
                m_Explosions.Add(GetRandomExplosion());
            }

            //button
            m_Button = new Button(Content, m_windowwidth);
            m_Button.LoadContent(m_Button.texButton,
                new Vector2(m_windowwidth.Val / 2 - m_Button.texButton.Width / 2, 0));

            //Newspaper
            m_Newspaper = new Newspaper(Content);
            m_Newspaper.LoadContent(m_Newspaper.texNewspaper, 
                new Vector2(0, m_windowheight - m_Newspaper.texNewspaper.Height));

            //Loads the speech class
            m_Speech = new MV_Speech(Content, m_windowwidth, m_windowheight);

            //Chooses which speech to start each level with
            m_Speech.StartTalking(6);
        }

        public void Update(GameTime gameTime, Player player)
        {
            //spawing the explosions
            elapsedSpawnDelayMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedSpawnDelayMilliseconds >= TOTAL_SPAWN_DELAY_MILLISECONDS)
            {
                // If we do not reset the timer more than one explosion
                // will spawn at once
                elapsedSpawnDelayMilliseconds = 0;

                // add another explosion to the list now
                m_Explosions.Add(GetRandomExplosion());

                // updates the explosions
                foreach (var explosion in m_Explosions)
                {
                    //updates the explosion
                    explosion.Update(gameTime);

                    //gives the explosions a textures
                    explosion.Texture = m_sprite;

                    //killing player if they intersect
                    if (player.BoundingBox.Intersects(explosion.BoundingBox))
                    {
                        player.position = player.GenericSPWN;
                    }
                }
            }

            //loading next room
            if (player.BoundingBox.Intersects(m_Button.BoundingBox))
            {
                if (LoadNextRoom != null)
                {
                    LoadNextRoom(new Testing_room8(m_content, m_windowwidth, m_windowheight, player));
                }
            }

            //Player picking up the newspaper
            if (player.BoundingBox.Intersects(m_Newspaper.BoundingBox))
            {
                m_Newspaper.PickUpPaper(5);
            }

            //updates the newspaper and speech
            m_Newspaper.Update(gameTime);
            m_Speech.Update(gameTime);

            for(int i=m_Explosions.Count()-1; i>=0; i--)
            {
                if (m_Explosions[i].Active != true)
                    m_Explosions.RemoveAt(i);
            }
        }

        //Gets random position for each explosion
        private Explosion GetRandomExplosion()
        {
            return new Explosion(m_sprite, m_windowwidth.Val, m_windowheight);
        }

        public event Action<IRoom> LoadNextRoom;

        public void Draw(SpriteBatch spriteBatch)
        {
            //draws the explosion
            foreach (Explosion explosion in m_Explosions)
            {
                explosion.Draw(spriteBatch);
            }

            //textures
            m_Button.Draw(spriteBatch);
            m_Newspaper.Draw(spriteBatch);
            m_Speech.Draw(spriteBatch);

            //to identify which room you are currently in
            spriteBatch.DrawString(Arial, "Test Room 7", new Vector2(20, 20), Color.Black);
        }
    }
}
