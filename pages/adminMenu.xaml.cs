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

namespace WpfApp.pages
{
    /// <summary>
    /// Логика взаимодействия для adminMenu.xaml
    /// </summary>
    public partial class adminMenu : Page
    {
        public adminMenu()
        {
            InitializeComponent();
            dgUsers.ItemsSource = BaseConnect.BaseModel.auth.ToList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new EditUser((auth)dgUsers.SelectedItem)); 
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            auth Selecteduser = (auth)dgUsers.SelectedItem;
            BaseConnect.BaseModel.auth.Remove(Selecteduser);
            BaseConnect.BaseModel.SaveChanges();
            MessageBox.Show("Выбранный пользователь удален");
            dgUsers.ItemsSource = BaseConnect.BaseModel.auth.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }
    }
}
