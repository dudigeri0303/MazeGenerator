using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator.Gui
{
    internal class ResetButton : Button
    {
        public ResetButton(int x, int y, int widht, int height, MouseHandler mouseHandler) : base(x, y, widht, height, mouseHandler)
        {
            this.text = "Reset";
        }

        protected override void onClick()
        {
            Maze.getInstance().resetMaze();
        }
    }
}
