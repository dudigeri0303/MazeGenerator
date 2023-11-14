using MazeGenerator.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGenerator
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private GenerateButton generateButton;
        private SolveButton solveButton;
        private ResetButton resetButton;

        private MouseHandler mouseHandler;

        public static SpriteFont font;
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
            font = Content.Load<SpriteFont>("basicFont");

            this.mouseHandler = new MouseHandler();

            this.generateButton = new GenerateButton(720, 10, 100, 50, this.mouseHandler);
            this.solveButton = new SolveButton(720, 100, 100, 50, this.mouseHandler);
            this.resetButton = new ResetButton(720, 190, 100, 50, this.mouseHandler);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            this.mouseHandler.setMouseStae();

            this.generateButton.act();
            this.solveButton.act();
            this.resetButton.act();

            Maze.getInstance().checkSolved();
            Maze.getInstance().generateOrSolve();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            this.spriteBatch.Begin();

            Maze.getInstance().drawMaze(this.spriteBatch);

            this.generateButton.drawGrid(this.spriteBatch);
            this.solveButton.drawGrid(this.spriteBatch);
            this.resetButton.drawGrid(spriteBatch);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}