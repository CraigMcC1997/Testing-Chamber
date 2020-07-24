using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Testing_Chamber
{
    class Testing_room3 : IRoom
    {
        //Info to allow the next room to load
        ContentManager m_content;
        WindowWidth m_windowwidth;
        int m_windowheight;

        //classes
        Box m_Box;
        Newspaper m_Newspaper;
        PressurePlate m_PressurePlate;
        MV_Speech m_Speech;
        List<Rock> m_Rocks;

        //booleans
        bool PlayerTooHeavy = false;

        //Loads the font
        SpriteFont Arial;

        //constructor FOR ROOM3
        public Testing_room3(ContentManager Content, WindowWidth windowwidth, int windowheight, Player player)
        {
            //Asigns the variables
            m_content = Content;
            m_windowwidth = windowwidth;
            m_windowheight = windowheight;

            //Lists
            m_Rocks = new List<Rock>();

            //sets the spawn location
            player.position = player.GenericSPWN;

            //Loads the font from the font sheet
            Arial = Content.Load<SpriteFont>("Font");

            //Textures
            var texBox = Content.Load<Texture2D>("Textures/Box");
            var texPlate = Content.Load<Texture2D>("Textures/Pressure Plate");
            var texrock = Content.Load<Texture2D>("Textures/Rock");

            //Box
            m_Box = new Box(Content, m_windowwidth, m_windowheight);
            m_Box.LoadContent(texBox, 
                m_Box.SpawnPos);

            //Newspaper
            m_Newspaper = new Newspaper(Content);
            m_Newspaper.LoadContent(m_Newspaper.texNewspaper, 
                new Vector2(0, m_windowheight - m_Newspaper.texNewspaper.Height));

            //pressure plate
            m_PressurePlate = new PressurePlate();
            m_PressurePlate.LoadContent(texPlate,
                new Vector2(m_windowwidth.Val - texPlate.Width * 3, m_windowheight - texPlate.Height * 2));

            //rock
            var rock = new Rock();
            rock.LoadContent(texrock, new Vector2(texPlate.Width, 0));
            m_Rocks.Add(rock);

            //rock
            rock = new Rock();
            rock.LoadContent(texrock, new Vector2(m_windowwidth.Val - texrock.Width *1.9f, texrock.Height));
            m_Rocks.Add(rock);

            //rock
            rock = new Rock();
            rock.LoadContent(texrock, new Vector2(0, 400));
            m_Rocks.Add(rock);

            //Loads the speech class
            m_Speech = new MV_Speech(Content, m_windowwidth, m_windowheight);

            //Chooses which speech to start each level with
            m_Speech.StartTalking(2);
        }

        public void Update(GameTime gameTime, Player player)
        {
            //detects what side the player is coming from and moves box accordingly
            if (player.BoundingBox.Intersects(m_Box.BoundingBox) 
                && m_Box.velocity == Vector2.Zero)
            {
                if (player.velocity.X > 0) m_Box.velocity.X = 10;
                if (player.velocity.X < 0) m_Box.velocity.X = -10;
                if (player.velocity.Y > 0) m_Box.velocity.Y = 10;
                if (player.velocity.Y < 0) m_Box.velocity.Y = -10;

                //stops the player moving
                player.ResetPos();
            }

            //allows the box to move
            m_Box.Move(gameTime);

            //gives the pressure plate a temp bounding box
            Rectangle plateBox = m_Box.BoundingBox;

            //expands size of pressure plate to allow for boxes velocity
            plateBox.Inflate(10,10);

            //loading next room
            if (plateBox.Contains(m_PressurePlate.BoundingBox))
            {
                if (LoadNextRoom != null)
                {
                    m_Box.velocity = Vector2.Zero;
                    LoadNextRoom(new Testing_room4(m_content, m_windowwidth, m_windowheight, player));
                }
            }

            //what happens for each rock in the list
            foreach (var rock in m_Rocks)
            {
                //stops the box from moving
                if(m_Box.BoundingBox.Intersects(rock.BoundingBox))
                {
                    m_Box.position -= m_Box.velocity;
                    m_Box.velocity = Vector2.Zero;
                }

                //stops the player from moving
                if (player.BoundingBox.Intersects(rock.BoundingBox))
                {
                    player.ResetPos();
                }
            }

            //player interacting with pressure plate
            if (player.BoundingBox.Intersects(m_PressurePlate.BoundingBox))
            {
                PlayerTooHeavy = true;
            }
            else { PlayerTooHeavy = false; }

            //Player picking up the newspaper
            if (player.BoundingBox.Intersects(m_Newspaper.BoundingBox))
            {
                m_Newspaper.PickUpPaper(2);
            }

            //updates the newspaper and speech
            m_Newspaper.Update(gameTime);
            m_Speech.Update(gameTime);
        }

        public event Action<IRoom> LoadNextRoom;

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draws every rock in the list
            foreach (var rock in m_Rocks)
            {
                rock.Draw(spriteBatch);
            }

            //draws classes
            m_PressurePlate.Draw(spriteBatch);
            m_Box.Draw(spriteBatch);
            m_Newspaper.Draw(spriteBatch);
            m_Speech.Draw(spriteBatch);

            //tells player they aren't heavy enough to activate the pressure plate
            if (PlayerTooHeavy)
            {
                spriteBatch.DrawString(Arial, "Not enough weight applied", new Vector2(m_PressurePlate.position.X - 100, m_PressurePlate.position.Y - 50), Color.Black);
            }

            //to identify which room you are currently in
            spriteBatch.DrawString(Arial, "Test Room 3", new Vector2(20, 20), Color.Black);
        }
    }
}