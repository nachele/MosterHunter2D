using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;


namespace MonoGame
{
    internal static class KeyBoardDetection
    {
        #region propiedades
        private static bool w;
        private static bool a;
        private static bool s;
        private static bool d;
        private static bool escPress = false;
        private static bool escRelease = true;
        public static bool W
        {
            get { return w; }
            set { w = value; }
        }  
        public static bool A
        {
            get { return a; }
        }
        public static bool S
        {
            get { return s; }
            set { s = value; }
        }
        public static bool D
        {
            get { return d; }   
        }
        public static bool ESCRELEASE
        {
            get { return escRelease; }
            set { escRelease = value; }
        }
        public static bool ESC
        {
            get { return escPress; }
            set { escPress = value; }
        }
        #endregion

        #region metodos
        public static void keys(GraphicsDeviceManager _graphics)
        {
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.W)) { w = true; }
            if (keyboard.IsKeyDown(Keys.A)) { a = true; }
            if (keyboard.IsKeyDown(Keys.S)) { s = true; }
            if (keyboard.IsKeyDown(Keys.D)) { d = true; }
            if (keyboard.IsKeyDown(Keys.F)) _graphics.ToggleFullScreen();
            if (keyboard.IsKeyDown(Keys.Escape)) { 
                if (Menu.active && escRelease) {    
                    Menu.active = false; 
                } else if(!Menu.active && escRelease) 
                { 
                    Menu.active = true; 
                }  
                escRelease = false;
                
            }

            if (keyboard.IsKeyUp(Keys.W)) { w = false; }
            if (keyboard.IsKeyUp(Keys.A)) { a = false; }
            if (keyboard.IsKeyUp(Keys.S)) { s = false; }
            if (keyboard.IsKeyUp(Keys.D)) { d = false; }
            if (keyboard.IsKeyUp(Keys.Escape)) { escRelease = true; }
        }//keys();
        #endregion
    }
}
  