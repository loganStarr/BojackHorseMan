using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello
{
    class Sprite
    {
        public Vector2 Postion;
        public Vector2 Size;
        public Texture2D Image;
        
        public Sprite(Texture2D i, Vector2 s, Vector2 p)
        {
            Size = s;
            Image = i;
            Postion = p;
        }
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Image,new Rectangle((int)Postion.X,(int)Postion.Y,(int)Size.X,(int)Size.X),Color.White);
        }
    }

}
