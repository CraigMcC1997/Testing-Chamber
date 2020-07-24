using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Chamber
{
    abstract class Sprite : ISprite
    {
        public Texture2D Texture;
        private Vector2 Position;
        private Vector2 Velocity;

        //moving the sprite
        public Vector2 position
        {
            get
            {
                return Position;
            }

            set
            {
                Position = value;
            }
        }

        //moving the sprite
        public void Move(GameTime gameTime)
        {
            Velocity = PerformMove(gameTime);

            Position += Velocity;
        }

        //moving the sprite
        protected abstract Vector2 PerformMove(GameTime gameTime);

        //drawing the sprite
        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Position, Color.White);
        }

        //loading the info of each sprite
        public void LoadContent(Texture2D texture, Vector2 startPosition)
        {
            Texture = texture;
            Position = startPosition;
        }

        //stops a sprite moving whenever this method is called
        public void ResetPos()
        {
            Position -= Velocity;
        }

        //creates a hitbox that surrounds each sprites rectangular position
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }
    }
}
