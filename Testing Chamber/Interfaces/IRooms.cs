using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Chamber
{
    interface IRoom
    {
        //Gives every room an Update and a Draw
        void Update(GameTime gameTime, Player player);
        void Draw(SpriteBatch spriteBatch);

        //for use when moving rooms
        event Action<IRoom> LoadNextRoom;
    }
}
