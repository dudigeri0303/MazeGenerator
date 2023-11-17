using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MazeGenerator
{
    public class IterativeRandomizedDFS : IGenerator
    {
        private Random random;
        private Stack<MazeGrid> gridStack;

        public IterativeRandomizedDFS(MazeGrid grid) 
        {
            this.random = new Random();
            this.gridStack = new Stack<MazeGrid>();
            this.gridStack.Push(grid);
        }
        public void generate()
        {
            //List for the Grids near the currentgrid baes in the gridsAround list
            List<MazeGrid> neighbours = new List<MazeGrid>();

            MazeGrid currentGrid = this.gridStack.Pop();

            //Adds the neighbours to the list (only if it is not visited)
            foreach (var g in currentGrid.getGridsAround())
            {
                if (Maze.getInstance().getGridMap()[g.Item1 + currentGrid.getIndexes().Item1, g.Item2 + currentGrid.getIndexes().Item2].getVisited() == false)
                {
                    neighbours.Add(Maze.getInstance().getGridMap()[g.Item1 + currentGrid.getIndexes().Item1, g.Item2 + currentGrid.getIndexes().Item2]);
                }
            }

            //if there is unvisited neighbour
            if (neighbours.Count > 0)
            {
                this.gridStack.Push(currentGrid);
                MazeGrid previousGrid = currentGrid;

                int randomIndex = this.random.Next(0, neighbours.Count);
                currentGrid = neighbours[randomIndex];
                currentGrid.setVisited(true);
                currentGrid.setColor(Color.White);

                previousGrid.addGridToConnectedGrids(currentGrid);
                currentGrid.addGridToConnectedGrids(previousGrid);

                Maze.getInstance().mergeGrids(previousGrid, currentGrid);
                this.gridStack.Push(currentGrid);
            }

            if (this.gridStack.Count == 0)
            {
                Maze.getInstance().setGenerating(false);
                Maze.getInstance().setGenerated(true);
                this.reset();
            }
        }

        public void reset()
        {
            this.gridStack.Clear();
            this.gridStack.Push(Maze.getInstance().getStartGrid());
        }

        public string getName()
        {
            return "IterativeRndDFS";
        }
    }
}
