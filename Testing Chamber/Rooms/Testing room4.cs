using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Testing_Chamber
{
    class Testing_room4 : IRoom
    {
        //Info to allow the next room to load
        ContentManager m_content;
        WindowWidth m_windowwidth;
        int m_windowheight;

        //list of spikes
        List<Spike> m_Spikes;

        //classes
        Button m_Button;
        Newspaper m_Newspaper;
        MV_Speech m_Speech;
        Bones m_Bones;

        //Loads the font
        SpriteFont Arial;

        //picks a location for the player to spawn
        private Vector2 spawnLocation;

        //constructor FOR ROOM4
        public Testing_room4(ContentManager Content, WindowWidth windowwidth, int windowheight, Player player)
        {
            //Asigns the variables
            m_content = Content;
            m_windowwidth = windowwidth;
            m_windowheight = windowheight;

            //Where the player spawns when they die
            spawnLocation = new Vector2(m_windowwidth.Val - player.Texture.Width * 3, m_windowheight - player.Texture.Height);

            //sets the players position to the rooms spawn loaction
            player.position = spawnLocation;

            //Loads the font from the font sheet
            Arial = Content.Load<SpriteFont>("Font");

            //textures
            var texSpike = Content.Load<Texture2D>("Spike");

            //button
            m_Button = new Button(Content, m_windowwidth);
            m_Button.LoadContent(m_Button.texButton, 
                new Vector2(m_Button.texButton.Width, 0));

            //bones
            m_Bones = new Bones(Content, m_windowwidth, m_windowheight);
            m_Bones.LoadContent(m_Bones.Texture,
                new Vector2(m_Bones.tex.Width /2, m_windowheight - m_Bones.tex.Height));

            //Newspaper
            m_Newspaper = new Newspaper(Content);
            m_Newspaper.LoadContent(m_Newspaper.texNewspaper,
                new Vector2(m_windowwidth.Val - m_Newspaper.texNewspaper.Width, 0));

            //list of spikes
            m_Spikes = new List<Spike>();

            //every X & Y value for the spikes
            int[] positions = new int[] 
            {
              1, 1,     3, 1,       1, 2,       3, 2,       1, 3,
              1, 4,     2, 4,       3, 4,       4, 4,       5, 4,
              5, 3,     5, 2,       6, 2,       7, 2,       9, 2,
              9, 1,     9, 3,       9, 4,       8, 4,       7, 4,
              7, 6,     6, 6,       5, 6,       4, 6,       3, 6, 
              2, 7,     2, 6,       2, 8,       8, 6,       9, 6,
              9, 7,     10, 7,      10, 8,      10, 4,      11, 4,
              11, 5,    12, 5
            };

            //position of new spike = original spike * next number in the array
            for (int s = 0; s<positions.Count() ; s += 2)
            {
                var Spikes = new Spike();
                Spikes.LoadContent(texSpike, 
                    new Vector2(m_windowwidth.Val - texSpike.Width * positions[s], m_windowheight - texSpike.Height * positions[s+1]));
                m_Spikes.Add(Spikes);
            }

            //Loads the speech class
            m_Speech = new MV_Speech(Content, m_windowwidth, m_windowheight);

            //Chooses which speech to start each level with
            m_Speech.StartTalking(3);
        }

        public void Update(GameTime gameTime, Player player)
        {
            //spikes killing player if they interact
            foreach (var spike in m_Spikes)
            {
                if (player.BoundingBox.Intersects(spike.BoundingBox))
                {
                    player.position = spawnLocation;
                }
            }

            //loading next room
            if (player.BoundingBox.Intersects(m_Button.BoundingBox))
            {
                if (LoadNextRoom != null)
                {
                    LoadNextRoom(new Testing_room5(m_content, m_windowwidth, m_windowheight, player));
                }
            }

            //Player picking up the newspaper
            if (player.BoundingBox.Intersects(m_Newspaper.BoundingBox))
            {
                m_Newspaper.PickUpPaper(3);
            }

            //updates the newspaper and speech
            m_Newspaper.Update(gameTime);
            m_Speech.Update(gameTime);
        }

        public event Action<IRoom> LoadNextRoom;

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draws every spike in the list
            foreach (var spike in m_Spikes)
            {
                spike.Draw(spriteBatch);
            }

            //draws textures
            m_Button.Draw(spriteBatch);
            m_Bones.Draw(spriteBatch);
            m_Newspaper.Draw(spriteBatch);
            m_Speech.Draw(spriteBatch);

            //to identify which room you are currently in
            spriteBatch.DrawString(Arial, "Test Room 4", new Vector2(20, 20), Color.Black);
        }
    }
}
