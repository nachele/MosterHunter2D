using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame
{
    internal class Entity
    {
        protected Texture2D Texture;
        protected Vector2 size;
        protected Vector2 position;
        protected int posx;
        protected int posy;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
    }
}
