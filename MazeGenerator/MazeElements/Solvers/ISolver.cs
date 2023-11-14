using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public interface ISolver
    {
        void solve();
        void reset();
    }
}
