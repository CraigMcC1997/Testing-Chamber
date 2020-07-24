using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Testing_Chamber
{
    class Explosion : Sprite
    {
        protected override Vector2 PerformMove(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        // random number generator(each class shares the same one)
        private static Random mRand = new Random();

        //texture
        public Texture2D texExplosion;

        //if explosion is alive or not
        private bool mActive = true;

        // life support for the bunnies
        const int TOTAL_LIFE_MILLISECONDS = 300;
        int mElapsedLifeMilliseconds = 0;

        public Explosion(Texture2D sprite, int screenWidth, int screenHeight)
        {
            //load explosions sprites for efficiency
            texExplosion = sprite;
            position = new Vector2((float)mRand.Next(0, screenWidth), (float)mRand.Next(0, screenHeight));
        }

        public bool Active
        {
            get { return mActive; }
            set { mActive = value; }
        }

        public void Update(GameTime gameTime)
        {
            if (mActive)
            {
                // check for death
                mElapsedLifeMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
                if (mElapsedLifeMilliseconds >= TOTAL_LIFE_MILLISECONDS)
                {
                    Active = false;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (mActive)
            {
                spriteBatch.Draw(texExplosion, position, Color.White);
            }
        }
    }
}
