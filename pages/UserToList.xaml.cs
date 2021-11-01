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
            btnCreateUser.Content = "Создать\nпользователя";
            bntOldVers.Content = "Cтарая\nверсия";
        }
        private void lbTraits_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock lb = (TextBlock)sender;
            int id = Convert.ToInt32(lb.Uid);
            lb.Text = "";
            List<users_to_traits> l = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == id).ToList();
            foreach (var a in l)
            {
                lb.Text += a.traits.trait + "; ";
            }
        }
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            auth CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.id == id);
            LoadPages.MainFrame.Navigate(new EditUser(CurrentUser));
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Uid);
            auth CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.id == id);
            BaseConnect.BaseModel.auth.Remove(CurrentUser);
            BaseConnect.BaseModel.SaveChanges();
            MessageBox.Show("Пользователь успешно удален!");
            users = BaseConnect.BaseModel.users.ToList();
            lbUsers.ItemsSource = users;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new login());
        }

        private void bntOldVers_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new adminMenu());
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            List<users> listUsers = users.ToList();
            try
            {
                int start = Convert.ToInt32(tbStart.Text) - 1;
                int finish = Convert.ToInt32(tbFinish.Text);
                listUsers = listUsers.Skip(start).Take(finish - start).ToList();
            }
            catch { }
            if (tbLogin.Text != "")
            {
                List<users> listUser = listUsers.ToList();
                listUser.Clear();
                foreach (users l in listUsers)
                {
                    if (l.auth.login.Contains(tbLogin.Text))
                    {
                        listUser.Add(l);
                    }
                }
                listUsers.Clear();
                listUsers = listUser;
            }
            if (tbName.Text != "")
            {
                listUsers = listUsers.Where(x => x.name.Contains(tbName.Text)).ToList();
            }
            if (dpDate.SelectedDate != null) { listUsers = listUsers.Where(x => x.dr == (DateTime)dpDate.SelectedDate).ToList(); }
            if (cbGenderS.SelectedValue != null)
            {
                listUsers = listUsers.Where(x => x.gender == Convert.ToInt32(cbGenderS.SelectedValue)).ToList();
            }
            lbUsers.ItemsSource = listUsers;
        }

        private void btnRset_Click(object sender, RoutedEventArgs e)
        {
            users = BaseConnect.BaseModel.users.ToList();
            lbUsers.ItemsSource = users;
            tbName.Text = "";
            tbLogin.Text = "";
            cbGenderS.SelectedValue = null;
            dpDate.SelectedDate = null;
            tbStart.Text = "";
            tbFinish.Text = "";
        }

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new reg(1));
        }
        private void tbStart_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
        int CurrentPages = 1;
        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                List<users> lb = users.ToList();
                TextBlock tb = (TextBlock)sender;
                int CountZapInList = users.Count;
                int CountZapOnPage = Convert.ToInt32(txtPageCount.Text);
                int CountPage;
                if (CountZapOnPage % 2 == 0)
                {
                    CountPage = (CountZapInList / CountZapOnPage);
                }
                else 
                {
                    CountPage = (CountZapInList / CountZapOnPage) + 1;
                }
                switch (tb.Uid)
                {
                    case "prev":
                        CurrentPages--;
                        break;
                    case "1":
                        if (CurrentPages < 3) CurrentPages = 1;
                        else if (CurrentPages > CountPage) CurrentPages = CountPage - 4;
                        else CurrentPages -= 2;
                        break;
                    case "2":
                        if (CurrentPages < 3) CurrentPages = 2;
                        else if (CurrentPages > CountPage) CurrentPages = CountPage - 3;
                        else CurrentPages -= 1;
                        break;
                    case "3":
                        if (CurrentPages < 3) CurrentPages = 3;
                        else if (CurrentPages > CountPage) CurrentPages = CountPage - 2;
                        break;
                    case "4":
                        if (CurrentPages < 3) CurrentPages = 4;
                        else if (CurrentPages > CountPage) CurrentPages = CountPage - 1;
                        else CurrentPages++;
                        break;
                    case "5":
                        if (CurrentPages < 3) CurrentPages = 5;
                        else if (CurrentPages > CountPage) CurrentPages = CountPage;
                        else CurrentPages += 2;
                        break;
                    case "next":
                        CurrentPages++;
                        break;
                    default:
                        CurrentPages = 1;
                        break;
                }

                if (CurrentPages < 1) CurrentPages = 1;
                if (CurrentPages > CountPage) CurrentPages = CountPage;
                //отрисовка
                if (CountPage < 5)
                {
                    txt5.Visibility = Visibility.Collapsed;
                }
                else if (CountPage < 4)
                {
                    txt4.Visibility = Visibility.Collapsed;
                }
                else if (CountPage < 3)
                {
                    txt3.Visibility = Visibility.Collapsed;
                }
                else if (CountPage < 2)
                {
                    txt2.Visibility = Visibility.Collapsed;
                }
                else
                {
                    txt2.Visibility = Visibility.Visible;
                    txt3.Visibility = Visibility.Visible;
                    txt4.Visibility = Visibility.Visible;
                    txt5.Visibility = Visibility.Visible;
                }
                if (CurrentPages < 3)
                {
                    txt1.Text = " 1 ";
                    txt2.Text = " 2 ";
                    txt3.Text = " 3 ";
                    txt4.Text = " 4 ";
                    txt5.Text = " 5 ";
                }  
                else if (CurrentPages > CountPage - 2)
                {
                    txt1.Text = " " + (CountPage - 4).ToString() + " ";
                    txt2.Text = " " + (CountPage - 3).ToString() + " ";
                    txt3.Text = " " + (CountPage - 2).ToString() + " ";
                    txt4.Text = " " + (CountPage - 1).ToString() + " ";
                    txt5.Text = " " + (CountPage).ToString() + " ";
                }
                else 
                {
                    txt1.Text = " " + (CurrentPages - 2).ToString() + " ";
                    txt2.Text = " " + (CurrentPages - 1).ToString() + " ";
                    txt3.Text = " " + (CurrentPages).ToString() + " ";
                    txt4.Text = " " + (CurrentPages + 1).ToString() + " ";
                    txt5.Text = " " + (CurrentPages + 2).ToString() + " ";

                }
                txtCurentPage.Text = "Текущая страница: " + (CurrentPages).ToString();

                lb = users.Skip(CurrentPages * CountZapOnPage - CountZapOnPage).Take(CountZapOnPage).ToList();
                lbUsers.ItemsSource = lb;
            }
            catch
            {
                //null
            }
        }

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<users> lb = users.ToList();
            try
            {
                if (txtPageCount.Text == "")
                {
                    lb = users.ToList();
                }
                else
                    lb = users.Take(Convert.ToInt32(txtPageCount.Text)).ToList();

                lbUsers.ItemsSource = lb;
            }
            catch
            {
                //null
            }
        }
    }
}
