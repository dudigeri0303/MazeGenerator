using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    internal class Button : GridLike
    {
        public Button(GraphicsDevice graphicsDevice, int x, int y, int widht, int height, int i, int j) : base(graphicsDevice, x, y, widht, height)
        {
        
        }
    }
}
