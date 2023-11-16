using MazeGenerator.MazeElements;
using MazeGenerator.MazeElements.Generators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MazeGenerator
{
    public class Maze
    {
        private static Maze instance;
        public static Maze getInstance() 
        {
            if (instance == null) 
            {
                instance = new Maze();
            }
            return instance;
        }


        private bool generating = false;
        private bool solving = false;
        private bool generated = false;
        private bool solved = false;

        private MazeGrid[,] gridMap;

        private List<Wall> wallList;

        private BackgroundGrid backgroundGrid;

        private MazeGrid startGrid;
        private MazeGrid finsihGrid;

        private AlgorithmChooser algorithmChooser;

        private Maze() 
        {
            this.backgroundGrid = new BackgroundGrid(12, 12, 680, 680);
            this.backgroundGrid.setColor(Color.Black);

            this.gridMap = new MazeGrid[Game1.rows, Game1.cols];
            this.fillGridMap();

            this.wallList = new List<Wall>();
            this.fillWallList();
            this.removeDupliavtedWalls();

            this.startGrid = this.gridMap[0, 0];
            this.startGrid.setColor(Color.Red);
            this.startGrid.setVisited(true);
            this.finsihGrid = this.gridMap[Game1.rows - 1, Game1.cols - 1];

            this.algorithmChooser = new AlgorithmChooser(this.startGrid);    
        }

        public MazeGrid[,] getGridMap() 
        {
            return this.gridMap;
        }

        public MazeGrid getFinishGrid() 
        {
            return this.finsihGrid;
        }

        public bool getSolved() 
        {
            return this.solved;
        }

        public bool getGenerated() 
        {
            return this.generated;
        }

        public void setGenerating(bool bolean) 
        {
            this.generating = bolean;
        }

        public void setGenerated(bool bolean) 
        {
            this.generated = bolean;
        }

        public void setSolving(bool bolean) 
        {
            this.solving = bolean;
        }

        public void setSolved(bool bolean)
        {
            this.solved = bolean;
        }

        public MazeGrid getStartGrid() 
        {
            return this.startGrid;
        }

        public AlgorithmChooser getAlgorithmChooser() 
        {
            return this.algorithmChooser;
        }

        public bool getGenerating() 
        {
            return this.generating;
        }

        public bool getSolving() 
        {
            return this.solving;
        }

        public List<Wall> getWallList() 
        {
            return this.wallList;
        }


        //Creates the girdmap
        private void fillGridMap() 
        {
            for (int i = 0; i < Game1.rows; i++) 
            {
                for (int j = 0; j < Game1.cols; j++) 
                {
                    this.gridMap[i, j] = new MazeGrid((i + 1) * (Game1.mazeGridWidth + Game1.mazeGridMargin) , (j+1) * (Game1.mazeGridHeight + Game1.mazeGridMargin), Game1.mazeGridWidth, Game1.mazeGridHeight, i, j);

                    //Adds the indexes of the the possible neighbours to the girdsAround list based on the girds location
                    if (i == 0 & j == 0)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(0, 1), Tuple.Create(1, 0)};
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (j == 0 & i > 0 & i < Game1.rows - 1)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(0, 1), Tuple.Create(1, 0), Tuple.Create(-1, 0) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (i == Game1.rows - 1 & j == 0)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(-1, 0), Tuple.Create(0, 1) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (i == Game1.rows - 1 & j > 0 & j < Game1.cols - 1)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(0, 1), Tuple.Create(0, -1), Tuple.Create(-1, 0) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (i == Game1.rows - 1 & j == Game1.cols - 1)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(0, -1), Tuple.Create(-1, 0) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (j == Game1.cols - 1 & i > 0 & i < Game1.rows - 1)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(1, 0), Tuple.Create(0, -1), Tuple.Create(-1, 0) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (i == 0 & j == Game1.cols - 1)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(1, 0), Tuple.Create(0, -1) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (i == 0 & j > 0 & j < Game1.cols - 1)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(0, 1), Tuple.Create(1, 0), Tuple.Create(0, -1) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (i > 0 & i < Game1.rows - 1 & j > 0 & j < Game1.cols - 1 )
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(0, 1), Tuple.Create(1, 0), Tuple.Create(0, -1), Tuple.Create(-1, 0)};
                        this.gridMap[i, j].setGridsAround(neighbours);
                    } 
                }
            } 
        }

        private void fillWallList() 
        {
            for (int i = 0; i < Game1.rows; i++)
            {
                for (int j = 0; j < Game1.cols; j++)
                {
                    var currentGrid = this.gridMap[i, j];
                    List<MazeGrid> neighbours = new List<MazeGrid>();

                    foreach (var g in currentGrid.getGridsAround())
                    {
                        neighbours.Add(this.gridMap[g.Item1 + currentGrid.getIndexes().Item1, g.Item2 + currentGrid.getIndexes().Item2]);
                    }
                    foreach (var n in neighbours) 
                    {
                        this.wallList.Add(new Wall(currentGrid, n));
                    }
                }
            }
        }

        private void removeDupliavtedWalls()
        {
            var newWallList = new List<Wall>();
            for (int i = 0; i < this.wallList.Count; i++)
            {
                for (int j = 0; j < this.wallList.Count; j++)
                {
                    if (this.wallList[i] != null & this.wallList[j] != null & i != j) 
                    {
                        if ((this.wallList[i].getGrid1() == this.wallList[j].getGrid2() & this.wallList[i].getGrid2() == this.wallList[j].getGrid1()))
                        {
                            this.wallList[j] = null;
                        }
                    }
                }
            }
            
            for (int i = 0;i < this.wallList.Count;i++)
            {
                if (this.wallList[i] != null) 
                {
                    newWallList.Add(this.wallList[i]);
                }
            }
            this.wallList = newWallList;

            foreach (Wall wall in this.wallList) 
            {
                Debug.WriteLine($"{wall.getGrid1().getIndexes().Item1},{wall.getGrid1().getIndexes().Item2} +++ {wall.getGrid2().getIndexes().Item1},{wall.getGrid2().getIndexes().Item2}");
            }
        }

        //grid1 = previousGrid, grid2 = currentGrid
        public void mergeGrids(MazeGrid grid1, MazeGrid grid2) 
        {
            if (grid1.getIndexes().Item1 > grid2.getIndexes().Item1)
            {
                grid2.incraseWidth(Game1.mazeGridMargin);
            }

            else if (grid1.getIndexes().Item1 < grid2.getIndexes().Item1)
            {
                grid1.incraseWidth(Game1.mazeGridMargin);
            }

            else if (grid1.getIndexes().Item2 > grid2.getIndexes().Item2)
            {
                grid2.incraseHeight(Game1.mazeGridMargin);
            }

            else if (grid1.getIndexes().Item2 < grid2.getIndexes().Item2)
            {
                grid1.incraseHeight(Game1.mazeGridMargin);
            }

        }

        public void drawMaze(SpriteBatch spriteBatch)
        {
            this.backgroundGrid.draw(spriteBatch);

            for (int i = 0; i < Game1.rows; i++)
            {
                for (int j = 0; j < Game1.cols; j++)
                {
                    this.gridMap[i, j].draw(spriteBatch);
                }
            }
        }

        //Decides to generate or solve the maze (if its already created)
        public void generateOrSolve()
        {
            if (this.generating == true & this.generated == false)
            {
                this.algorithmChooser.getChosenGenerator().generate();
            }
            else if (this.solving == true & this.generated == true & this.solved == false) 
            {
                this.algorithmChooser.getChosenSolver().solve();
            }
        }

        public void resetMaze()
        {
            for (int i = 0; i < Game1.rows; i++)
            {
                for (int j = 0; j < Game1.cols; j++)
                {
                    this.gridMap[i, j].reset();

                }
            }
            this.generating = false;
            this.solving = false;
            this.generated = false;
            this.solved = false;

            this.startGrid = this.gridMap[0, 0];
            this.startGrid.setColor(Color.Red);
            this.startGrid.setVisited(false);
            this.finsihGrid = this.gridMap[Game1.rows - 1, Game1.cols - 1];

            this.algorithmChooser.getChosenGenerator().reset();
            this.algorithmChooser.getChosenSolver().reset();
        }

        public void checkSolved() 
        { 
            if (this.solved == true) 
            {
                for (int i = 0; i < Game1.rows; i++)
                {
                    for (int j = 0; j < Game1.cols; j++)
                    {
                        if (this.gridMap[i, j].getColor() == Color.Green) 
                        {
                            this.gridMap[i, j].setColor(Color.White);
                        }
                    }
                }
            }
        }
    }
}
