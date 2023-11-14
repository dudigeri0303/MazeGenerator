using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MazeGenerator
{
    public class MazeGrid : GridLike
    {
        private bool visited = false;
        private int visitedCount = 0;

        private Tuple<int, int> indexes;
        private Tuple<int, int>[] gridsAround;
        private List<MazeGrid> connectedGrids; 

        public MazeGrid(int x, int y, int widht, int height, int i, int j) : base(x, y, widht, height)
        {
            this.indexes = Tuple.Create(i, j);
            this.connectedGrids = new List<MazeGrid>();
        }

        public Tuple<int, int>[] getGridsAround() 
        {
            return this.gridsAround;
        }

        public void setGridsAround(Tuple<int, int>[] neighbours) 
        {
            this.gridsAround = neighbours;
        }
        public bool getVisited() 
        { 
            return visited; 
        }

        public void setVisited(bool visited) 
        { 
            this.visited = visited; 
        }

        public Tuple<int, int> getIndexes() 
        {
            return this.indexes;
        }

        public void addGridToConnectedGrids(MazeGrid grid) 
        {
            this.connectedGrids.Add(grid);
        }

        public List<MazeGrid> getConnectedGrids() 
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

        public void reset() 
        {
            this.color = Color.Black;
            this.width = Game1.mazeGridWidth;
            this.heigth = Game1.mazeGridHeight;
            this.rect = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.heigth);

            this.visited = false;
            this.visitedCount = 0;
            this.connectedGrids.Clear();
        }
    }
}
