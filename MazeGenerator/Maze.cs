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

        private bool generating = true;
        private bool solving = false;
        private bool generated = false;
        private bool solved = false;

        private Random rnd;

        private MazeGrid[,] gridMap;
        private MazeGrid backgroundGrid;

        private MazeGrid solveStartGrid;
        private MazeGrid finsihGrid;

        private Stack<MazeGrid> gridStack { get; set; }
        private Stack<MazeGrid> solveStack;

        private Generator generator;
        private Solver solver;

        public Maze(GraphicsDevice graphicsDevice) 
        {
            this.graphicsDevice = graphicsDevice;

            this.rows = 30; 
            this.cols = 30;

            this.generator = new Generator();
            this.solver = new Solver();

            this.backgroundGrid = new MazeGrid(this.graphicsDevice, 12, 12, 680, 680, 0, 0);
            this.backgroundGrid.setColor(Color.Black);

            this.gridStack = new Stack<MazeGrid>();
            this.solveStack = new Stack<MazeGrid>();
      

            this.gridMap = new MazeGrid[this.rows, this.cols];
            this.fillGridMap();


            this.gridMap[0, 0].setColor(Color.Red);
            this.gridMap[0, 0].setVisited(true);
            this.gridStack.Push(this.gridMap[0, 0]);

            this.solveStartGrid = this.gridMap[0, 0];
            this.solveStack.Push(this.solveStartGrid);

            this.finsihGrid = this.gridMap[rows - 1, cols - 1];
        }

        public Stack<MazeGrid> getGridStack() 
        {
            return this.gridStack;
        }

        public MazeGrid[,] getGridMap() 
        {
            return this.gridMap;
        }

        public MazeGrid getFinishGrid() 
        {
            return this.finsihGrid;
        }

        public Stack<MazeGrid> getSolveStack() 
        {
            return this.solveStack;
        }

        public bool getSolved() 
        {
            return this.solved;
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
            if (this.solved == false) 
            {
                if (this.gridStack.Count > 0)
                {
                    this.generator.iterativeRandomizedDepthFirstSearch(this);
                }
                else { this.solver.Tremauxs(this); }
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
