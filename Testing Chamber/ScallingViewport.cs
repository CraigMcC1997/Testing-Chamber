using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Testing_Chamber
{
    // simplest way to solve resolution independence in MonoGame is to create a scaling matrix 
    // that can be passed into each SpriteBatch.Begin call. The idea is to pick a virtual resolution 
    // and scale everything to fit into that box. When you're coding your game, you'll always work with the virtual resolution.
    public sealed class ScalingViewport : BaseViewport
    {
        private readonly int m_virtualWidth;
        private readonly int m_virtualHeight;

        public ScalingViewport(GraphicsDevice graphicsDevice, int virtualWidth, int virtualHeight)
            : base(graphicsDevice)
        {
            m_virtualWidth = virtualWidth;
            m_virtualHeight = virtualHeight;
        }

        public override int VirtualWidth
        {
            get { return m_virtualWidth; }
        }

        public override int VirtualHeight
        {
            get { return m_virtualHeight; }
        }

        public override int ViewportWidth
        {
            get { return GraphicsDevice.Viewport.Width; }
        }

        public override int ViewportHeight
        {
            get { return GraphicsDevice.Viewport.Height; }
        }

        public override Matrix GetScaleMatrix()
        {
            // Calculates the scale value and returns a matrix with the new scale
            // For example, let's say your virtual resolution is 800x480 and the actual resolution of the device is 1024x768. 
            // Using the calculations below, the final image would be scaled 1.28 times horizontally and 1.6 times vertically.
            var scaleX = (float)ViewportWidth / VirtualWidth;
            var scaleY = (float)ViewportHeight / VirtualHeight;
            return Matrix.CreateScale(scaleX, scaleY, 1.0f);
        }
    }
}
