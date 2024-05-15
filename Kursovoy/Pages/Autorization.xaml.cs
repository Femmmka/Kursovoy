using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Kursovoy.Classes;



namespace Kursovoy.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        MainWindow mainWindow;
        //int IdParent;
        public Authorization(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            if (File.Exists(mainWindow.localPath + @"\save.txt") && File.ReadAllText(mainWindow.localPath + @"\save.txt") != "")
            {
                StreamReader sr = new StreamReader(mainWindow.localPath + @"\save.txt");
                try
                {
                    email.Text = sr.ReadLine();
                    password.Password = sr.ReadLine();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка сохранения", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            Regex regexEmail = new Regex(@"\A[^@]+@([^@\.]+\.)+[^@\.]+\z");
            MatchCollection matchesEmail = regexEmail.Matches(email.Text);
            if (matchesEmail.Count > 0 && email.Text.Length > 0)
            {
                if (password.Password.Length > 0)
                {
                    MySqlDataReader user = mainWindow.Query($"SELECT * FROM `parents` WHERE (email = '{email.Text}' AND password = '{password.Password}')");

                    if (user == null) return;
                    if (user.Read())
                    {
                        UserId.id = Convert.ToInt32(user.GetValue(0).ToString());

                        mainWindow.userInfo.id = Convert.ToInt32(user.GetValue(0).ToString());
                        mainWindow.userInfo.email = user.GetValue(1).ToString();
                        mainWindow.userInfo.password = user.GetValue(2).ToString();
                        mainWindow.userInfo.fio = user.GetValue(3).ToString();
                        mainWindow.userInfo.phone = user.GetValue(4).ToString();
                        mainWindow.userInfo.role = Convert.ToInt32(user.GetValue(5).ToString());

                        if (save.IsChecked == true)
                        {
                            StreamWriter sw = new StreamWriter(mainWindow.localPath + @"\save.txt");
                            sw.WriteLine(email.Text);
                            sw.WriteLine(password.Password);
                            sw.Close();
                        }

                        user.Close();
                        if (mainWindow.userInfo.role == 0)
                        {
                            mainWindow.OpenPages(MainWindow.pages.main);
                        }
                        else if (mainWindow.userInfo.role == 1) mainWindow.OpenPages(MainWindow.pages.admin);
                    }
                    else MessageBox.Show("Такой пользователь не был найден.", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else MessageBox.Show("Введите пароль", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else MessageBox.Show("Введите логин", "Warning", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void RegInClick(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPages(MainWindow.pages.registration);
        }
    }
}
