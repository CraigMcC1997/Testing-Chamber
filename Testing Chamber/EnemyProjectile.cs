using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Testing_Chamber
{
    class EnemyBullet : Sprite
    {
        private WindowWidth m_WindowWidth;
        private int m_WindowHeight;

        //sets whether or not the bullet is moving
        public bool ToggleBulletMove = false;

        //bool
        bool m_hasPlayed = false;

        //Sound effects
        private SoundEffectInstance Sound_FireGun;

        public EnemyBullet(ContentManager Content, WindowWidth WindowWidth, int WindowHeight)
        {
            m_WindowWidth = WindowWidth;
            m_WindowHeight = WindowHeight;

            //sound file
            Sound_FireGun = Content.Load<SoundEffect>("Sound Files/Shoot").CreateInstance();
        }

        //Sound plays when gun fires
        public void PlaySound()
        {
            if (Sound_FireGun.State != SoundState.Playing && !m_hasPlayed)
            {
                Sound_FireGun.Play();
                m_hasPlayed = true;
            }
        }

        //allows sound to play again
        public void PlayAgain()
        {
            m_hasPlayed = false;
        }

        public void Update(GameTime gameTime)
        {
            //kills bullet if it leaves the screen
            if (position.Y >= m_WindowHeight + Texture.Height)
            {
                KillBullet();
            }
        }

        public void ShootBullet(Enemy m_Enemy)
        {
            //Gives the bullet a position
            position = new Vector2
                 (m_Enemy.position.X + m_Enemy.Texture.Width / 2 - Texture.Width / 2, m_Enemy.position.Y);

            //allows bullet to move
            ToggleBulletMove = true;

            //plays a sound when the gun fires
            PlaySound();
        }

        //method for stopping bullet moving
        public void KillBullet()
        {
            //set pos outside game window
            position = new Vector2(m_WindowWidth.Val * 2, m_WindowHeight * 2);

            //making bool for moving false
            ToggleBulletMove = false;
        }

        protected override Vector2 PerformMove(GameTime gameTime)
        {
            //gives bullet a velocity
            Vector2 velocity;

            //moves the bullet on the Y axis
            velocity = new Vector2(0, 15);

            return velocity;
        }
    }
}
