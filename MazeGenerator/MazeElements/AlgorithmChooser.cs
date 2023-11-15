using MazeGenerator.MazeElements.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator.MazeElements
{
    public class AlgorithmChooser
    {
        //Generators
        private IterativeRandomizedDFS iterativeRandDFS;
        private AldousBorder aldousBorder;

        //Solvers
        private Tremaux tremaux;

        //choosen algorithms
        private IGenerator choosenGenerator;
        private ISolver choosenSolver;

        MazeGrid startGrid;

        public AlgorithmChooser(MazeGrid startGrid) 
        {
            this.startGrid = startGrid;

            //Generators
            this.iterativeRandDFS = new IterativeRandomizedDFS(startGrid);
            this.aldousBorder = new AldousBorder(startGrid);
            
            //Solvers
            this.tremaux = new Tremaux(startGrid);

            //choosen algorithms
            this.choosenGenerator = this.iterativeRandDFS;
            this.choosenSolver = this.tremaux;
        }

        public IGenerator getChosenGenerator() 
        {
            return this.choosenGenerator;
        }

        public ISolver getChosenSolver() 
        {
            return this.choosenSolver;
        }
        
    }
}
