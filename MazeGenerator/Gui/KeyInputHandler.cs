using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGenerator
{
    public class KeyInputHandler
    {
        private bool rKeyBool;
        private KeyboardState keybordState;

        public KeyInputHandler()
        {
            this.rKeyBool = false;

        }

        private void isKeysDown()
        {
            if (this.keybordState.IsKeyDown(Keys.R))
            {
                this.rKeyBool = true;
            }
        }

        private void isKeysUp()
        {
            if (this.keybordState.IsKeyUp(Keys.R))
            {
                this.rKeyBool = false;
            }
        }

        private void actionBasedOnPressedKey(Game1 game)
        {
            if (this.rKeyBool == true)
            {
               
            }
        }

        public void handleKey(Game1 game)
        {
            this.keybordState = Keyboard.GetState();
            this.isKeysDown();
            this.isKeysUp();
            this.actionBasedOnPressedKey(game);
        }
    }
}

