
namespace MazeGenerator
{ 
    public class Wall
    {
        private MazeGrid grid1; 
        private MazeGrid grid2;

        public Wall(MazeGrid grid1, MazeGrid grid2) 
        {
            this.grid1 = grid1;
            this.grid2 = grid2;
        }

        public MazeGrid getGrid1 ()
        {
            return this.grid1; 
        }

        public MazeGrid getGrid2 ()
        {
            return this.grid2;
        }
    }
}
