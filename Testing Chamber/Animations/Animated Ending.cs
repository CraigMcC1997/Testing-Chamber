using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Chamber
{
    class Animated_Ending : AnimatedSprite
    {
        public Animated_Ending(Texture2D texture)
        {
            //only one animation is needed for this, 
            //but allows for more from the same spritesheet
            AddAnimation("End", new Animation(texture, 15, 0, 14, 0.2f, false));

            //talking animation
            AddAnimation("Talk", new Animation(texture, 9, 0, 8, 0.2f, true));
        } 
    }
}
