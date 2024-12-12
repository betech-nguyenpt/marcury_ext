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
using wpf_example.Panels;
using wpf_example.UserControlExt;

namespace wpf_example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello, world!");

        }

        private void BtnCanvas_Click(object sender, RoutedEventArgs e)
        {
            CanvasPanel wnd = new();
            wnd.Show();
        }

        private void BtnCanvas2_Click(object sender, RoutedEventArgs e)
        {
            CanvasPanel2 wnd = new();
            wnd.Show();
        }

        private void BtnWrap_Click(object sender, RoutedEventArgs e)
        {
            WrapsPanel wnd = new();
            wnd.Show();
        }

        private void BtnWrap2_Click(object sender, RoutedEventArgs e)
        {
            WrapsPanel2 wnd = new();
            wnd.Show();
        }

        private void BtnStack_Click(object sender, RoutedEventArgs e)
        {
            StacksPanel wnd = new();
            wnd.Show();
        }

        private void BtnStack2_Click(object sender, RoutedEventArgs e)
        {
            StacksPanel2 wnd = new();
            wnd.Show();
        }

        private void BtnDock_Click(object sender, RoutedEventArgs e)
        {
            DocksPanel wnd = new();
            wnd.Show();
        }

        private void BtnDock2_Click(object sender, RoutedEventArgs e)
        {
            DocksPanel2 wnd = new();
            wnd.Show();
        }

        private void BtnGrid_Click(object sender, RoutedEventArgs e)
        {
            GridsPanel wnd = new();
            wnd.Show();
        }

        private void BtnGrid2_Click(object sender, RoutedEventArgs e)
        {
            GridsPanel2 wnd = new();
            wnd.Show();
        }

        private void BtnGrid3_Click(object sender, RoutedEventArgs e)
        {
            GridsPanel3 wnd = new();
            wnd.Show();
        }

        private void BtnGrid4_Click(object sender, RoutedEventArgs e)
        {
            GridsPanel4 wnd = new();
            wnd.Show();
        }

        private void BtnGrid5_Click(object sender, RoutedEventArgs e)
        {
            GridsPanel5 wnd = new();
            wnd.Show();
        }

        private void BtnGrid6_Click(object sender, RoutedEventArgs e)
        {
            GridsPanel6 wnd = new();
            wnd.Show();
        }

        private void BtnGrid7_Click(object sender, RoutedEventArgs e)
        {
            GridsPanel7 wnd = new();
            wnd.Show();
        }

        private void BtnUserControl_Click(object sender, RoutedEventArgs e)
        {
            WndUserControl wnd = new();
            wnd.Show();
        }
    }
}