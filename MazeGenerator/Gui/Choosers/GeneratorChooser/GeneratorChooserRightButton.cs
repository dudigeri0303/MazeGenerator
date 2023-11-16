
namespace MazeGenerator
{
    public class GeneratorChooserRightButton : RightButton
    {
        private AlgorithmChooser algorithmChooser;
        private GeneratorChooser generatorChooser;
        public GeneratorChooserRightButton(int x, int y, int widht, int height, MouseHandler mouseHandler, AlgorithmChooser algorithmChooser, GeneratorChooser generatorChooser) : base(x, y, widht, height, mouseHandler)
        {
            this.algorithmChooser = algorithmChooser;
            this.generatorChooser = generatorChooser;
        }

        protected override void onClick()
        {
            this.algorithmChooser.incraseGeneratorIndexerAndChangeGenerator();
            this.generatorChooser.updateText(this.algorithmChooser);
        }
    }
}
