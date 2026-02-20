using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


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
        private static int row; //filas de los arrays 2d
        private static int col; //columnas de los arrays 2d
        private static int TextureWidth; //renderWidth texturas
        private static int TextureHeight; //alto texturas
        private static Vector2 size; //vector renderWidth alto texturas.
        private static List<(int, int)> HousesIndex = new List<(int, int)>();
        private static int OfsetXstart = -200;
        private static int OfsetYstart = -200;
        private static bool init;
        private static int renderWidth;
        public static int OfsetXIndex { get; private set; }
        public static int OfsetIndex { get; private set; }
        public static int PlayerPosY { get; private set; }
        public static int PlayerPosX { get; private set; }
        private static int topRenderY;
        public static int RightRenderX { get; private set; }
        private static int bottomRenderYInit;
        public static int LeftRenderX { get; private set; }

        private static int bottomRenderY;
        private static int mapInitposx;
        private static int deltaRenderY;

        #endregion

        #region metodos
        public static string FileTo1DArray(string path) //Lee el fichero de texto y devuevle un string con los caracteres y comas.
        {
            bool initCol = false;
            string file = "";
            using(StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    file += sr.ReadLine();
                    if (!initCol)
                    {
                        col = (file.Split(',').Length );
                        initCol = true;
                    }
                    ++row;
                }
                   
            }
            return file;
        }//FileToString1D();
        public static void StringTo2DArray(string file) //Combierte el string con caracteres del mapa en un array de 1 dimension y otro de 2 dimensiones. 
        {
            map = file.Split(',');
            map2d = new string[row,col];
            int indexX = 0;
            int indexY = 0;
            foreach(string s in map)
            {
                if(s != "h" && s != "c" && s != "a" && s != "w" && s != "b")
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
        public static void MapEntities(ContentManager content) // Crea los objetos con la imagen correspondiente y la posicion correspondiente.
        {

            EntitysTexture2D = new Entity[row,col];
            
            for (int i = 0; i < row; i++)
            {
                for(int x = 0; x < col - 2; x++)
                { 
                  
                    if(map2d[i, x] == "h")
                    {
                        EntitysTexture2D[i, x] = new Entity(content, map2d[i, x] + ".png", (OfsetXstart + x * 100), (OfsetYstart + i * 100), new Vector2(100, 100), 1);
                    }
                    else if (map2d[i,x] == "c")
                    {
                        EntitysTexture2D[i, x] = new Entity(content, map2d[i, x] + ".png", (OfsetXstart - 300 + x * 100), (OfsetYstart  - 300 + i * 100), new Vector2(600, 600), 1);
                    }
                    else if (map2d[i, x] == "a")
                    {
                        EntitysTexture2D[i, x] = new Entity(content, map2d[i, x] + ".png", (OfsetXstart + x * 100), (OfsetYstart + i * 100), new Vector2(100, 100), 1);
                    }
                    else if (map2d[i, x] == "w")
                    {
                        EntitysTexture2D[i, x] = new Entity(content, map2d[i, x] + ".png", (OfsetXstart + x * 100), (OfsetYstart + i * 100), new Vector2(100, 100), 1);
                    }
                    else if (map2d[i, x] == "b")
                    {
                        EntitysTexture2D[i, x] = new Entity(content, map2d[i, x] + ".png", (OfsetXstart + x * 100), (OfsetYstart + i * 100), new Vector2(300, 300), 1);
                    }
                }
            }
        }//MapEntities();

        public static void Draw(MainCharacter Player, SpriteBatch _spriteBatch, int width, int heigth)
        {
            for (int i = 0; i < row - 1; i++)
            {
                for (int x = 0; x < col - 2; x++)
                {
                    _spriteBatch.Draw(
                            EntitysTexture2D[0, 0].TEXTURE,
                            new Rectangle(
                                ((int)EntitysTexture2D[0, 0].Posx + OfsetXstart + x * 100),
                                ((int)EntitysTexture2D[0, 0].Posy + OfsetYstart + i * 100),
                                (int)EntitysTexture2D[0, 0].SIZE.X,
                                (int)EntitysTexture2D[0, 0].SIZE.Y
                            ),
                            Color.White
                        );

                    if (EntitysTexture2D[i, x].Posy > Player.Posy - heigth && EntitysTexture2D[i, x].Posy < Player.Posy + (int)Player.SIZE.Y + heigth &&EntitysTexture2D[i, x].Posx > Player.Posx - width &&EntitysTexture2D[i, x].Posx < Player.Posx + (int)Player.SIZE.X + width)
                    {
                        if (map2d[i, x] == "h" || map2d[i, x] == "a" || map2d[i,x] == "w")
                        {
                            _spriteBatch.Draw(
                            EntitysTexture2D[i, x].TEXTURE,
                            new Rectangle(
                                ((int)EntitysTexture2D[i, x].Posx + OfsetXstart),
                                ((int)EntitysTexture2D[i, x].Posy + OfsetYstart) ,
                                (int)EntitysTexture2D[i, x].SIZE.X,
                                (int)EntitysTexture2D[i, x].SIZE.Y
                            ),
                            Color.White
                        );
                        }

                    }
                }
            }
            for (int i = 0; i < row - 1; i++)
            {
                for (int x = 0; x < col - 2; x++)
                {

                    if (
                        EntitysTexture2D[i, x].Posy > Player.Posy - heigth &&
                        EntitysTexture2D[i, x].Posy < Player.Posy + (int)Player.SIZE.Y + heigth &&

                        EntitysTexture2D[i, x].Posx > Player.Posx - width &&
                        EntitysTexture2D[i, x].Posx < Player.Posx + (int)Player.SIZE.X + width
                       )
                    {
                        if (map2d[i, x] == "c")
                        {
                            _spriteBatch.Draw(
                            EntitysTexture2D[i, x].TEXTURE,
                            new Rectangle(
                                ((int)EntitysTexture2D[i, x].Posx + OfsetXstart) ,
                                ((int)EntitysTexture2D[i, x].Posy + OfsetYstart) ,
                                (int)EntitysTexture2D[i, x].SIZE.X,
                                (int)EntitysTexture2D[i, x].SIZE.Y
                            ),
                            Color.White
                        );
                        }
                            
                    }
                }
            }

            Player.Draw(_spriteBatch);
            for (int i = 0; i < row - 1; i++)
            {
                for (int x = 0; x < col - 2; x++)
                {

                    if (
                        EntitysTexture2D[i, x].Posy > Player.Posy - heigth &&
                        EntitysTexture2D[i, x].Posy < Player.Posy + (int)Player.SIZE.Y + heigth &&

                        EntitysTexture2D[i, x].Posx > Player.Posx - width &&
                        EntitysTexture2D[i, x].Posx < Player.Posx + (int)Player.SIZE.X + width
                       )
                    {
                        if (map2d[i, x] == "b")
                        {
                            _spriteBatch.Draw(
                            EntitysTexture2D[i, x].TEXTURE,
                            new Rectangle(
                                ((int)EntitysTexture2D[i, x].Posx + OfsetXstart),
                                ((int)EntitysTexture2D[i, x].Posy + OfsetYstart),
                                (int)EntitysTexture2D[i, x].SIZE.X,
                                (int)EntitysTexture2D[i, x].SIZE.Y
                            ),
                            Color.White
                        );
                        }

                    }
                }
            }
        }
        public static void Movement()
        {
            for (int i = 0; i < row; ++i)
            {
                for (int x = 0; x < col - 2; x++)
                {
                    if (KeyBoardDetection.W) { EntitysTexture2D[i, x].Posy += 3;  }
                    if (KeyBoardDetection.S) { EntitysTexture2D[i, x].Posy -= 3; }
                    if (KeyBoardDetection.D) { EntitysTexture2D[i,x].Posx -= 3; }
                    if (KeyBoardDetection.A) { EntitysTexture2D[i, x].Posx += 3; }

                }
            }

        } //Movimiento del mapa.
        #endregion


    }//Map.
}
