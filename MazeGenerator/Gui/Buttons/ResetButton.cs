namespace MazeGenerator
{
    internal class ResetButton : Button
    {
        public ResetButton(int x, int y, int widht, int height, MouseHandler mouseHandler) : base(x, y, widht, height, mouseHandler)
        {
            this.text = "Reset";
        }

        protected override void onClick()
        {
            Maze.getInstance().resetMaze();
        }
    }
}
