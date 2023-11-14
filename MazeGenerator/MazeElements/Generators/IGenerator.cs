using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public interface IGenerator
    {
        void generate();
        void reset();
    }
}
