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
        private Maze maze;

        public ResetButton(GraphicsDevice graphicsDevice, int x, int y, int widht, int height, MouseHandler mouseHandler, Maze maze) : base(graphicsDevice, x, y, widht, height, mouseHandler)
        {
            this.maze = maze;
            this.text = "Reset";
        }

        protected override void onClick()
        {
            this.maze.resetMaze();
        }
    }
}
