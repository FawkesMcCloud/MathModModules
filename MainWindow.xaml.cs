using MathModMudules.UCS;
using System.Windows;


namespace MathModMudules
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AboutUC about = new AboutUC();
        Modules modules = new Modules();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = modules;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataContext = about;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
