using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Chamber
{
    class AnimatedSkull : AnimatedSprite
    {
        public AnimatedSkull(Texture2D texture)
        {
            //only one animation is needed for this, 
            //but allows for more from the same spritesheet
            AddAnimation("Skull", new Animation(texture, 7, 0, 6, 0.1f, true));
        }
    }
}
