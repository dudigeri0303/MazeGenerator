using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;


namespace MazeGenerator
{
    public class Solver
    {
        private Random random;
        private Stack<MazeGrid> solveStack;

        public Solver(MazeGrid grid) 
        {
            this.random = new Random();
            this.solveStack = new Stack<MazeGrid>();
            this.solveStack.Push(grid);
        }

        public void reset(MazeGrid grid) 
        {
            this.solveStack.Clear();
            this.solveStack.Push(grid);
        
        }

        public void Tremauxs(Maze maze) 
        {
            if (maze.getFinishGrid().getColor() != Color.Yellow)
            {
                maze.getFinishGrid().setColor(Color.Yellow);
            }

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
            if (currentGrid == maze.getFinishGrid())
            {
                maze.setSolving(false);
                maze.setSolved(true);
                maze.getFinishGrid().setColor(Color.Blue);
                return;
            }
        }
    }
}
