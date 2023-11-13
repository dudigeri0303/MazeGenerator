using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeGenerator
{
    public class Maze
    {
        private GraphicsDevice graphicsDevice;

        private int rows, cols;

        private bool generating = false;
        private bool solving = false;
        private bool generated = false;
        private bool solved = false;

        private MazeGrid[,] gridMap;
        private BackgroundGrid backgroundGrid;

        private MazeGrid startGrid;
        private MazeGrid finsihGrid;

        private Generator generator;
        private Solver solver;

        public Maze(GraphicsDevice graphicsDevice) 
        {
            this.graphicsDevice = graphicsDevice;

            this.rows = 30;
            this.cols = 30;

            this.backgroundGrid = new BackgroundGrid(this.graphicsDevice, 12, 12, 680, 680);
            this.backgroundGrid.setColor(Color.Black);

            this.gridMap = new MazeGrid[this.rows, this.cols];
            this.fillGridMap();

            this.startGrid = this.gridMap[0, 0];
            this.startGrid.setColor(Color.Red);
            this.startGrid.setVisited(true);
            this.finsihGrid = this.gridMap[rows - 1, cols - 1];

            this.generator = new Generator(this.startGrid);
            this.solver = new Solver(startGrid);
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


        //Creates the girdmap
        private void fillGridMap() 
        {
            //The hight and width of a grid
            int width = 18;
            int height = 18;

            for (int i = 0; i < this.rows; i++) 
            {
                for (int j = 0; j < this.cols; j++) 
                {
                    this.gridMap[i, j] = new MazeGrid(this.graphicsDevice, (i + 1) * (width + 4) , (j+1) * (height + 4), width, height, i, j);

                    //Adds the indexes of the the possible neighbours to the girdsAround list based on the girds location
                    if (i == 0 & j == 0)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(0, 1), Tuple.Create(1, 0)};
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (j == 0 & i > 0 & i < this.rows - 1)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(0, 1), Tuple.Create(1, 0), Tuple.Create(-1, 0) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (i == this.rows - 1 & j == 0)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(-1, 0), Tuple.Create(0, 1) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (i == this.rows - 1 & j > 0 & j < this.cols - 1)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(0, 1), Tuple.Create(0, -1), Tuple.Create(-1, 0) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (i == this.rows - 1 & j == this.cols - 1)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(0, -1), Tuple.Create(-1, 0) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (j == this.cols - 1 & i > 0 & i < this.rows - 1)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(1, 0), Tuple.Create(0, -1), Tuple.Create(-1, 0) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (i == 0 & j == this.cols - 1)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(1, 0), Tuple.Create(0, -1) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (i == 0 & j > 0 & j < this.cols - 1)
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(0, 1), Tuple.Create(1, 0), Tuple.Create(0, -1) };
                        this.gridMap[i, j].setGridsAround(neighbours);
                    }

                    else if (i > 0 & i < this.rows -1 & j > 0 & j < this.cols-1 )
                    {
                        Tuple<int, int>[] neighbours = new Tuple<int, int>[] { Tuple.Create(0, 1), Tuple.Create(1, 0), Tuple.Create(0, -1), Tuple.Create(-1, 0)};
                        this.gridMap[i, j].setGridsAround(neighbours);
                    } 
                }
            } 
        }

        public void drawMaze(SpriteBatch spriteBatch)
        {
            this.backgroundGrid.drawGrid(spriteBatch);

            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    this.gridMap[i, j].drawGrid(spriteBatch);
                }
            }
        }

        //Decides to generate or solve the maze (if its already created)
        public void generateOrSolve()
        {
            if (this.generating == true & this.generated == false)
            {
                this.generator.iterativeRandomizedDepthFirstSearch(this);
            }
            else if (this.solving == true & this.generated == true & this.solved == false) 
            {
                this.solver.Tremauxs(this);
            }
        }

        public void checkSolved() 
        { 
            if (this.solved == true) 
            {
                for (int i = 0; i < this.rows; i++)
                {
                    for (int j = 0; j < this.cols; j++)
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
