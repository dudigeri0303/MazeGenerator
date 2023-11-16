
namespace MazeGenerator
{
    public abstract class RightButton : Button
    {
        protected RightButton(int x, int y, int widht, int height, MouseHandler mouseHandler) : base(x, y, widht, height, mouseHandler)
        {
            this.text = "->";
        }
    }
}
