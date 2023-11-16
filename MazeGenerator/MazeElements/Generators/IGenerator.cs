
namespace MazeGenerator
{
    public interface IGenerator : IAlgorithmName
    {
        void generate();
        void reset();
    }
}
