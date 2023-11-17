using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MazeGenerator
{
    public class RightWallFollower : ISolver
    {
        private Random random;
        private MazeGrid startGrid;
        private Stack<MazeGrid> solveStack;

        public RightWallFollower(MazeGrid grid) 
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
                foreach (var grid in currentGrid.getConnectedGrids())
                {
                    if (grid.getIndexes().Item2 > currentGrid.getIndexes().Item2 & grid.getIndexes().Item1 == currentGrid.getIndexes().Item1)
                    {
                        MazeGrid nextGrid1 = grid;
                        currentGrid.incraseVisitiedCount();

                        nextGrid1.getConnectedGrids().Remove(currentGrid);
                        currentGrid.getConnectedGrids().Remove(nextGrid1);
                        currentGrid = nextGrid1;

                        this.solveStack.Push(currentGrid);
                        return;
                    }
                }
                foreach (var grid in currentGrid.getConnectedGrids())
                {
                    if (grid.getIndexes().Item1 > currentGrid.getIndexes().Item1 & grid.getIndexes().Item2 == currentGrid.getIndexes().Item2)
                    {
                        MazeGrid nextGrid1 = grid;
                        currentGrid.incraseVisitiedCount();

                        nextGrid1.getConnectedGrids().Remove(currentGrid);
                        currentGrid.getConnectedGrids().Remove(nextGrid1);
                        currentGrid = nextGrid1;

                        this.solveStack.Push(currentGrid);
                        return;
                    }
                }

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
            return "RightWallFollower";
        }
        public void setStartGrid(MazeGrid grid)
        {
            this.startGrid = grid;
            this.reset();
        }
    }
}
