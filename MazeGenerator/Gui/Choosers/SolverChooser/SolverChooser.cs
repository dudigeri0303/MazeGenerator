using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MazeGenerator
{
    public class SolverChooser
    {
        private string text;

        private SolverChooserLeftButton leftButton;
        private SolverChooserRightButton rightButton;

        private Vector2 position;

        public SolverChooser(AlgorithmChooser algorithmChooser)
        {
            this.text = algorithmChooser.getChosenSolver().getName();

            this.leftButton = new SolverChooserLeftButton(700, 350, 20, 20, Game1.mouseHandler, algorithmChooser, this);
            this.rightButton = new SolverChooserRightButton(840, 350, 20, 20, Game1.mouseHandler, algorithmChooser, this);

            this.position = new Vector2(722, 350);
        }

        public void updateText(AlgorithmChooser algorithmChooser)
        {
            this.text = algorithmChooser.getChosenSolver().getName();
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            this.leftButton.draw(spriteBatch);
            this.rightButton.draw(spriteBatch);
            spriteBatch.DrawString(Game1.font, this.text, new Vector2((int)this.position.X, (int)this.position.Y), Color.Black);
        
        }

        public void act()
        {
            this.leftButton.act();
            this.rightButton.act();
        }
    }
}
