using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Testing_Chamber
{
    public class AnimatedSprite
    {
        private Animation m_currentAnimation;
        private readonly Dictionary<string, Animation> m_animations = new Dictionary<string, Animation>();
        public Vector2 Position { get; set; }

        protected void AddAnimation(string name, Animation anim)
        {
            if (!m_animations.ContainsKey(name))
            {
                m_animations.Add(name, anim);
            }
        }

        public void PlayAnimation(string name)
        {
            if (m_animations.ContainsKey(name))
            {
                m_currentAnimation = m_animations[name];
            }
        }

        public void Update(GameTime gameTime)
        {
            if (m_currentAnimation != null)
            {
                m_currentAnimation.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (m_currentAnimation != null)
            {
                m_currentAnimation.Draw(Position, spriteBatch);
            }
        }
    }
}
