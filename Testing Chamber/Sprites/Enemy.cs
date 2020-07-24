using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Testing_Chamber
{
    class Enemy : Sprite
    {
        //Info to allow the next room to load
        ContentManager m_content;
        WindowWidth m_windowwidth;
        int m_windowheight;
        Player target;

        //used for when animation ends
        bool IsAnimationOver = false;

        //animation
        public ExplosionAnimation anim_Explode;

        //Give enemy health
        public int Health = 1000;
        private int m_DMG = 100;

        internal Player Target
        {
            get
            {
                return target;
            }

            set
            {
                target = value;
            }
        }

        //constructor FOR ROOM2
        public Enemy(ContentManager Content, WindowWidth windowwidth, int windowheight)
        {
            //Asigns the variables
            m_content = Content;
            m_windowwidth = windowwidth;
            m_windowheight = windowheight;

            var ExplodeAnimation = Content.Load<Texture2D>("Textures/ExplosionAnim");

            //animated explostion
            anim_Explode = new ExplosionAnimation(ExplodeAnimation);
        }

        public void Update(GameTime gameTime)
        {
            //moves enemy off screen after death animation plays
            if (IsAnimationOver)
            {
                position = new Vector2(m_windowwidth.Val * 2, m_windowheight * 2);
            }

            anim_Explode.Update(gameTime);
        }

        //enemy taking damage
        public void TakeDMG()
        {
            //lowering the enemies health
            Health -= m_DMG;

            //kills enemy if health is below 0
            if (Health <= 0)
            {
                KillEnemy();
            }
        }

        //enemy dying
        public void KillEnemy()
        {
            //no health
            Health = 0;

            anim_Explode.Position = new Vector2(position.X + 125, position.Y + 200);
            anim_Explode.PlayAnimation("Explode");

            IsAnimationOver = true;
        }

        protected override Vector2 PerformMove(GameTime gameTime)
        {
            //gives the enemy a velocity equal to 0
            Vector2 velocity = Vector2.Zero;

            //works out how far the player is to the button
            Vector2 distanceToButton;
            distanceToButton = new Vector2(m_windowwidth.Val/2, 0) - target.position;

            //if player is within the range stated then move the enemy towards the player
            if (Health > 0)
            {
                if (distanceToButton.Length() < m_windowheight / 1.5f)
                {
                    velocity = new Vector2
                        (target.position.X - position.X - Texture.Width / 2 + target.Texture.Width / 2, 0);
                    velocity *= 0.1f;
                }
            }
            return velocity;
        }
    }
}
