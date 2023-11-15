using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MazeGenerator
{
    public class Gui
    {
        private List<Button> buttons;
        private GeneratorChooser generatorChooser;
        private SolverChooser solverChooser;

        public Gui(AlgorithmChooser algorithmChooser) 
        {
            this.buttons = new List<Button>()
            {
                new GenerateButton(720, 10, 100, 50, Game1.mouseHandler),
                new SolveButton(720, 100, 100, 50, Game1.mouseHandler),
                new ResetButton(720, 190, 100, 50, Game1.mouseHandler)
            };

            this.generatorChooser = new GeneratorChooser(algorithmChooser);
            this.solverChooser = new SolverChooser(algorithmChooser);
        }


        public void drawGui(SpriteBatch spriteBatch) 
        {
            foreach (Button button in this.buttons) 
            {
                button.draw(spriteBatch);
            }

            this.generatorChooser.draw(spriteBatch);
            this.solverChooser.draw(spriteBatch);
        
        }

        public void updateAndAct() 
        {
            foreach (Button button in this.buttons)
            {
                button.act();
            }
            this.generatorChooser.act();
            this.solverChooser.act();
        }


    }
}
