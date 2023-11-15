using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public abstract class GridLike
    {
        protected Vector2 position;
        protected int width, heigth;
        protected Texture2D texture;
        protected Color color;
        protected Rectangle rect;

        public GridLike(int x, int y, int widht, int height) 
        {
            this.position = new Vector2((int)x, (int)y);
            this.color = Color.Black;
            this.width = widht;
            this.heigth = height;

            this.rect = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.heigth);
            this.texture = new Texture2D(Game1.graphics.GraphicsDevice, 1, 1);
            this.texture.SetData(new[] { Color.White });
        }

        public Color getColor()
        {
            return this.color;
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public void incraseWidth(int width)
        {
            this.width += width;
            this.rect = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.heigth);
        }

        public void incraseHeight(int height)
        {
            this.heigth += height;
            this.rect = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.heigth);
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture, this.rect, this.color);
        }
    }
}
