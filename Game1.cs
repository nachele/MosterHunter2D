using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;


namespace MonoGame
{
    public class Game1 : Game
    {
        #region propiedades
        private  static int fps = 120;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private MainCharacter entity;
        private Task animation;        
        public static int FPS
        {
            get { return fps; }
        }
        #endregion

        #region metodos
        public Game1() //constructor
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }//Game1();
        protected override void Initialize() //inicializacion de objetos propiedades.
        {
            
            entity = new MainCharacter(100,100); //Creando Objeto MainCharacter.
            animation = Task.Run(entity.Animation);
            string file = Map.FileTo1DArray("map.txt");
            Map.StringTo2DArray(file);
            //Map.EntitysTexture2DLoad();
            base.Initialize(); //utilizando Initialize de Game.
            Entity ntity = new Entity();
            ntity.Position = new Vector2(100, 100);

        }
        
        protected override void LoadContent()//Cargando el contenido.
        {
           // TODO: use this.Content to load your game content here
            Menu.Load(Content, "PlayButton"); //cargando la imagen del menu
            _spriteBatch = new SpriteBatch(GraphicsDevice); //cargando el spritebatch
            entity.Load(Content,"PersonajeCaminaRecto",2); //cargando textura tamaño de imagen y el recorte.
            Map.TextureLoad(Content, "Cabaña.png");//cargando las texturas del mapa.         
        }

        protected override void Update(GameTime gameTime)//bulce principal.
        {
            KeyBoardDetection.keys(_graphics); //objeto para detectar las teclas pulsadas.
            Menu.Update(); //update del menu.
            if (!Menu.active) {
                entity.Movement(); //movimiento del personaje principal.
            };
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)  //bucle donde dibujar Imagenes.
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); // Fondo
            
            _spriteBatch.Begin();
            if (!Menu.active) {
                Map.Draw(_spriteBatch);
                entity.Draw(_spriteBatch); } //dibuajando entidades.
            if (Menu.active) { Menu.Draw(_spriteBatch); } //dibujando el meno.
            _spriteBatch.End();
            base.Draw(gameTime);
        }//Draw();
        #endregion
    }
}
