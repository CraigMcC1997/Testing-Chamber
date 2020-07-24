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
    class Testing_room : IRoom
    {
        //Info to allow the next room to load
        ContentManager m_content;
        WindowWidth m_windowwidth;
        int m_windowheight;

        //Classes
        Gun m_Gun;
        Button m_Button;
        Bullet m_Bullet;
        Newspaper m_Newspaper;
        MV_Speech m_Speech;
        _666 m_666;
        Limb m_Limb;

        //Loads the font
        SpriteFont Arial;

        //constructor for the first room
        public Testing_room(ContentManager Content, WindowWidth windowwidth, int windowheight, Player player)
        {
            //Asigns the variables
            m_content = Content;
            m_windowwidth = windowwidth;
            m_windowheight = windowheight;

            //sets the spawn location
            player.position = player.GenericSPWN;

            //allows the player to move when room loads
            player.GiveLife();

            //Loads the font from the font sheet
            Arial = Content.Load<SpriteFont>("Font");

            //Textures
            var texBullet = Content.Load<Texture2D>("Bullet");

            //gun
            m_Gun = new Gun(Content, m_windowwidth, m_windowheight);
            m_Gun.LoadContent(m_Gun.texGun, 
                new Vector2(m_windowwidth.Val /2 - m_Gun.texGun.Width /2, m_windowheight /2 - m_Gun.texGun.Height / 2));

            //button
            m_Button = new Button(Content, m_windowwidth);
            m_Button.LoadContent(m_Button.texButton, 
                new Vector2(m_windowwidth.Val/2 - m_Button.texButton.Width /2, 0));

            //bullet
            m_Bullet = new Bullet(Content, m_windowwidth, m_windowheight);
            m_Bullet.LoadContent(texBullet, 
                new Vector2(m_windowwidth.Val - 100));

            //Newspaper
            m_Newspaper = new Newspaper(Content);
            m_Newspaper.LoadContent(m_Newspaper.texNewspaper, 
                new Vector2(0, m_windowheight - m_Newspaper.texNewspaper.Height));

            //666
            m_666 = new _666(Content, m_windowwidth, m_windowheight);
            m_666.LoadContent(m_666.Texture, 
                new Vector2(m_windowwidth.Val - m_windowwidth.Val /4, m_windowheight /6));

            //limb
            m_Limb = new Limb(Content, m_windowwidth, m_windowheight);
            m_Limb.LoadContent(m_Limb.Texture, 
                new Vector2(m_windowwidth.Val - m_windowwidth.Val / 5, m_windowheight / 4));

            //Loads the speech class
            m_Speech = new MV_Speech(Content, m_windowwidth, m_windowheight);

            //Chooses which speech to start each level with
            m_Speech.StartTalking(0);
        }

        public void Update(GameTime gameTime, Player player)
        {
            //LOADING NEXT ROOM
            if (m_Bullet.BoundingBox.Intersects(m_Button.BoundingBox))
            {
                if (m_loadNextRoom != null)
                {
                    m_Button.PlaySound();
                    m_loadNextRoom(new Testing_room2(m_content, m_windowwidth, m_windowheight, player));
                }
            }

            //Gun tracks the players position
            if (player.BoundingBox.Intersects(m_Gun.BoundingBox))
            {
                m_Gun.position = new Vector2
                (player.position.X + m_Gun.Texture.Width, player.position.Y - m_Gun.Texture.Height / 3);
            }

            //shooting the bullet
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) 
                && player.BoundingBox.Intersects(m_Gun.BoundingBox) && !m_Bullet.ToggleBulletMove)
            {
                m_Bullet.ShootBullet(m_Gun);
            }

            //allows the gun firing sound to play again without looping
            if (Keyboard.GetState().IsKeyUp(Keys.Enter) && m_Bullet.ToggleBulletMove)
            {
                m_Bullet.PlayAgain();
            }

            //whether or not the bullet is moving on screen
            if (m_Bullet.ToggleBulletMove)
            {
                m_Bullet.Move(gameTime);
            }

            //Player picking up the newspaper
            if (player.BoundingBox.Intersects(m_Newspaper.BoundingBox))
            {
                m_Newspaper.PickUpPaper(0);
            }

            //updates the newspaper and speech
            m_Newspaper.Update(gameTime);
            m_Speech.Update(gameTime);
            m_Bullet.Update(gameTime);
        }


        //Loading the next possible rooms
        private Action<IRoom> m_loadNextRoom;

        //adds to the value to allow the rooms to change
        public event Action<IRoom> LoadNextRoom
        {
            add { m_loadNextRoom += value; }
            remove { m_loadNextRoom -= value; }
        }

        //Draws everything in the class
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draws each class
            m_666.Draw(spriteBatch);
            m_Limb.Draw(spriteBatch);
            m_Bullet.Draw(spriteBatch);
            m_Gun.Draw(spriteBatch);
            m_Button.Draw(spriteBatch);
            m_Newspaper.Draw(spriteBatch);
            m_Speech.Draw(spriteBatch);

            //to identify which room you are currently in
            spriteBatch.DrawString(Arial, "Test Room 1", new Vector2(20, 20), Color.Black);
        }
    }
}