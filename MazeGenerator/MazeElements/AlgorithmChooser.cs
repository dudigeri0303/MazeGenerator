using MazeGenerator.MazeElements.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class AlgorithmChooser
    {
        //Generators
        private List<IGenerator> generatorList;

        //Solvers
        private List<ISolver> solverList;

        //Indexers
        private int generatorIndexer = 0;
        private int solverIndexer = 0;

        //choosen algorithms
        private IGenerator choosenGenerator;
        private ISolver choosenSolver;

        private MazeGrid startGrid;

        public AlgorithmChooser(MazeGrid startGrid) 
        {
            this.startGrid = startGrid;

            //Generators
            this.generatorList = new List<IGenerator>()
            {
                new IterativeRandomizedDFS(startGrid),
                new RandomizedPrim(startGrid),
                new RandomizedKruskal(),
                new AldousBorder(startGrid)
            };


            //Solvers
            this.solverList = new List<ISolver>()
            {
                new Tremaux(startGrid)
            };


            //choosen algorithms
            this.choosenGenerator = this.generatorList[this.generatorIndexer];
            this.choosenSolver = this.solverList[this.solverIndexer];
        }

        public IGenerator getChosenGenerator() 
        {
            return this.choosenGenerator;
        }

        public ISolver getChosenSolver() 
        {
            return this.choosenSolver;
        }

        public void incraseGeneratorIndexerAndChangeGenerator() 
        {
            if (!Maze.getInstance().getGenerating()) 
            {
                if (this.generatorIndexer < this.generatorList.Count - 1)
                {
                    this.generatorIndexer++;
                    this.choosenGenerator = this.generatorList[this.generatorIndexer];
                }
            }
            
        }
        public void decraseGeneratorIndexerAndChangeGenerator()
        {
            if (!Maze.getInstance().getGenerating()) 
            {
                if (this.generatorIndexer > 0)
                {
                    this.generatorIndexer--;
                    this.choosenGenerator = this.generatorList[this.generatorIndexer];
                }
            }
            
        }

        public void incraseSolverIndexerAndChangeSolver()
        {
            if (!Maze.getInstance().getSolving()) 
            {
                if (this.solverIndexer < this.solverList.Count - 1)
                {
                    this.solverIndexer++;
                    this.choosenSolver = this.solverList[this.solverIndexer];
                }
            }  
        }

        public void decrasesSolverIndexerAndChangeSolver()
        {
            if (!Maze.getInstance().getSolving()) 
            {
                if (this.solverIndexer > 0)
                {
                    this.solverIndexer--;
                    this.choosenSolver = this.solverList[this.solverIndexer];
                }
            }
        }
    }
}
