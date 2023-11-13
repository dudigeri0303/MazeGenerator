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
        

        public MouseHandler() 
        {

        }

        public void setMouseStae() 
        {
            var mouseState = Mouse.GetState();
            this.mousePositione = new Point(mouseState.X, mouseState.Y);
        }

        public Point getMousePosition() 
        {
            return this.mousePositione;
        }


    }
}
