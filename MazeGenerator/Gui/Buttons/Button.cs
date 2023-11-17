using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MazeGenerator
{
    public abstract class Button : GridLike, IClickable
    {
        protected MouseHandler mouseHandler;
        protected string text;

        public Button(int x, int y, int widht, int height, MouseHandler mouseHandler) : base(x, y, widht, height)
        {
            this.mouseHandler = mouseHandler;
        }

        protected bool isClicked() 
        {
            if (this.rect.Contains(this.mouseHandler.getMousePosition()) & this.mouseHandler.getLeftClicked())
            {
                return true;
            }

            return false;
        }

        protected void colorChangeBasedOnMousePos() 
        {
            if (this.rect.Contains(this.mouseHandler.getMousePosition()) & this.getColor() != Color.DarkGray)
            {
                this.setColor(Color.DarkGray);
            }
            else if (!this.rect.Contains(this.mouseHandler.getMousePosition()) & this.getColor() != Color.Black)
            {
                this.setColor(Color.Black);
            }
        }

        protected abstract void onClick();

        public void click()
        {
            this.onClick();
        }

        public void act() 
        {
            this.colorChangeBasedOnMousePos();
            if (this.isClicked()) 
            {
                this.click();
            }
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            base.draw(spriteBatch);
            spriteBatch.DrawString(Game1.font, this.text, new Vector2(this.position.X, this.position.Y), Color.White);
        }

        
    }
}
