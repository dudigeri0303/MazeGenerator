
namespace MazeGenerator
{
    public abstract class LeftButton : Button
    {
        public LeftButton(int x, int y, int widht, int height, MouseHandler mouseHandler) : base(x, y, widht, height, mouseHandler)
        {
            this.text = "<-";
        }

    }
}
