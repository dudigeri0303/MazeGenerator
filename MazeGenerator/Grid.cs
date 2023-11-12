using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    internal class Grid
    {
        private Vector2 position;
        private int width, heigth;
        private Texture2D texture;
        private Color color;
        private Rectangle rect;
        private bool visited = false;
        private Tuple<int, int>[] gridsAround;
        private Tuple<int, int> indexes;
        private List<Grid> connectedGrids; 
        private int visitedCount = 0;


        public Grid(GraphicsDevice graphicsDevice, int x, int y, int widht, int height, int i, int j) 
        {
            this.position = new Vector2((int)x, (int)y);
            this.color = Color.Black;
            this.width = widht;
            this.heigth = height;

            this.rect = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.heigth);
            this.texture = new Texture2D(graphicsDevice, 1, 1);
            this.texture.SetData(new[] { Color.White });

            this.indexes = Tuple.Create(i, j);
            
            this.connectedGrids = new List<Grid>();
        }
        public void setColor(Color color) 
        {
            this.color = color;
        }

        public Tuple<int, int>[] getGridsAround() 
        {
            return this.gridsAround;
        }

        public void setGridsAround(Tuple<int, int>[] neighbours) 
        {
            this.gridsAround = neighbours;
        }
        public bool getVisited() { return visited; }
        public void setVisited(bool visited) {  this.visited = visited; }

        public Tuple<int, int> getIndexes() 
        {
            return this.indexes;
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

        public void drawGrid(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(this.texture, this.rect, this.color);
        
        }

        public void addGridToConnectedGrids(Grid grid) 
        {
            this.connectedGrids.Add(grid);
        }

        public List<Grid> getConnectedGrids() 
        {
            return this.connectedGrids;
        }

        public int getVisitedCount() 
        {
            return this.visitedCount;
        }

        public void incraseVisitiedCount() 
        {
            this.visitedCount++;
        }

        public Color getColor()
        {
            return this.color;
        }

        
    }
}
