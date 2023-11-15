using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class SolverChooserRightButton : RightButton
    {
        private AlgorithmChooser algorithmChooser;
        private SolverChooser solverChooser;

        public SolverChooserRightButton(int x, int y, int widht, int height, MouseHandler mouseHandler, AlgorithmChooser algorithmChooser, SolverChooser solverChooser) : base(x, y, widht, height, mouseHandler)
        {
            this.algorithmChooser = algorithmChooser;
            this.solverChooser = solverChooser;
        }

        protected override void onClick()
        {
            this.algorithmChooser.incraseSolverIndexerAndChangeSolver();
            this.solverChooser.updateText(this.algorithmChooser);
        }
    }
}
