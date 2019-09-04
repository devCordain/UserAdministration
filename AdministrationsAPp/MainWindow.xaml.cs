﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdministrationsAPp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal List<User> normalUsers = new List<User>();
        internal List<User> adminUsers = new List<User>();
        public MainWindow()
        {
            InitializeComponent();

            // Create listeners
            CreateButton.Click += CreateButton_Click;
            DeleteButton.Click += DeleteButton_Click;
            ChangeButton.Click += ChangeButton_Click;
            ToUserButton.Click += ToUserButton_Click;
            ToAdminButton.Click += ToAdminButton_Click;
            AdminLB.SelectionChanged += AdminLB_SelectionChanged;
            UserLB.SelectionChanged += UserLB_SelectionChanged;

            // Set the source for the ListBoxes and choose display value
            UserLB.ItemsSource = normalUsers;
            AdminLB.ItemsSource = adminUsers;

            UserLB.DisplayMemberPath = "UserName";
            AdminLB.DisplayMemberPath = "UserName";

        }

        private void ToAdminButton_Click(object sender, RoutedEventArgs e)
        {
            if ((UserLB.SelectedItem as User) != null)
            {
                adminUsers.Add(UserLB.SelectedItem as User);
                normalUsers.Remove(UserLB.SelectedItem as User);
                ListBoxRefresh();
                ToAdminButton.IsEnabled = false;
            }
            DeleteButton.IsEnabled = false;
            ChangeButton.IsEnabled = false;
        }

        private void ToUserButton_Click(object sender, RoutedEventArgs e)
        {
            if ((AdminLB.SelectedItem as User) != null)
            {
                normalUsers.Add(AdminLB.SelectedItem as User);
                adminUsers.Remove(AdminLB.SelectedItem as User);
                ListBoxRefresh();
                ToUserButton.IsEnabled = false;
            }
            DeleteButton.IsEnabled = false;
            ChangeButton.IsEnabled = false;

        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminLB.SelectedIndex != -1)
            {
                (AdminLB.SelectedItem as User).UserName = NameTB.Text;
                (AdminLB.SelectedItem as User).UserEmail = EmailTB.Text;
                ListBoxRefresh();
            }
            else if (UserLB.SelectedIndex != -1)
            {
                (UserLB.SelectedItem as User).UserName = NameTB.Text;
                (UserLB.SelectedItem as User).UserEmail = EmailTB.Text;
                ListBoxRefresh();
            }

            NameTB.Text = null;
            EmailTB.Text = null;

            ChangeButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (AdminLB.SelectedIndex != -1)
            {
                adminUsers.Remove((AdminLB.SelectedItem as User));
                ListBoxRefresh();
            }
            else if (UserLB.SelectedIndex != -1)
            {
                normalUsers.Remove((UserLB.SelectedItem as User));
                ListBoxRefresh();
            }

            NameTB.Text = null;
            EmailTB.Text = null;

            ChangeButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTB.Text != "" && EmailTB.Text != "")
            {
                normalUsers.Add(new User(NameTB.Text, EmailTB.Text));
                NameTB.Text = null;
                EmailTB.Text = null;
                ListBoxRefresh();
            }

        }

        /// <summary>
        /// Updates the List Boxes with current users.
        /// </summary>
        private void ListBoxRefresh()
        {
            AdminLB.Items.Refresh();
            UserLB.Items.Refresh();

        }

        private void UserLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                NormalEmailLabel.Content = "User Email: " + (UserLB.SelectedItem as User).UserEmail;
                NameTB.Text = (UserLB.SelectedItem as User).UserName;
                EmailTB.Text = (UserLB.SelectedItem as User).UserEmail;

                ToAdminButton.IsEnabled = true;
                ToUserButton.IsEnabled = false;
                DeleteButton.IsEnabled = true;
                ChangeButton.IsEnabled = true;
            }
            catch
            {
                NormalEmailLabel.Content = "User Email: ";
                NameTB.Text = null;
                EmailTB.Text = null;
            }
        }

        private void AdminLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AdminEmailLabel.Content = "Admin Email: " + (AdminLB.SelectedItem as User).UserEmail;
                NameTB.Text = (AdminLB.SelectedItem as User).UserName;
                EmailTB.Text = (AdminLB.SelectedItem as User).UserEmail;

                ToAdminButton.IsEnabled = false;
                ToUserButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                ChangeButton.IsEnabled = true;
            }
            catch
            {
                AdminEmailLabel.Content = "Admin Email:";
                NameTB.Text = null;
                EmailTB.Text = null;
            }
        }
    }
}