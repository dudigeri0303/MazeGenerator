
namespace MazeGenerator
{
    public class GeneratorChooserLeftButton : LeftButton
    {
        private AlgorithmChooser algorithmChooser;
        private GeneratorChooser generatorChooser;

        public GeneratorChooserLeftButton(int x, int y, int widht, int height, MouseHandler mouseHandler, AlgorithmChooser algorithmChooser, GeneratorChooser generatorChooser) : base(x, y, widht, height, mouseHandler)
        {
            this.algorithmChooser = algorithmChooser;
            this.generatorChooser = generatorChooser;
        }

        protected override void onClick()
        {

            this.algorithmChooser.decraseGeneratorIndexerAndChangeGenerator();
            this.generatorChooser.updateText(this.algorithmChooser);
        }
    }
}
