﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class Generator
    {
        private Random random;
        private Stack<MazeGrid> gridStack;

        public Generator(MazeGrid grid) 
        {
            this.random = new Random();
            this.gridStack = new Stack<MazeGrid>();
            this.gridStack.Push(grid);
        }

        public Stack<MazeGrid> GetGridStack() {  return this.gridStack; }

        public void iterativeRandomizedDepthFirstSearch(Maze maze)
        {
            //List for the Grids near the currentgrid baes in the gridsAround list
            List<MazeGrid> neighbours = new List<MazeGrid>();

            MazeGrid curretGrid = this.gridStack.Pop();
            Tuple<int, int> indexes = curretGrid.getIndexes();

            //Adds the neighbours to the list (only if it is not visited)
            foreach (var g in curretGrid.getGridsAround())
            {
                if (maze.getGridMap()[g.Item1 + indexes.Item1, g.Item2 + indexes.Item2].getVisited() == false)
                {
                    neighbours.Add(maze.getGridMap()[g.Item1 + indexes.Item1, g.Item2 + indexes.Item2]);
                }
            }

            //if there is unvisited neighbour
            if (neighbours.Count > 0)
            {
                this.gridStack.Push(curretGrid);
                MazeGrid previousGrid = curretGrid;

                int randomIndex = this.random.Next(0, neighbours.Count);
                curretGrid = neighbours[randomIndex];
                curretGrid.setVisited(true);
                curretGrid.setColor(Color.White);

                previousGrid.addGridToConnectedGrids(curretGrid);
                curretGrid.addGridToConnectedGrids(previousGrid);

                //Inscreses the size of the correct grid to give the illusion of deleting the walll between them
                if (previousGrid.getIndexes().Item1 > curretGrid.getIndexes().Item1)
                {
                    curretGrid.incraseWidth(4);
                }

                else if (previousGrid.getIndexes().Item1 < curretGrid.getIndexes().Item1)
                {
                    previousGrid.incraseWidth(4);
                }

                else if (previousGrid.getIndexes().Item2 > curretGrid.getIndexes().Item2)
                {
                    curretGrid.incraseHeight(4);
                }

                else if (previousGrid.getIndexes().Item2 < curretGrid.getIndexes().Item2)
                {
                    previousGrid.incraseHeight(4);
                }
                this.gridStack.Push(curretGrid);
            }

            if (this.gridStack.Count == 0) 
            {
                maze.setGenerating(false);
                maze.setGenerated(true);
            }
        }
    }
}
