using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGenerator
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Gui gui;


        public static SpriteFont font;
        public static MouseHandler mouseHandler = new MouseHandler();

        public static int mazeGridWidth = 18;
        public static int mazeGridHeight = 18;
        public static int mazeGridMargin = 4;
        public static int rows = 30;
        public static int cols = 30;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 700;

            graphics.ApplyChanges();


            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.gui = new Gui(Maze.getInstance().getAlgorithmChooser());

            font = Content.Load<SpriteFont>("basicFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseHandler.setMouseStae();

            this.gui.updateAndAct();

            Maze.getInstance().checkSolved();
            Maze.getInstance().generateOrSolve();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            this.spriteBatch.Begin();

            Maze.getInstance().drawMaze(this.spriteBatch);

            this.gui.drawGui(this.spriteBatch);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}