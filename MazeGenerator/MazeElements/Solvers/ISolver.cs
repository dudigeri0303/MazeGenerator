using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public interface ISolver : IAlgorithmName
    {
        void solve();
        void reset();
    }
}
