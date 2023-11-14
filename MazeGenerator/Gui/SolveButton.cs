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
        private Maze maze;

        public SolveButton(GraphicsDevice graphicsDevice, int x, int y, int widht, int height, MouseHandler mouseHandler, Maze maze) : base(graphicsDevice, x, y, widht, height, mouseHandler)
        {
            this.maze = maze;
            this.text = "Solve";
        }

        protected override void onClick()
        {
            if (this.maze.getGenerated())
            {
                this.maze.setSolving(true);
            }
            
        }
    }
}
