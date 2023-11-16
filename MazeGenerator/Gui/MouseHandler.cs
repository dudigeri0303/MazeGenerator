using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MazeGenerator
{
    public class MouseHandler
    {
        private Point mousePositione;
        private bool leftClicked;
        private bool previousLeftCilcked;
        private bool rightClicked;
        private bool previousRightCilcked;


        public MouseHandler() 
        {
            this.leftClicked = false;
            this.previousLeftCilcked = false;
        }

        public bool getLeftClicked() 
        {
            return this.leftClicked;
        }

        public bool getRightClicked()
        {
            return this.rightClicked;
        }

        public void setMouseStae() 
        {
            var mouseState = Mouse.GetState();
            this.mousePositione = new Point(mouseState.X, mouseState.Y);

            if (mouseState.LeftButton == ButtonState.Pressed & !this.previousLeftCilcked)
            {
                this.leftClicked = true;
                this.previousLeftCilcked = true;
            }
            else { this.leftClicked = false; }


            if (mouseState.LeftButton == ButtonState.Released)
            {
                this.leftClicked = false; 
                this.previousLeftCilcked= false;
            }

            if (mouseState.RightButton == ButtonState.Pressed & !this.previousRightCilcked)
            {
                this.rightClicked = true;
                this.previousRightCilcked = true;
            }
            else { this.rightClicked = false; }


            if (mouseState.RightButton == ButtonState.Released)
            {
                this.rightClicked = false;
                this.previousRightCilcked = false;
            }
        }

        public Point getMousePosition() 
        {
            return this.mousePositione;
        }


    }
}
