
namespace MazeGenerator
{
    public interface ISolver : IAlgorithmName
    {
        void solve();
        void reset();

        void setStartGrid(MazeGrid grid);
    }
}
