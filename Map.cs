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

    internal static class Map
    {
        #region propiedades
        private static string[] map;//array de una dimension con los caracteres.
        private static string[,]map2d;//array de dos dimensiones con los caracteres.
        private static Texture2D[,] map2dImages;//Array de imagenes
        private static MainCharacter[,] EntitysTexture2D;
        private static int row = 5; //filas de los arrays 2d
        private static int col = 5; //columnas de los arrays 2d
        private static int TextureWidth; //ancho texturas
        private static int TextureHeight; //alto texturas
        private static Vector2 size; //vector ancho alto texturas.
        private static List<(int, int)> HousesIndex = new List<(int, int)>();

        #endregion
        
        #region metodos
        private static void Inizilazed()
        {

        }
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

        public static void TextureLoad(ContentManager content, string imagePath)// introduce en el array map2dImages las imagenes si es "h" hierba.jpg si es "c" cabaña.jpg.
        {

            map2dImages = new Texture2D[row,col]; //inicializando array
            for(int i = 0; i < row; i++)//cambiando el path de la imagen dependiendo del caracter encontrado en el map 
            {
                for(int x = 0; x < col; x++)
                {
                    if (map2d[i,x] == "h")
                    {

                        imagePath = "h.png";

                    }
                    else if(map2d[i,x] == "c")
                    {

                        imagePath = "c.png";

                    }
                  
                    map2dImages[i, x] = content.Load<Texture2D>(imagePath); //introduciendo la imagen en la posicion correspondiente

                    TextureWidth = map2dImages[i,x].Width; //obteniendo ancho de la imagen
                    TextureHeight = map2dImages[i,x].Height;//obteniendo alto de la imagen.

                    size.X = TextureWidth;  //ancho de la textura a vector size.
                    size.Y = TextureHeight; //alto de la textura a vector height.
                }
            }          
        }//TextureLoad();

        public static void EntitysTexture2DLoad() 
        {
            int posY;
            int posX = 0;
            EntitysTexture2D = new MainCharacter[row,col];
            for (int i = 0; i < row; i++) 
            {
                for (int x = 0; x < col; x++)
                {
                    EntitysTexture2D[i, x].TEXTURE = map2dImages[i, x];   
                    EntitysTexture2D[i, x].Position = Vector2(i * 100, x * 100) ;
                }
            }
        }

        private static Vector2 Vector2(int v1, int v2)
        {
            throw new NotImplementedException();
        }

        public static  void Draw(SpriteBatch _spriteBatch) //dibuja las imagenes del mapa.
        {

            for(int i = 0; i < row; i++)
            {
                for(int x = 0; x < col; x++)
                {
                    if (map2d[i,x] == "h")
                    {
                        _spriteBatch.Draw(map2dImages[i, x], new Rectangle((i * 100), (x * 100), (int)size.X, (int)size.Y), Color.White); // Pintar imagen
                    }else if (map2d[i,x] == "c")
                    {
                        _spriteBatch.Draw(map2dImages[i, x - 1], new Rectangle((i * 100), (x * 100), (int)size.X, (int)size.Y), Color.White); // Pintar imagen

                        HousesIndex.Add((i, x));
                    }
                }
            }
            _spriteBatch.Draw(map2dImages[HousesIndex[0].Item1, HousesIndex[0].Item2], new Rectangle((HousesIndex[0].Item1 * 100), (HousesIndex[0].Item2 * 100), 400, 400), Color.White); // Pintar imagen

        }//Draw(); 

       
        #endregion


    }//Map.
}
