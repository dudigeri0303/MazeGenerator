using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MazeGenerator
{
    public class RandomizedPrim : IGenerator
    {
        private List<Wall> wallist;
        private MazeGrid startGrid;
        private Random random;

        public RandomizedPrim(MazeGrid startGrid) 
        {
            this.wallist = null;
            this.startGrid = startGrid;
            this.random = new Random();
        } 
        public void generate()
        {
            if (this.wallist == null) 
            {
                this.wallist = new List<Wall> ();
                this.startGrid.setColor(Color.White);
                this.startGrid.setVisited(true);
                this.addWalls(this.startGrid);
            }

            if (this.wallist.Count > 0)
            {
                int randomWallIndex = this.random.Next(0, this.wallist.Count);
                Wall choosenWall = this.wallist[randomWallIndex];

                if (choosenWall.getGrid1().getVisited() & !choosenWall.getGrid2().getVisited())
                {
                    MazeGrid grid1 = choosenWall.getGrid1();
                    MazeGrid grid2 = choosenWall.getGrid2();

                    this.addWalls(grid2);

                    

                    grid2.setColor(Color.White);
                    grid2.setVisited(true);

                    Maze.getInstance().mergeGrids(grid1, grid2);

                    grid1.addGridToConnectedGrids(grid2);
                    grid2.addGridToConnectedGrids(grid1);
                    
                }
                else if (!choosenWall.getGrid1().getVisited() & choosenWall.getGrid2().getVisited()) 
                {
                    MazeGrid grid1 = choosenWall.getGrid1();
                    MazeGrid grid2 = choosenWall.getGrid2();

                    this.addWalls(grid1);



                    grid1.setColor(Color.White);
                    grid1.setVisited(true);

                    Maze.getInstance().mergeGrids(grid1, grid2);

                    grid1.addGridToConnectedGrids(grid2);
                    grid2.addGridToConnectedGrids(grid1);
                }
                this.wallist.Remove(choosenWall);
                Debug.WriteLine(this.wallist.Count);
            }
            else 
            {
                Maze.getInstance().setGenerating(false);
                Maze.getInstance().setGenerated(true);
                this.reset();
            }
            
        }

        private void addWalls(MazeGrid grid) 
        {
            foreach (Wall wall in Maze.getInstance().getWallList())
            {
                if ((wall.getGrid1() == grid | wall.getGrid2() == grid) & !this.wallist.Contains(wall))
                {
                    this.wallist.Add(wall);
                }
            }
        }

        public string getName()
        {
            return "RndPrim's";
        }

        public void reset()
        {
            this.wallist = null;
            this.startGrid = Maze.getInstance().getStartGrid();
        }
    }
}
