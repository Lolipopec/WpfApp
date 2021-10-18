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
    /// Логика взаимодействия для UserToList.xaml
    /// </summary>
    public partial class UserToList : Page
    {
        List<users> users;
        public UserToList()
        {
            InitializeComponent();
            users = BaseConnect.BaseModel.users.ToList();
            lbUsers.ItemsSource = users;
            List<genders> genders = BaseConnect.BaseModel.genders.ToList();
            cbGenderS.ItemsSource = genders;
            cbGenderS.SelectedValuePath = "id";
            cbGenderS.DisplayMemberPath = "gender";
        }
        private void lbTraits_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock lb = (TextBlock)sender;
            int id = Convert.ToInt32(lb.Uid);
            List <users_to_traits> l= BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == id).ToList();
            foreach (var a in l)
            {
                lb.Text += a.traits.trait +"; ";
            }
        }
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            auth CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.id == id);
            //LoadPages.MainFrame.Navigate(new ChangeUser(CurrentUser));
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            auth CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.id == id);
            BaseConnect.BaseModel.auth.Remove(CurrentUser);
            BaseConnect.BaseModel.SaveChanges();
            MessageBox.Show("Пользователь успешно удален!");
        }

        private void ListSort_Selected(object sender, RoutedEventArgs e)
        {
            if (ListSort.SelectedIndex == 0)
            {
                DateGB.Visibility = Visibility.Visible;
                StartGB.Visibility = Visibility.Collapsed;
                LogGB.Visibility = Visibility.Collapsed;
                FinishGB.Visibility = Visibility.Collapsed;
                Gend.Visibility = Visibility.Collapsed;
                NameGB.Visibility = Visibility.Collapsed;
            }
            if (ListSort.SelectedIndex == 1)
            {
                DateGB.Visibility = Visibility.Collapsed;
                StartGB.Visibility = Visibility.Visible;
                LogGB.Visibility = Visibility.Collapsed;
                FinishGB.Visibility = Visibility.Visible;
                Gend.Visibility = Visibility.Collapsed;
                NameGB.Visibility = Visibility.Collapsed;
            }
            if (ListSort.SelectedIndex == 2)
            {
                DateGB.Visibility = Visibility.Collapsed;
                StartGB.Visibility = Visibility.Collapsed;
                LogGB.Visibility = Visibility.Visible;
                FinishGB.Visibility = Visibility.Collapsed;
                Gend.Visibility = Visibility.Collapsed;
                NameGB.Visibility = Visibility.Collapsed;
            }
            if (ListSort.SelectedIndex == 4)
            {
                DateGB.Visibility = Visibility.Collapsed;
                StartGB.Visibility = Visibility.Collapsed;
                LogGB.Visibility = Visibility.Collapsed;
                FinishGB.Visibility = Visibility.Collapsed;
                Gend.Visibility = Visibility.Visible;
                NameGB.Visibility = Visibility.Collapsed;
            }
            if (ListSort.SelectedIndex == 3)
            {
                DateGB.Visibility = Visibility.Collapsed;
                StartGB.Visibility = Visibility.Collapsed;
                LogGB.Visibility = Visibility.Collapsed;
                FinishGB.Visibility = Visibility.Collapsed;
                Gend.Visibility = Visibility.Collapsed;
                NameGB.Visibility = Visibility.Visible;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new login());
        }

        private void bntOldVers_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new adminMenu());
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            List<users> listUsers = users;
            if (ListSort.SelectedIndex == 0)
            {
                listUsers = users.Where(x => x.dr == (DateTime)dpDate.SelectedDate).ToList();
            }
            if (ListSort.SelectedIndex == 1)
            {
                int start = Convert.ToInt32(tbStart.Text) - 1;
                int finish = Convert.ToInt32(tbFinish.Text);
                listUsers = users.Skip(start).Take(finish - start).ToList();
            }
            if (ListSort.SelectedIndex == 2)
            {
                auth user =BaseConnect.BaseModel.auth.FirstOrDefault(x => x.login == tbLogin.Text);
                listUsers = users.Where(x => x.id == user.id).ToList();
            }
            if (ListSort.SelectedIndex == 3)
            {
                listUsers = users.Where(x => x.name == tbName.Text).ToList();
            }
            if (ListSort.SelectedIndex == 4)
            {
                listUsers = users.Where(x => x.gender == Convert.ToInt32(cbGenderS.SelectedValue)).ToList();
            }
            lbUsers.ItemsSource = listUsers;
        }

        private void btnRset_Click(object sender, RoutedEventArgs e)
        {
            lbUsers.ItemsSource = users;
        }

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new reg());
        }
    }
}
