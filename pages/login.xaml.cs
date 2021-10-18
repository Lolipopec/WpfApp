﻿using System;
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
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : Page
    {
        public login()
        {
            InitializeComponent();
            txtLogin.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
           auth CurrentUser = BaseConnect.BaseModel.auth.FirstOrDefault(x => x.login == txtLogin.Text && x.password == txtPassword.Password);
            if (CurrentUser != null)
            {
                switch (CurrentUser.role)
                {
                    case 1:
                        LoadPages.MainFrame.Navigate(new UserToList());
                        break;
                    case 2:
                    default:
                        LoadPages.MainFrame.Navigate(new info(CurrentUser));
                        break;
                }

            }
            else { MessageBox.Show("Не верный логин или пароль"); }
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            LoadPages.MainFrame.Navigate(new reg(2));
        }
    }
}
