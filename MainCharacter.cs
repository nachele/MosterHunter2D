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
    internal class MainCharacter
    {
        #region propiedades
        protected Texture2D Texture; 
        protected Vector2 size;
        protected Vector2 position;
        protected int TextureWidth;
        protected int TextureHeight;
        protected float speed;
        protected  Rectangle sourceRect;
        public Texture2D TEXTURE
        {
            get { return Texture; }
            set { Texture = value; }
        }
        public Rectangle SOURCERECT
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }
        public int TEXTUREWIDTH
        {
            get { return TextureWidth; }
            set { TextureWidth = value; }
        }
        public int TEXTUREHEIGHT
        {
            get { return TextureHeight; }
            set { TextureHeight = value; }
        }
        public Vector2 Position //geter
        {
            get { return position; }
            set { position = value; }
        }//geter();

        #endregion

        #region funciones
        public MainCharacter(int xpos, int ypos) //constructor. Inicializa las propiedades.
        {
            this.position = new Vector2(100,100); //posicion.
            this.TextureWidth = 0; //ancho de la textura.
            this.TextureHeight = 0;//alto de la textura.
            this.size = new Vector2(this.TextureWidth,this.TextureHeight); //ancho alto textura.
            this.speed = 5; //velocidad
            sourceRect = new Rectangle(0, 0, 0, 0); 

        }//MainCharacter();
        public void Load(ContentManager content, string imageName , int dividendo) //carga la imagen y el source rect para recortar la parte de la imagen que quiero.
        {
            this.Texture = content.Load<Texture2D>(imageName); //carga el contenido de la imagen.
            TextureWidth = this.Texture.Width; //obteniedo el ancho
            TextureHeight = this.Texture.Height; //TextureHeight.
            size.X = TextureWidth; //tamaño x de la imagen.
            size.Y = TextureHeight;//tamaño y de la imagen.
            sourceRect.Width = TextureWidth / dividendo; sourceRect.Height = TextureHeight / dividendo; //rectangulo para recortar imagen.
        }

        public virtual void Movement()//movimiento del muñeco en x y en y segun la tecla presionada.
        {
            if (KeyBoardDetection.W)
            {
                this.position.Y -= this.speed;
            }
            if (KeyBoardDetection.A)
            { 
                this.position.X -= this.speed;
            }
            if (KeyBoardDetection.S)
            {
                this.position.Y += this.speed;
            }
            if(KeyBoardDetection.D)
            {
                this.position.X += this.speed;
            }
        }//SetPosition()
        public void Draw(SpriteBatch _spriteBatch) //dibuja la imagen recortada por el sprite que quiero.
        {
            _spriteBatch.Draw(Texture, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y),sourceRect,Color.White); // Pintar imagen
        }//Draw();
        public async Task Animation() //character sprite animation.
        {
            while (true)
            {
                if (KeyBoardDetection.S)
                {
                    sourceRect.Y = 0;
                    if (sourceRect.X < TextureWidth / 2)
                    {
                        sourceRect.X = 50;
                    }
                    else if (sourceRect.X >= TextureWidth / 2)
                    {
                        sourceRect.X = 0;
                    }
                }
                if (KeyBoardDetection.W)
                {
                    sourceRect.Y = 50;
                    if (sourceRect.X < TextureWidth / 2)
                    {
                        sourceRect.X = 50;
                    }
                    else if (sourceRect.X >= TextureWidth / 2)
                    {
                        sourceRect.X = 0;
                    }
                }
                if (KeyBoardDetection.A)
                {
                    sourceRect.Y = 0;
                    if (sourceRect.X < TextureWidth / 2)
                    {
                        sourceRect.X = 50;
                    }
                    else if (sourceRect.X >= TextureWidth / 2)
                    {
                        sourceRect.X = 0;
                    }
                }
                if (KeyBoardDetection.D)
                {
                    sourceRect.Y = 50;
                    if (sourceRect.X < TextureWidth / 2)
                    {
                        sourceRect.X = 50;
                    }
                    else if (sourceRect.X >= TextureWidth / 2)
                    {
                        sourceRect.X = 0;
                    }
                }
                await Task.Delay(Game1.FPS);
            }
            
        }//Animation();
        #endregion
    }
}
