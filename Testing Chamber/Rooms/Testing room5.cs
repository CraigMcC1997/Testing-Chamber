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
    class Testing_room5 : IRoom
    {
        //Info to allow the next room to load
        ContentManager m_content;
        WindowWidth m_windowwidth;
        int m_windowheight;

        //Loads the font
        SpriteFont Arial;

        //textures
        Button m_Button;
        List<Arrow> m_Arrows;
        Newspaper m_Newspaper;
        MV_Speech m_Speech;

        //constructor FOR ROOM5
        public Testing_room5(ContentManager Content, WindowWidth windowwidth, int windowheight, Player player)
        {
            //Asigns the variables
            m_content = Content;
            m_windowwidth = windowwidth;
            m_windowheight = windowheight;

            //sets the spawn location
            player.position = player.GenericSPWN;

            //Loads the font from the font sheet
            Arial = Content.Load<SpriteFont>("Font");

            //loads the list of arrows
            m_Arrows = new List<Arrow>();

            //Arrow1
            var arrowTex = Content.Load<Texture2D>("Textures/Arrow");

            var arrow = new Arrow(m_windowwidth);
            arrow.LoadContent(arrowTex, new Vector2(0, 0));
            m_Arrows.Add(arrow);

            arrow = new Arrow(m_windowwidth);
            arrow.LoadContent(arrowTex, 
                new Vector2(m_windowwidth.Val, m_windowheight / 2 - arrowTex.Width));
            m_Arrows.Add(arrow);

            arrow = new Arrow(m_windowwidth);
            arrow.LoadContent(arrowTex, new 
                Vector2(m_windowwidth.Val / 2 - arrowTex.Width / 2, m_windowheight / 2));
            m_Arrows.Add(arrow);

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
            m_Speech.StartTalking(4);
        }

        public void Update(GameTime gameTime, Player player)
        {
            //updates arrows
            foreach (var arrow in m_Arrows)
            {
                //move the arrows
                arrow.Move(gameTime);

                //killing player when they interact
                if (player.BoundingBox.Intersects(arrow.BoundingBox))
                {
                    player.position = player.GenericSPWN;
                }
            }

            //loading next room
            if (player.BoundingBox.Intersects(m_Button.BoundingBox))
            {
                if (LoadNextRoom != null)
                {
                    LoadNextRoom(new Testing_room6(m_content, m_windowwidth, m_windowheight, player));
                }
            }

            //Player picking up the newspaper
            if (player.BoundingBox.Intersects(m_Newspaper.BoundingBox))
            {
                m_Newspaper.PickUpPaper(4);
            }

            //updates the newspaper and speech
            m_Newspaper.Update(gameTime);
            m_Speech.Update(gameTime);
        }

        public event Action<IRoom> LoadNextRoom;

        public void Draw(SpriteBatch spriteBatch)
        {
            //textures
            m_Button.Draw(spriteBatch);

            foreach (var arrow in m_Arrows)
            {
                arrow.Draw(spriteBatch);
            }

            m_Newspaper.Draw(spriteBatch);
            m_Speech.Draw(spriteBatch);

            //to identify which room you are currently in
            spriteBatch.DrawString(Arial, "Test Room 5", new Vector2(20, 20), Color.Black);
        }
    }
}
