using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;


namespace MazeGenerator
{
    public class Tremaux : ISolver
    {
        private Random random;
        private MazeGrid startGrid;
        private Stack<MazeGrid> solveStack;

        public Tremaux(MazeGrid grid) 
        {
            this.startGrid = grid;
            this.random = new Random();
            this.solveStack = new Stack<MazeGrid>();
            this.solveStack.Push(this.startGrid);
        }

        public void solve() 
        {
            MazeGrid currentGrid = this.solveStack.Pop();
            this.solveStack.Push(currentGrid);
            currentGrid.setColor(Color.Blue);

            if (currentGrid.getConnectedGrids().Count > 0)
            {
                int randIndex = this.random.Next(0, currentGrid.getConnectedGrids().Count);

                MazeGrid nextGrid = currentGrid.getConnectedGrids().ElementAt(randIndex);
                currentGrid.incraseVisitiedCount();

                nextGrid.getConnectedGrids().Remove(currentGrid);
                currentGrid.getConnectedGrids().Remove(nextGrid);

                currentGrid = nextGrid;

                this.solveStack.Push(currentGrid);
            }

            //Go back if hits a dead end
            else
            {
                currentGrid.setColor(Color.Green);
                currentGrid = this.solveStack.Pop();
            }

            //Checks if the maze is finished
            if (currentGrid == Maze.getInstance().getFinishGrid())
            {
                Maze.getInstance().setSolving(false);
                Maze.getInstance().setSolved(true);
                this.reset();
            }
        }

        public void reset()
        {
            this.solveStack.Clear();
            this.solveStack.Push(this.startGrid);

        }

        public string getName()
        {
            return "Tremaux's";
        }
        public void setStartGrid(MazeGrid grid)
        {
            this.startGrid = grid;
            this.reset();
        }
    }
}
