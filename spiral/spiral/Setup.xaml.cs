using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace spiral
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Setup : Window
    {
        public int GapWidth { get; set; }
        public int LineLength { get; set; }
        public Setup()
        {
            InitializeComponent();
        }

        private void LoopButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetInputValues(out int gapWidth, out int lineLength))
            {
                Spiral spiral = new Spiral(true, lineLength, gapWidth);
                spiral.Show();
                this.Close();
            }
        }

        private void RecursionButton_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetInputValues(out int gapWidth, out int lineLength))
            {
                Spiral spiral = new Spiral(false, lineLength, gapWidth);
                spiral.Show();
                this.Close();
            }
        }

        private bool TryGetInputValues(out int gapWidth, out int lineLength)
        {
            gapWidth = 0;
            lineLength = 0;

            if (int.TryParse(GapWidthTextBox.Text, out gapWidth) &&
                int.TryParse(LineLengthTextBox.Text, out lineLength))
            {
                if (gapWidth <= 0 || lineLength <= 0)
                {
                    MessageBox.Show("Please enter valid numeric values greater than 0");
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show("Please enter valid numeric values for Gap Width and Line Length.");
                return false;
            }
        }
    }
}