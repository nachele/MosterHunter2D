using Microsoft.Xna.Framework.Graphics;

using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;




namespace MonoGame
{
    internal class MainCharacter : Entity
    {
        #region propiedades

        protected float speed = 4;
        public float Speed
        {
            get { return speed; }
        }
        //geter();

        #endregion

        #region funciones
        public MainCharacter(ContentManager content, string imageName, int posX, int posY, Vector2 size, int dividendo) : base(content, imageName,posX,posY, size, dividendo) 
        {

        }
        public  void Movement()//movimiento del muñeco en x y en y segun la tecla presionada.
       {
            
            if (KeyBoardDetection.W)
            {
               // this.posY -= this.speed;
            }
            if (KeyBoardDetection.A)
            { 
                //this.posX -= this.speed;
            }
            if (KeyBoardDetection.S)
            {
                //this.posY += this.speed;
            }
            if(KeyBoardDetection.D)
            {
                //this.posX += this.speed;
            }
        }//SetPosition()
        public void Draw(SpriteBatch _spriteBatch) //dibuja la imagen recortada por el sprite que quiero.
        {
            _spriteBatch.Draw(Texture, new Rectangle((int)posX, (int)posY, (int)size.X, (int)size.Y),sourceRect,Color.White); // Pintar imagen
        }//Renderer();
        public async Task Animation() //character sprite animation.
        {
            while (true)
            {
                if (KeyBoardDetection.S)
                {
                    sourceRect.Y = 0;
                    if (sourceRect.X < size.X / 2)
                    {
                        sourceRect.X = 50;
                    }
                    else if (sourceRect.X >= size.Y / 2)
                    {
                        sourceRect.X = 0;
                    }
                }
                if (KeyBoardDetection.W)
                {
                    sourceRect.Y = 50;
                    if (sourceRect.X < size.X / 2)
                    {
                        sourceRect.X = 50;
                    }
                    else if (sourceRect.X >= size.Y / 2)
                    {
                        sourceRect.X = 0;
                    }
                }
                if (KeyBoardDetection.A)
                {
                    sourceRect.Y = 0;
                    if (sourceRect.X < size.X / 2)
                    {
                        sourceRect.X = 50;
                    }
                    else if (sourceRect.X >= size.Y / 2)
                    {
                        sourceRect.X = 0;
                    }
                }
                if (KeyBoardDetection.D)
                {
                    sourceRect.Y = 50;
                    if (sourceRect.X < size.X / 2)
                    {
                        sourceRect.X = 50;
                    }
                    else if (sourceRect.X >= size.X / 2)
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
