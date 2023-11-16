using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MazeGenerator
{
    public class MouseHandler
    {
        private Point mousePositione;
        private bool clicked;
        private bool previousCilcked;
        

        public MouseHandler() 
        {
            this.clicked = false;
            this.previousCilcked = false;
        }

        public bool getClicked() 
        {
            return this.clicked;
        }

        public void setMouseStae() 
        {
            var mouseState = Mouse.GetState();
            this.mousePositione = new Point(mouseState.X, mouseState.Y);

            if (mouseState.LeftButton == ButtonState.Pressed & !this.previousCilcked)
            {
                this.clicked = true;
                this.previousCilcked = true;
            }
            else { this.clicked = false; }


            if (mouseState.LeftButton == ButtonState.Released)
            {
                this.clicked = false; 
                this.previousCilcked= false;
            }
            
        }

        public Point getMousePosition() 
        {
            return this.mousePositione;
        }


    }
}
