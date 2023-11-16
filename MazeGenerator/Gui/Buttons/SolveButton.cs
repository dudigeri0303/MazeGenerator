
namespace MazeGenerator
{
    internal class SolveButton : Button
    {
        public SolveButton(int x, int y, int widht, int height, MouseHandler mouseHandler) : base(x, y, widht, height, mouseHandler)
        {
            this.text = "Solve";
        }

        protected override void onClick()
        {
            if (Maze.getInstance().getSolved())
            {
                Maze.getInstance().resetForReSolve();
                Maze.getInstance().setSolving(true);

            }

            else if (Maze.getInstance().getGenerated())
            {
                Maze.getInstance().setSolving(true);
            }
        }     
    }
}
