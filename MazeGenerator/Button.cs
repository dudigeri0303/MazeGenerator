using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public abstract class Button : GridLike
    {

        protected MouseHandler mouseHandler;
        public Button(GraphicsDevice graphicsDevice, int x, int y, int widht, int height, MouseHandler mouseHandler) : base(graphicsDevice, x, y, widht, height)
        {
            this.mouseHandler = mouseHandler;
        }

        protected bool isClicked() 
        {
            if (this.rect.Contains(this.mouseHandler.getMousePosition()))
            {
                return true;
            }
            return false;
        }

        protected abstract void onClick();

        public void act() 
        {
            if (this.isClicked()) 
            {
                this.onClick();
            }
        }
    }
}
