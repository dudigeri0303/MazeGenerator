using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace MazeGenerator
{
    internal class Maze
    {
        private GraphicsDevice graphicsDevice;

        private int rows, cols;

        private bool solved;

        private Random rnd;

        private Grid[,] gridMap;
        private Grid backgroundGrid;

        private Grid solveStartGrid;
        private Grid finsihGrid;

        private Stack<Grid> gridStack;
        private Stack<Grid> solveStack;

        public Maze(GraphicsDevice graphicsDevice) 
        {
            this.graphicsDevice = graphicsDevice;

            this.rows = 30; 
            this.cols = 30;

            this.solved = false;

            this.rnd = new Random();

            this.backgroundGrid = new Grid(this.graphicsDevice, 12, 12, 680, 680, 0, 0);
            this.backgroundGrid.setColor(Color.Black);

            this.gridStack = new Stack<Grid>();
            this.solveStack = new Stack<Grid>();
      

            this.gridMap = new Grid[this.rows, this.cols];
            this.fillGridMap();


            this.gridMap[0, 0].setColor(Color.Red);
            this.gridMap[0, 0].setVisited(true);
            this.gridStack.Push(this.gridMap[0, 0]);

            this.solveStartGrid = this.gridMap[0, 0];
            this.solveStack.Push(this.solveStartGrid);

            this.finsihGrid = this.gridMap[rows - 1, cols - 1];
        }

        private void fillGridMap() 
        {
            int width = 18;
            int height = 18;

            for (int i = 0; i < this.rows; i++) 
            {
                for (int j = 0; j < this.cols; j++) 
                {
                    this.gridMap[i, j] = new Grid(this.graphicsDevice, (i + 1) * (width + 4) , (j+1) * (height + 4), width, height, i, j);

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

        public void GenerateMaze() 
        {
            List<Grid> neighbours = new List<Grid>();

            Grid curretGrid = this.gridStack.Pop();
            Tuple<int, int> indexes = curretGrid.getIndexes();

            foreach (var g in curretGrid.getGridsAround())
            {
                if (this.gridMap[g.Item1 + indexes.Item1, g.Item2 + indexes.Item2].getVisited() == false)
                {
                    neighbours.Add(this.gridMap[g.Item1 + indexes.Item1, g.Item2 + indexes.Item2]);
                }
            }

            if (neighbours.Count > 0)
            {

                this.gridStack.Push(curretGrid);
                Grid previousGrid = curretGrid;

                int randomIndex = this.rnd.Next(0, neighbours.Count);
                curretGrid = neighbours[randomIndex];
                curretGrid.setVisited(true);
                curretGrid.setColor(Color.Red);

                previousGrid.addGridToConnectedGrids(curretGrid);
                curretGrid.addGridToConnectedGrids(previousGrid);

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
        }
        

        public void solveMaze()
        {
            if (this.solveStack.Count > 0 & this.finsihGrid.getVisitedCount() == 0)
            {
                if (this.finsihGrid.getColor() != Color.Yellow) 
                {
                    this.finsihGrid.setColor(Color.Yellow);
                }

                Grid currentGrid = this.solveStack.Pop();
                solveStack.Push(currentGrid);

                currentGrid.setColor(Color.Blue);

                if (currentGrid.getConnectedGrids().Count > 0)
                {
                    int randIndex = this.rnd.Next(0, currentGrid.getConnectedGrids().Count);

                    Grid nextGrid = currentGrid.getConnectedGrids().ElementAt(randIndex);
                    currentGrid.incraseVisitiedCount();

                    nextGrid.getConnectedGrids().Remove(currentGrid);
                    currentGrid.getConnectedGrids().Remove(nextGrid);

                    currentGrid = nextGrid;

                    this.solveStack.Push(currentGrid);
                }

                else
                {
                    currentGrid.setColor(Color.Green);
                    currentGrid = this.solveStack.Pop();
                }
            }
        }

        public void generateOrSolve()
        {
            if (this.solved == false) 
            {
                if (this.gridStack.Count > 0)
                {
                    this.GenerateMaze();
                }
                else { this.solveMaze(); }
            } 
        }

        public void checkSolved() 
        { 

            if (this.finsihGrid.getVisitedCount() != 0) 
            {
                this.solved = true;
                for (int i = 0; i < this.rows; i++)
                {
                    for (int j = 0; j < this.cols; j++)
                    {
                        if (this.gridMap[i, j].getColor() != Color.Blue) 
                        {
                            this.gridMap[i, j].setColor(Color.Red);
                        }
                    }
                }

            }
        }
    }
}
