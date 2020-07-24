using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Chamber
{
    class AnimatedBlood : AnimatedSprite
    {
        public AnimatedBlood(Texture2D texture)
        {
            //only one animation is needed for this, 
            //but allows for more from the same spritesheet
            AddAnimation("Blood", new Animation(texture, 6, 0, 5, 0.2f, true));
        }
    }
}
