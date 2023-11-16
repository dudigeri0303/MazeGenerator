using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MazeGenerator
{
    public class RandomizedKruskal : IGenerator
    {
        private List<Wall> wallList;
        private List<List<MazeGrid>> gridSetList;
        private Random random;
        
        public RandomizedKruskal() 
        {
            this.random = new Random();
            this.wallList = null;
            this.gridSetList = null;
        
        }

        public void generate()
        {
            if (this.wallList == null & this.gridSetList == null) 
            {
                this.wallList = new List<Wall>();
                this.gridSetList = new List<List<MazeGrid>>();

                this.fillWallList();
                this.fillGridSetList();
            }

            else if (this.wallList.Count > 0)
            {
                int randomWallIndex = this.random.Next(0, this.wallList.Count);
                Wall choosenWall = this.wallList[randomWallIndex];

                var grid1 = choosenWall.getGrid1();
                var grid2 = choosenWall.getGrid2();

                List<MazeGrid> grid1Set = null;
                List<MazeGrid> grid2Set = null;

                foreach (var set in this.gridSetList)
                {
                    if (set.Contains(grid1) & !set.Contains(grid2))
                    {
                        grid1Set = set;
                    }
                    else if (set.Contains(grid2) & !set.Contains(grid1))
                    {
                        grid2Set = set;
                    }
                    else if (set.Contains(grid1) & set.Contains(grid2)) 
                    {
                        grid1Set = set;
                        grid1Set = set;
                    }

                }

                if (grid1Set != null & grid2Set != null) 
                {
                    if (grid1Set != grid2Set)
                    {
                        List<MazeGrid> gridJoinedSet = new List<MazeGrid>();
                        foreach (var grid in grid1Set)
                        {
                            if (!gridJoinedSet.Contains(grid))
                            {
                                gridJoinedSet.Add(grid);
                            }
                        }

                        foreach (var grid in grid2Set)
                        {
                            if (!gridJoinedSet.Contains(grid))
                            {
                                gridJoinedSet.Add(grid);
                            }
                        }


                        this.gridSetList.Remove(grid2Set);
                        this.gridSetList.Remove(grid1Set);
                        this.gridSetList.Add(gridJoinedSet);

                        grid1.setColor(Color.White);
                        grid2.setColor(Color.White);

                        Maze.getInstance().mergeGrids(grid1, grid2);

                        grid1.addGridToConnectedGrids(grid2);
                        grid2.addGridToConnectedGrids(grid1);
                    }
                }  
                this.wallList.Remove(choosenWall);
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
            this.wallList = null;
            this.gridSetList = null;
        }

        private void fillWallList() 
        {
            foreach (var wall in Maze.getInstance().getWallList()) 
            {
                this.wallList.Add(wall);
            }
        
        }

        private void fillGridSetList() 
        {
            for (int i = 0; i < Game1.rows; i++) 
            {
                for (int j = 0; j < Game1.cols; j++) 
                {
                    this.gridSetList.Add(new List<MazeGrid>() { Maze.getInstance().getGridMap()[i, j] });
                }
            }
        }

        public string getName()
        {
            return "RndKruskal";
        }

        
    }
}
