using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace Testing_Chamber
{
    class Testing_room8 : IRoom
    {
        //Info to allow the next room to load
        ContentManager m_content;
        WindowWidth m_windowwidth;
        int m_windowheight;
        private GraphicsDeviceManager m_Graphics;

        //textures
        Button m_Button;
        Enemy m_Enemy;
        Newspaper m_Newspaper;
        Gun m_Gun;
        Bullet m_Bullet;
        EnemyBullet m_EnemyBullet;
        MV_Speech m_Speech;

        //Loads the font
        SpriteFont Arial;

        //constructor FOR ROOM2
        public Testing_room8(ContentManager Content, WindowWidth windowwidth, int windowheight, Player player)
        {
            //Asigns the variables
            m_content = Content;
            m_windowwidth = windowwidth;
            m_windowheight = windowheight;

            //Loads the font from the font sheet
            Arial = Content.Load<SpriteFont>("Font");

            //sets the players position to the rooms spawn loaction
            player.position = player.GenericSPWN;

            //Textures
            var texEnemy = Content.Load<Texture2D>("Enemy");
            var texBullet = Content.Load<Texture2D>("Bullet");

            //enemy
            m_Enemy = new Enemy(Content, m_windowwidth, m_windowheight);
            m_Enemy.LoadContent(texEnemy, 
                new Vector2(m_windowwidth.Val - texEnemy.Width, 0));

            //gun
            m_Gun = new Gun(Content, m_windowwidth, m_windowheight);
            m_Gun.LoadContent(m_Gun.texGun, player.position);
            
            //bullet
            m_Bullet = new Bullet(Content, m_windowwidth, m_windowheight);
            m_Bullet.LoadContent(texBullet,
                new Vector2(m_windowwidth.Val - 100));

            //enemy bullet
            m_EnemyBullet = new EnemyBullet(Content, m_windowwidth, m_windowheight);
            m_EnemyBullet.LoadContent(texBullet,
                new Vector2(m_windowwidth.Val - 100));

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
            m_Speech.StartTalking(7);
        }

        public void Update(GameTime gameTime, Player player)
        {
            //if players bullet interacts with enemy
            if (m_Bullet.BoundingBox.Intersects(m_Enemy.BoundingBox))
            {
                //enemy takes damage
                m_Enemy.TakeDMG();

                //reset bullet to be fired from gun again
                m_Bullet.KillBullet();
            }

            //Player picking up the newspaper
            if (player.BoundingBox.Intersects(m_Newspaper.BoundingBox))
            {
                m_Newspaper.PickUpPaper(7);
            }

            //enemy tracks players position
            m_Enemy.Target = player;

            //allows enemy to move
            m_Enemy.Move(gameTime);

            //Gun tracks players position
            m_Gun.position = new Vector2
                (player.position.X + m_Gun.Texture.Width, player.position.Y - m_Gun.Texture.Height/3);

            //shooting the bullet
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)
                && player.BoundingBox.Intersects(m_Gun.BoundingBox) 
                && !m_Bullet.ToggleBulletMove)
            {
                m_Bullet.ShootBullet(m_Gun);
            }

            //enemy shooting player if player is under enemy
            if (player.position.X > m_Enemy.position.X 
                && player.position.X < m_Enemy.position.X + m_Enemy.Texture.Width
                && !m_EnemyBullet.ToggleBulletMove)
            {
                m_EnemyBullet.ShootBullet(m_Enemy);
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

            //whether or not the enemy bullet is moving
            if (m_EnemyBullet.ToggleBulletMove)
            {
                m_EnemyBullet.Move(gameTime);
            }

            //"killing" player if interacts with enemy
            if (player.BoundingBox.Intersects(m_Enemy.BoundingBox))
            {
                player.position = player.GenericSPWN;
            }

            //bullet interacting with player
            if (m_EnemyBullet.BoundingBox.Intersects(player.BoundingBox))
            {
                //reseting the players position
                player.position = player.GenericSPWN;

                //reseting bullet to be shot again
                m_EnemyBullet.KillBullet();
            }

            //if player interacts with the button, play ending animation 
            if (m_Bullet.BoundingBox.Intersects(m_Button.BoundingBox) && m_Enemy.Health <= 0)
            {
                if (LoadNextRoom != null)
                {
                    LoadNextRoom(new Final_Room(m_content, m_windowwidth, m_windowheight, m_Graphics));
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
            m_Bullet.Update(gameTime);
            m_EnemyBullet.Update(gameTime);
            m_Enemy.anim_Explode.Update(gameTime);
            m_Enemy.Update(gameTime);
        }

        public event Action<IRoom> LoadNextRoom;

        public void Draw(SpriteBatch spriteBatch)
        {
          
            //textures
            m_Button.Draw(spriteBatch);
            m_Bullet.Draw(spriteBatch);
            m_Enemy.Draw(spriteBatch);
            m_Enemy.anim_Explode.Draw(spriteBatch);
            m_EnemyBullet.Draw(spriteBatch);
            m_Gun.Draw(spriteBatch);
            m_Newspaper.Draw(spriteBatch);
            m_Speech.Draw(spriteBatch);

            //to identify which room you are currently in
            spriteBatch.DrawString(Arial, "Test Room 8", new Vector2(20, 20), Color.Black);
        }
    }
}
