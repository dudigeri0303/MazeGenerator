using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (mouseState.LeftButton == ButtonState.Pressed) 
            {
                this.mousePositione = new Point(mouseState.X, mouseState.Y);
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
