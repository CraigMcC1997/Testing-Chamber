using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Testing_Chamber
{
    public class Animation
    {
        private Texture2D m_texture;
        private readonly int m_totalNumberOfFrames;
        private readonly int m_startFrame;
        private readonly int m_endFrame;
        private readonly float m_timePerFrame;
        private int m_currentFrame;
        private float m_timeElapsed;
        private readonly bool m_repeat;

        public Animation(Texture2D texture, int totalNumberOfFrames, int frameStart, int frameEnd, float timePerFrame, bool repeat)
        {
            m_texture = texture;
            m_totalNumberOfFrames = totalNumberOfFrames;
            m_startFrame = frameStart;
            m_endFrame = frameEnd;
            m_timePerFrame = timePerFrame;
            m_currentFrame = frameStart;
            m_timeElapsed = 0;
            m_repeat = repeat;
        }

        public void Update(GameTime gameTime)
        {
            m_timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (m_timeElapsed > m_timePerFrame)
            {
                m_currentFrame++;

                if (m_currentFrame > m_endFrame)
                {
                    if (m_repeat)
                    {
                        m_currentFrame = m_startFrame;
                    }
                    else
                    {
                        m_currentFrame = m_endFrame;
                    }
                }
                m_timeElapsed -= m_timePerFrame;
            }
        }

        public void Draw(Vector2 position, SpriteBatch spriteBatch)
        {
            var frameWidth = m_texture.Width / m_totalNumberOfFrames;

            // TODO the Y component of sourceRect will have to be changed for spritesheets
            // with both rows and columns. The 0 represents the Y coordinate of the top left corner
            // of the spritesheet. So for each row this will have to incremented by frame height. The 
            // m_texture.Height will also have to be changed to frame height
            // This is an exercise for you to do however
            var sourcerect = new Rectangle(frameWidth * m_currentFrame, 0,
                frameWidth, m_texture.Height);
            spriteBatch.Draw(m_texture, position, sourcerect, Color.White);
        }
    }
}
