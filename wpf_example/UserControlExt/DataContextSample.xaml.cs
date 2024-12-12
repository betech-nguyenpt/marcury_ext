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

namespace wpf_example.UserControlExt
{
    /// <summary>
    /// Interaction logic for DataContextSample.xaml
    /// </summary>
    public partial class DataContextSample : UserControl
    {
        public DataContextSample()
        {
            InitializeComponent();
            this.DataContext = Window.GetWindow(this);
        }
    }
}
