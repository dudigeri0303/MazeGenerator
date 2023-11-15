using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MazeGenerator
{
    public class MouseHandler
    {
        private Point mousePositione;
        private bool clicked;
        

        public MouseHandler() 
        {

        }

        public bool getClicked() 
        {
            return this.clicked;
        }

        public void setMouseStae() 
        {
            var mouseState = Mouse.GetState();
            this.mousePositione = new Point(mouseState.X, mouseState.Y);

            if (mouseState.LeftButton == ButtonState.Pressed) 
            {
                this.clicked = true;
            }
            else { this.clicked = false; }
            
        }

        public Point getMousePosition() 
        {
            return this.mousePositione;
        }


    }
}
