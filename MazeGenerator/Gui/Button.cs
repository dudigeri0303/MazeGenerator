using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
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
        protected string text;

        public Button(int x, int y, int widht, int height, MouseHandler mouseHandler) : base(x, y, widht, height)
        {
            this.mouseHandler = mouseHandler;
        }

        protected bool isClicked() 
        {
            if (this.rect.Contains(this.mouseHandler.getMousePosition()) & this.mouseHandler.getClicked())
            {
                return true;
            }
            return false;
        }

        protected abstract void onClick();

        public void act() 
        {
            if (this.isClicked() ) 
            {
                this.onClick();
            }
        }

        public override void drawGrid(SpriteBatch spriteBatch)
        {
            base.drawGrid(spriteBatch);
            spriteBatch.DrawString(Game1.font, this.text, new Vector2(this.position.X, this.position.Y), Color.White);

        }
    }
}
