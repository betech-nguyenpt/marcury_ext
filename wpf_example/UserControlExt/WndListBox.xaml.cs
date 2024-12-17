using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace wpf_example.UserControlExt
{
    /// <summary>
    /// Interaction logic for WndListBox.xaml
    /// </summary>
    public partial class WndListBox : Window
    {
        private ObservableCollection<User> users = new ObservableCollection<User>();
        public WndListBox()
        {
            InitializeComponent();

            users.Add(new User() { Name = "John Doe" });
            users.Add(new User() { Name = "Jane Doe" });

            LbUsers.ItemsSource = users;
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            users.Add(new User() { Name = "New user" });
        }

        private void BtnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (LbUsers.SelectedItem != null)
            {
                (LbUsers.SelectedItem as User).Name = "Random Name";
            }
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (LbUsers.SelectedItem != null) {
                users.Remove(LbUsers.SelectedItem as User);
            }
        }

        public class User : INotifyPropertyChanged
        {
            private string name;
            public string Name
            {
                get { return this.name; }
                set
                {
                    if (this.name != value)
                    {
                        this.name = value;
                        this.NotifyPropertyChanged("Name");
                    }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            public void NotifyPropertyChanged(string propName)
            {
                if (this.PropertyChanged != null) {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
            }
        }
    }
}
