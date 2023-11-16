using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MazeGenerator.MazeElements.Generators
{
    public class AldousBorder : IGenerator
    {
        private Random random;
        private MazeGrid currentGrid;

        public AldousBorder(MazeGrid startGrid) 
        {
            this.random = new Random();
            this.currentGrid = startGrid;
        }

        public void generate()
        {
            if (!this.isFinished())
            {
                List<MazeGrid> neighbours = new List<MazeGrid>();

                foreach (var g in currentGrid.getGridsAround())
                {
                    neighbours.Add(Maze.getInstance().getGridMap()[g.Item1 + currentGrid.getIndexes().Item1, g.Item2 + currentGrid.getIndexes().Item2]);
                }

                MazeGrid previousGrid = currentGrid;

                int randomIndex = this.random.Next(0, neighbours.Count);
                currentGrid = neighbours[randomIndex];

                if (!currentGrid.getVisited())
                {
                    currentGrid.setVisited(true);
                    currentGrid.setColor(Color.White);

                    previousGrid.addGridToConnectedGrids(currentGrid);
                    currentGrid.addGridToConnectedGrids(previousGrid);

                    Maze.getInstance().mergeGrids(previousGrid, currentGrid);  
                }   
            }
            else 
            {
                Maze.getInstance().setGenerating(false);
                Maze.getInstance().setGenerated(true);
                this.reset();
            }
        }

        public void reset()
        {
            this.currentGrid = Maze.getInstance().getStartGrid();
        }

        private bool isFinished() 
        {
            for (int i = 0; i < Game1.rows; i++) 
            {
                for (int j = 0; j < Game1.cols; j++) 
                {
                    if (!Maze.getInstance().getGridMap()[i, j].getVisited()) 
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void chooseRandomMazeGrid() 
        {
            var randI = this.random.Next(0, Game1.rows);
            var randJ = this.random.Next(0, Game1.cols);

            this.currentGrid = Maze.getInstance().getGridMap()[randI, randJ];
        }

        public string getName()
        {
            return "AldousBorder";
        }
    }
}
