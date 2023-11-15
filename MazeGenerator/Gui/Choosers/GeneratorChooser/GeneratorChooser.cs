using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MazeGenerator
{
    public class GeneratorChooser
    {
        private string text;   

        private GeneratorChooserLeftButton leftButton;
        private GeneratorChooserRightButton rightButton;

        private Vector2 position;
        
        public GeneratorChooser(AlgorithmChooser algorithmChooser) 
        {
            this.text = algorithmChooser.getChosenGenerator().getName();
            
            this.leftButton = new GeneratorChooserLeftButton(700, 300, 20, 20, Game1.mouseHandler, algorithmChooser, this);
            this.rightButton = new GeneratorChooserRightButton(840, 300, 20, 20, Game1.mouseHandler, algorithmChooser, this);

            this.position = new Vector2(722, 300);
        }

        public void updateText(AlgorithmChooser algorithmChooser) 
        {
            this.text = algorithmChooser.getChosenGenerator().getName();
        }

        public void draw(SpriteBatch spriteBatch) 
        {
            this.leftButton.draw(spriteBatch);
            this.rightButton.draw(spriteBatch);
            spriteBatch.DrawString(Game1.font, this.text, new Vector2(this.position.X, this.position.Y), Color.Black);
        }

        public void act() 
        {
            this.leftButton.act();
            this.rightButton.act();
        }
    }
}
