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
                }
            }
        }//MapEntities();

        public static void Renderer(MainCharacter Player) //Dibuja los entities.
        {
            if (!init) //inicializando variables.
            {
                renderWidth = 7; //tamaño del renderer.
                OfsetXIndex = Math.Abs(OfsetXstart / 100);
                OfsetIndex = Math.Abs(OfsetYstart / 100);
                PlayerPosY = (Math.Abs(((int)Player.Posy) / 100)); // me da el indice de arriba.
                PlayerPosX = (Math.Abs(((int)Player.Posx) / 100)); // me da el indice de la derecha. 
                topRenderY = PlayerPosY - renderWidth; //alto del render.
                RightRenderX = PlayerPosX + renderWidth;//ancho derecha render
                bottomRenderYInit = PlayerPosY + renderWidth;//parte de abajo del render inicial.
                LeftRenderX = PlayerPosX - renderWidth;//ancho izquierda del render.
                bottomRenderY = 0; //parte de abajo del render.
                init = true;
            }
            if( (int)Math.Abs((EntitysTexture2D[0, 0].Posy - OfsetYstart)) / 100 >= 0 && (int)Math.Abs((EntitysTexture2D[0, 0].Posy - OfsetYstart)) / 100 < (row - 1))
            {
                deltaRenderY = (int)Math.Abs((EntitysTexture2D[0, 0].Posy - OfsetYstart)) / 100; // lo que se le suma en y al renderer para que vaya renderizando nuevo mapa.
            }
            
            if(bottomRenderYInit + deltaRenderY < row)
            {
                bottomRenderY = bottomRenderYInit + deltaRenderY; //nos da la posicion del render parte de abajo. (indice).
            }
            if((bottomRenderY - (renderWidth * 2))>= 0)
            {
                topRenderY = bottomRenderY - (renderWidth * 2); //nos da la posicion del render parte de arriba.(indice).
            }
            else if(topRenderY < 0) //si el render y es menor que la fila de arriba que no se vuelva negativo, porque es un indice
            {
                topRenderY = 0;
            }
        }
        
        public static void RenderUpdate(MainCharacter Player, SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < row - 1; i++)
            {
                for (int x = 0; x < col - 2; x++)
                {
                    if (EntitysTexture2D[i,x].Posy > Player.Posy - 500 && EntitysTexture2D[i,x].Posy < Player.Posy + 500)
                    {
                        _spriteBatch.Draw(EntitysTexture2D[i, x].TEXTURE, new Rectangle((((int)EntitysTexture2D[i, x].Posx + OfsetXstart) + (int)Player.SIZE.X / 2), (((int)EntitysTexture2D[i, x].Posy + OfsetYstart) + (int)Player.SIZE.Y), ((int)(EntitysTexture2D[i, x].SIZE.X)), (int)(EntitysTexture2D[i, x].SIZE.Y)), Color.White); // Pintar imagen
                    }
                }
            }
        }
        public static void DrawRendered(SpriteBatch _spriteBatch, MainCharacter Player) //DrawRendered 
        {
            //lo que renderizo el juego.
            for (int i = topRenderY; i < bottomRenderY; i++)
            {
                for (int x = LeftRenderX; x < RightRenderX; x++)
                {
                    if (map2d[i, x] == "h")
                        _spriteBatch.Draw(EntitysTexture2D[i, x].TEXTURE, new Rectangle((((int)EntitysTexture2D[i, x].Posx + OfsetXstart) + (int)Player.SIZE.X / 2), (((int)EntitysTexture2D[i, x].Posy + OfsetYstart ) + (int)Player.SIZE.Y), ((int)(EntitysTexture2D[i, x].SIZE.X)), (int)(EntitysTexture2D[i, x].SIZE.Y)), Color.White); // Pintar imagen
                }
            } //dibujando la hierba.
            for (int i = topRenderY; i < bottomRenderY; i++)
            {
                for (int x = LeftRenderX; x < RightRenderX; x++)
                {
                    if (map2d[i, x] == "c")
                        _spriteBatch.Draw(EntitysTexture2D[i, x].TEXTURE, new Rectangle((((int)EntitysTexture2D[i, x].Posx + OfsetXstart) + (int)Player.SIZE.X / 2), (((int)EntitysTexture2D[i, x].Posy + OfsetYstart ) + (int)Player.SIZE.Y), ((int)(EntitysTexture2D[i, x].SIZE.X)), (int)(EntitysTexture2D[i, x].SIZE.Y)), Color.White); // Pintar imagen
                }
            } //dibujando las casas.
        }
        public static void DrawAll(SpriteBatch _spriteBatch, MainCharacter Player)//Dibujar todos los entities.
        {
            for (int i = 0; i < row - 1; i++)
            {
                for (int x = 0; x < col - 2; x++)
                {
                    if (map2d[i, x] == "h")
                        _spriteBatch.Draw(EntitysTexture2D[i, x].TEXTURE, new Rectangle((((int)EntitysTexture2D[i, x].Posx + OfsetXstart) + (int)Player.SIZE.X / 2), (((int)EntitysTexture2D[i, x].Posy + OfsetYstart) + (int)Player.SIZE.Y), ((int)(EntitysTexture2D[i, x].SIZE.X)), (int)(EntitysTexture2D[i, x].SIZE.Y)), Color.White); // Pintar imagen
                }
            } //dibujando la hierba.
            for (int i = 0; i < row; i++)
            {
                for (int x = 0; x < col - 2; x++)
                {
                    if (map2d[i, x] == "c")
                        _spriteBatch.Draw(EntitysTexture2D[i, x].TEXTURE, new Rectangle((((int)EntitysTexture2D[i, x].Posx + OfsetXstart) + (int)Player.SIZE.X / 2), (((int)EntitysTexture2D[i, x].Posy + OfsetYstart) + (int)Player.SIZE.Y), ((int)(EntitysTexture2D[i, x].SIZE.X)), (int)(EntitysTexture2D[i, x].SIZE.Y)), Color.White); // Pintar imagen
                }
            }  //dibujando las casas.
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
