using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testing_Chamber
{
    interface ISprite
    {
        //Creates functions to be used by the "Sprite" class
        void LoadContent(Texture2D texture, Vector2 startPosition);

        void Draw(SpriteBatch spritebatch);

        Rectangle BoundingBox { get; }
    }
}

