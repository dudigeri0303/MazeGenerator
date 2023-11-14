using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class BackgroundGrid : GridLike
    {
        public BackgroundGrid(GraphicsDevice graphicsDevice, int x, int y, int widht, int height) : base(graphicsDevice, x, y, widht, height)
        {
        }
    }
}
