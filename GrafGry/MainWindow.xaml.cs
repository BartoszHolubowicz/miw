using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GrafGry
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Tree<int> myLittleTree;
        private Tree<int> minMaxedTree;

        public MainWindow()
        {
            myLittleTree = null;
            minMaxedTree = null;

            InitializeComponent();
        }

        private int[] TextToIntArray(string text)
        {
            string[] numbers = text.Split(' ');
            int[] result = new int[numbers.Length];

            for (int i = 0; i < result.Length; i++)
                result[i] = int.Parse(numbers[i]);

            return result;
        }

        private void LittleEyeGeneratorButton_Click(object sender, RoutedEventArgs e)
        {
            string startingPlayer = StartingPlayer.SelectedIndex == 0 ? "P" : "A";
            int[] protTokens = TextToIntArray(ProtagonistTokens.Text);
            int[] antTokens = TextToIntArray(AntagonistTokens.Text);
            int endingScore = int.Parse(DrawScore.Text);

            myLittleTree = Tree<int>.GenerateLittleEye(startingPlayer, protTokens, antTokens, endingScore);

            GraphVisCode.Text = myLittleTree.ToString();
        }

        private void MinMaxButton_Click(object sender, RoutedEventArgs e)
        {
            if (myLittleTree != null)
            {
                minMaxedTree = myLittleTree.MinMax();

                GraphVisCode.Text = minMaxedTree.ToString();
            }
        }
    }
}
