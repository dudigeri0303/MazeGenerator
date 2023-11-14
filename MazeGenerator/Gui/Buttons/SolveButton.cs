using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    internal class SolveButton : Button
    {
        public SolveButton(int x, int y, int widht, int height, MouseHandler mouseHandler) : base(x, y, widht, height, mouseHandler)
        {
            this.text = "Solve";
        }

        protected override void onClick()
        {
            if (Maze.getInstance().getGenerated())
            {
                Maze.getInstance().setSolving(true);
            }
            
        }
    }
}
