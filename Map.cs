using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.IO;


namespace MonoGame
{
    enum TextureTipes
    {
        hierba,
        casa,
    }
    internal static class Map
    {
        #region propiedades
        private static string[] map;//array de una dimension con los caracteres.
        private static string[,]map2d;//array de dos dimensiones con los caracteres.
        private static Entity[,] mapEntities;
        private static Entity[,] EntitysTexture2D;
        private static int row = 5; //filas de los arrays 2d
        private static int col = 5; //columnas de los arrays 2d
        private static int TextureWidth; //ancho texturas
        private static int TextureHeight; //alto texturas
        private static Vector2 size; //vector ancho alto texturas.
        private static List<(int, int)> HousesIndex = new List<(int, int)>();
        #endregion
        
        #region metodos
        public static string FileTo1DArray(string path) //lee el fichero de texto y devuevle un string con los caracteres y comas.
        {
            
            string file;
            using(StreamReader sr = new StreamReader(path))
            {

                file = sr.ReadToEnd();   
            }
            return file;
        }//FileToString1D();

        public static void StringTo2DArray(string file) //combierte el string con caracteres del mapa en un array de 1 dimension y otro de 2 dimensiones. 
        {
            map = file.Split(',');
            map2d = new string[row,col];
            int indexX = 0;
            int indexY = 0;
            foreach(string s in map)
            {
                if(s != "h" && s != "c")
                {
                    ++indexY;
                    
                    indexX = 0;
                }
                else
                {

                    map2d[indexY,indexX] = s;
                    ++indexX;
                }
            }
            
        }//StringTo2DArray();
        
        public static void MapEntitines(ContentManager content)
        {
            EntitysTexture2D = new Entity[row,col];
            for (int i = 0; i < row; i++)
            {
                for(int x = 0; x < col; x++)
                {

                    EntitysTexture2D[i, x] = new Entity(content, map2d[x, i] + ".png", x * 100, i * 100, new Vector2(100, 100), 1);
                }
            }
        }

        public static void DrawUpdate(SpriteBatch _spriteBatch)
        {

            for (int i = 0; i < row; i++)
            {
                for (int x = 0; x < col; x++)
                {
                    _spriteBatch.Draw(EntitysTexture2D[i, x].TEXTURE, new Rectangle(((int)EntitysTexture2D[i, x].Posx), ((int)EntitysTexture2D[i, x].Posy), ((int)(EntitysTexture2D[i, x].SIZE.X)), (int)(EntitysTexture2D[i, x].SIZE.Y)), Color.White); // Pintar imagen
                }
            }
        }
        public static void Movement()
        {
            for (int i = 0; i < row; ++i)
            {
                for (int x = 0; x < col; x++)
                {
                    EntitysTexture2D[i, x].Posy -= 1;
                }
            }

        }
        #endregion


    }//Map.
}
