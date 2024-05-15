using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Kursovoy.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        MainWindow mainWindow;
        public Registration(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void RegIn(object sender, RoutedEventArgs e)
        {
            Regex regexEmail = new Regex(@"\A[^@]+@([^@\.]+\.)+[^@\.]+\z");
            MatchCollection matchesEmail = regexEmail.Matches(email.Text);
            if (matchesEmail.Count > 0 && email.Text.Length > 0)
            {
                Regex regexPassword = new Regex(@"([A-Z]|[0-9]){6,}");
                MatchCollection matchesPassword = regexPassword.Matches(password.Text);
                if (matchesPassword.Count > 0 && password.Text.Length > 0)
                {
                    if (password.Text == password_2.Text)
                    {
                        Regex regexFio = new Regex(@"[А-Яа-я]{3,}");
                        MatchCollection matchesFio = regexFio.Matches(fio.Text);
                        if (matchesFio.Count > 0 && fio.Text.Length > 0)
                        {
                            Regex regexPhone = new Regex(@"7[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]");
                            MatchCollection matchesPhone = regexPhone.Matches(phone.Text);
                            if (matchesPhone.Count > 0 && phone.Text.Length > 0)
                            {
                                mainWindow.Query($"INSERT INTO `parents`(`email`, `password`, `fio`, `phone`, `role`) VALUES ('{email.Text}','{password.Text}','{fio.Text}','{phone.Text}','0');");
                                MessageBox.Show("Регистрация прошла успешно! Зайдите в аккаунт с главного окна!");
                                mainWindow.OpenPages(MainWindow.pages.authorization);
                            }
                            else MessageBox.Show("Пожалуйста, укажите телефон в формате 70000000000", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
                        else MessageBox.Show("Пожалуйста, укажите ФИО на русском языке и без цифр", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    else MessageBox.Show("Пожалуйста, правильность введенных паролей", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else MessageBox.Show("Пароль должен содержать латинские заглавные буквы, цифры, не менее 6 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else MessageBox.Show("Пожалуйста, укажите email в формате xxx@xxx.xx", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPages(MainWindow.pages.authorization);
        }
    }
}
