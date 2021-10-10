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
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Page
    {
        public EditUser(auth SelectedUser)
        {
            InitializeComponent();
            try
            {
                txtLog.Text = SelectedUser.login;
                txtPass.Password = SelectedUser.password;
            }
            catch { }
            users Old = BaseConnect.BaseModel.users.FirstOrDefault(x => x.id == SelectedUser.id);
            if (Old != null)
            {
                try
                {
                    txtName.Text = Old.name;
                }
                catch { }
                try
                {
                    txtDr.Text = Old.dr.ToString("yyyy MM dd");
                }
                catch { }
                
                    listGenders.ItemsSource = BaseConnect.BaseModel.genders.ToList();
                    listGenders.SelectedValuePath = "id";
                    listGenders.DisplayMemberPath = "gender";
                    listGenders.IsSe
                try
                {
                    List<users_to_traits> LUTT = BaseConnect.BaseModel.users_to_traits.Where(x => x.id_user == Old.id).ToList();
                    foreach (users_to_traits UTT in LUTT)
                    {
                        if (UTT.traits.trait == cb1.Content.ToString())
                        {
                            cb1.IsChecked = true;
                        }
                        if (UTT.traits.trait == cb2.Content.ToString())
                        {
                            cb2.IsChecked = true;
                        }
                        if (UTT.traits.trait == cb3.Content.ToString())
                        {
                            cb3.IsChecked = true;
                        }
                    }
                }
                catch { }
            }


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //auth logPass = new auth() { login = txtLog.Text, password = txtPass.Password, role = 2 };
            //BaseConnect.BaseModel.auth.Add(logPass);
            //BaseConnect.BaseModel.SaveChanges();

            //users user = new users { name = txtName.Text, id = logPass.id, gender = (int)listGenders.SelectedValue, dr = (DateTime)date.SelectedDate };
            //BaseConnect.BaseModel.users.Add(user);

            //if (cb1.IsChecked == true)
            //{
            //    users_to_traits UTT = new users_to_traits();
            //    UTT.id_user = user.id;
            //    UTT.id_trait = 1;
            //    BaseConnect.BaseModel.users_to_traits.Add(UTT);
            //}
            //if (cb2.IsChecked == true)
            //{
            //    users_to_traits UTT = new users_to_traits();
            //    UTT.id_user = user.id;
            //    UTT.id_trait = 2;
            //    BaseConnect.BaseModel.users_to_traits.Add(UTT);
            //}
            //if (cb3.IsChecked == true)
            //{
            //    users_to_traits UTT = new users_to_traits();
            //    UTT.id_user = user.id;
            //    UTT.id_trait = 3;
            //    BaseConnect.BaseModel.users_to_traits.Add(UTT);
            //}
            //BaseConnect.BaseModel.SaveChanges();
            //MessageBox.Show("Успех");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.GoBack();
        }
    }
}
