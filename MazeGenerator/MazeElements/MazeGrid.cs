using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MazeGenerator
{
    public class MazeGrid : GridLike, IClickable
    {
        private bool visited = false;
        private int visitedCount = 0;

        private Tuple<int, int> indexes;
        private Tuple<int, int>[] gridsAround;
        private List<MazeGrid> connectedGrids;
        private List<MazeGrid> connectedGridsBackup;

        private MouseHandler mouseHandler;

        public MazeGrid(int x, int y, int widht, int height, int i, int j, MouseHandler mouseHandler) : base(x, y, widht, height)
        {
            this.indexes = Tuple.Create(i, j);
            this.connectedGrids = new List<MazeGrid>();
            this.connectedGridsBackup = new List<MazeGrid>();
            this.mouseHandler = mouseHandler;
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
            this.connectedGridsBackup.Add(grid);
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
            this.connectedGridsBackup.Clear();
        }

        public void resetForReSolve() 
        {
            this.color = Color.White;
            this.visited = false;
            this.visitedCount = 0;

            this.connectedGrids = new List<MazeGrid>();

            foreach(var grid in this.connectedGridsBackup) 
            {
                this.connectedGrids.Add(grid);
            }
        }

        public void click()
        {
            if (this.rect.Contains(this.mouseHandler.getMousePosition()) & this.mouseHandler.getLeftClicked())
            {
                Maze.getInstance().getSolveStartGrid().setColor(Color.White);
                Maze.getInstance().setSolveStartGrig(this);
                Maze.getInstance().getAlgorithmChooser().setStartGridForSolvers();
            }
            else if (this.rect.Contains(this.mouseHandler.getMousePosition()) & this.mouseHandler.getRightClicked())
            {
                Maze.getInstance().getSolveFinishGrid().setColor(Color.White);

                Maze.getInstance().setSolveFinishGrid(this);
            }
        }
    }
}
