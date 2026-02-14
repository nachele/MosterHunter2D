using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MonoGame 
{
    internal static class Menu
    {
        #region propiedades
        static public bool active = true;
        static private Vector2 playButton = new Vector2(100,100);
        static private Texture2D texture;
        static private Vector2 size = new Vector2(100, 100);
        #endregion

        #region metodos
        static public void Load(ContentManager content, string imageName) //carga la imagen.
        {
            texture = content.Load<Texture2D>(imageName);
        }//load();
        static public void Draw(SpriteBatch _spriteBatch) //dibuja la imagen;
        {
            if (active)
            {
                _spriteBatch.Draw(texture, new Rectangle((int)playButton.X, (int)playButton.Y, (int)size.X, (int)size.Y), Microsoft.Xna.Framework.Color.White); // Pintar imagen
            }
        }//Draw();
        static public void Update()
        {

        }//state();
        #endregion
    }
}
