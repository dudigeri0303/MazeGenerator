using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class Solver
    {
        private Random random;

        public Solver() 
        {
            this.random = new Random();
        }

        public void Tremauxs(Maze maze) 
        {
            if (maze.getFinishGrid().getColor() != Color.Yellow)
            {
                maze.getFinishGrid().setColor(Color.Yellow);
            }

            MazeGrid currentGrid = maze.getSolveStack().Pop();
            maze.getSolveStack().Push(currentGrid);
            currentGrid.setColor(Color.Blue);

            if (currentGrid.getConnectedGrids().Count > 0)
            {
                int randIndex = this.random.Next(0, currentGrid.getConnectedGrids().Count);

                MazeGrid nextGrid = currentGrid.getConnectedGrids().ElementAt(randIndex);
                currentGrid.incraseVisitiedCount();

                nextGrid.getConnectedGrids().Remove(currentGrid);
                currentGrid.getConnectedGrids().Remove(nextGrid);

                currentGrid = nextGrid;

                maze.getSolveStack().Push(currentGrid);
            }

            //Go back if hits a dead end
            else
            {
                currentGrid.setColor(Color.Green);
                currentGrid = maze.getSolveStack().Pop();
            }

            //Checks if the maze is finished
            if (currentGrid == maze.getFinishGrid())
            {
                maze.setSolved(true);
                maze.getFinishGrid().setColor(Color.Blue);
                return;
            }
        }
    }
}
