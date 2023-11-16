
namespace MazeGenerator
{
    internal class GenerateButton : Button
    {
        public GenerateButton(int x, int y, int widht, int height, MouseHandler mouseHandler) : base(x, y, widht, height, mouseHandler)
        {
            this.text = "Generate";
        }

        protected override void onClick()
        {
            Maze.getInstance().setGenerating(true);
        }
    }
}
