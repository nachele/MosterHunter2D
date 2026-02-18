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
        private MainCharacter Player;
        private Task animation;
        private Texture2D fondo;
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
            
            Player = new MainCharacter(Content, "PersonajeCaminaRecto", GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/2 - 50, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height/2,new Vector2(100,100),2); //Creando Objeto MainCharacter.
            animation = Task.Run(Player.Animation);
            string file = Map.FileTo1DArray("map.txt");
            Map.StringTo2DArray(file);
            Map.MapEntities(Content);
            //Map.EntitysTexture2DLoad();
            base.Initialize(); //utilizando Initialize de Game.
            

        }
        
        protected override void LoadContent()//Cargando el contenido.
        {
           
            Menu.Load(Content, "PlayButton"); //cargando la imagen del menu

            _spriteBatch = new SpriteBatch(GraphicsDevice); //cargando el spritebatch
            fondo = Content.Load<Texture2D>("h");

        }

        protected override void Update(GameTime gameTime)//bulce principal.
        {
            KeyBoardDetection.keys(_graphics); //objeto para detectar las teclas pulsadas.
            Menu.Update(); //update del menu.
            if (!Menu.active) {
                Map.Movement();
                Player.Movement(); //movimiento del personaje principal.
            };
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)  //bucle donde dibujar Imagenes.
        {

            GraphicsDevice.Clear(Color.Green); // Fondo
            
            _spriteBatch.Begin();
            //_spriteBatch.DrawRendered(fondo, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            if (!Menu.active) {
                //Map.Renderer(Player);
                //Map.DrawRendered(_spriteBatch, Player);
               Map.RenderUpdate(Player,_spriteBatch);
                Player.Draw(_spriteBatch); } //dibuajando entidades.
            if (Menu.active) { Menu.Draw(_spriteBatch); } //dibujando el meno.
            _spriteBatch.End();
            base.Draw(gameTime);
        }//Renderer();
        #endregion
    }
}
