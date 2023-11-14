using MazeGenerator.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Net.Mime;

namespace MazeGenerator
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyInputHandler keyInputHandler;

        private GenerateButton generateButton;
        private SolveButton solveButton;
        private ResetButton resetButton;

        private MouseHandler mouseHandler;

        public Maze maze;

        public static SpriteFont font;
        public static int mazeGridWidth = 18;
        public static int mazeGridHeight = 18;
        public static int mazeGridMargin = 4;


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


            this.maze = new Maze(this.GraphicsDevice);

            this.keyInputHandler = new KeyInputHandler(this.GraphicsDevice);

            this.mouseHandler = new MouseHandler();

            this.generateButton = new GenerateButton(this.GraphicsDevice, 720, 10, 100, 50, this.mouseHandler, this.maze);
            this.solveButton = new SolveButton(this.GraphicsDevice, 720, 100, 100, 50, this.mouseHandler, this.maze);
            this.resetButton = new ResetButton(this.GraphicsDevice, 720, 190, 100, 50, this.mouseHandler, this.maze);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            this.keyInputHandler.handleKey(this);

            this.mouseHandler.setMouseStae();

            this.generateButton.act();
            this.solveButton.act();
            this.resetButton.act();
            
            this.maze.checkSolved();
            this.maze.generateOrSolve();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            this.spriteBatch.Begin();

            this.maze.drawMaze(this.spriteBatch);

            this.generateButton.drawGrid(this.spriteBatch);
            this.solveButton.drawGrid(this.spriteBatch);
            this.resetButton.drawGrid(spriteBatch);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}