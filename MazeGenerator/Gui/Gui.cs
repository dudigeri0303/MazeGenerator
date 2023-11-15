using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class Gui
    {
        private List<Button> buttons;

        public Gui() 
        {
            this.buttons = new List<Button>()
            {
                new GenerateButton(720, 10, 100, 50, Game1.mouseHandler),
                new SolveButton(720, 100, 100, 50, Game1.mouseHandler),
                new ResetButton(720, 190, 100, 50, Game1.mouseHandler)

            };
        }


        public void drawGui(SpriteBatch spriteBatch) 
        {
            foreach (Button button in this.buttons) 
            {
                button.draw(spriteBatch);
            }
        
        }

        public void updateAndAct() 
        {
            foreach (Button button in this.buttons)
            {
                button.act();
            }
        }


    }
}
