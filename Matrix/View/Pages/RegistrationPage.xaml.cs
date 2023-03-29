using Matrix.View.UserControls;
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

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage() => InitializeComponent();

        private async void RegBtn_Click(object sender, RoutedEventArgs e) //Проверка данных регистрации с использованием класса RegCheck
        {
            switch (RegistrationClass.RegCheck(RegSurBox.Text, RegNameBox.Text, EmailBox.Text, PasBox.Password))
            {
                case "EmptySurname": MessageBoxs.ShowDialog("Введите фамилию!", "Регистрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error); break;
                case "InvalidSurname": MessageBoxs.ShowDialog("Длинна фамилии не должна превышать 25 символов!", "Регистрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error); break;
                case "EmptyName": MessageBoxs.ShowDialog("Введите имя!", "Регистрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error); break;
                case "InvalidName": MessageBoxs.ShowDialog("Длинна имени не должна превышать 25 символов!", "Регистрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error); break;
                case "EmptyEmail": MessageBoxs.ShowDialog("Введите адрес электронной почты!", "Регистрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error); break;
                case "EmptyPassword": MessageBoxs.ShowDialog("Введите пароль!", "Регистрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error); break;
                case "InvalidPassword": MessageBoxs.ShowDialog("Длинна пароля не должна превышать 25 символов!", "Регистрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error); break;
                case "NameIsDigit": MessageBoxs.ShowDialog("Имя не должно содержать цифры!", "Регистрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error); break;
                case "SurnameIsDigit": MessageBoxs.ShowDialog("Фамилия не должна содержать цифры!", "Регистрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error); break;
                case "Ok": Registration(); break;

            }
        }

        private void Registration()
        {
            using CosmeticshopContext dc = new();
            if (!dc.Accounts.Any(x => x.Email == EmailBox.Text)) //Проверка существует ли учетная запись с введенной электронной почтой
            {
                Account ac1 = new() { Email = EmailBox.Text, Password = HashClass.HashPassword(PasBox.Password), Name = RegNameBox.Text, Surname = RegSurBox.Text, Midname = RegMidBox.Text, Usertype = 1, Phone=ConverterClass.PhoneConverter(PhoneBox.Text), Flat = txtFlatBox.Text, House = txtHouseBox.Text, Street =txtStreetBox.Text };
                try
                {
                    EmailCheck.EmailCheck = EmailClass.EmailCheck(EmailBox.Text); //Отправка сообщения проверки на введенную электронную почту
                    EmailCheck.Visibility = Visibility.Visible;
                }
                catch { MessageBoxs.ShowDialog("Введён неверный адрес электронной почты!", "Регистрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error); }
            }
            else MessageBoxs.ShowDialog("Данная почта уже занята!", "Регистрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
        }

        private void EmailCheck_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            using CosmeticshopContext dc = new();
            if (EmailCheck.Visibility != Visibility.Collapsed || !EmailCheck.LogFailed) return;
            Account ac1 = new() { Email = EmailBox.Text, Password = HashClass.HashPassword(PasBox.Password), Name = RegNameBox.Text, Surname = RegSurBox.Text, Midname = RegMidBox.Text, Usertype = 1 };
            dc.Accounts.Add(ac1); //Добавлеие аккаунта
            dc.SaveChanges();
            MessageBoxs.ShowDialog("Регистрация успешно завершена", "Регитрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Info);
            Navigation.F1.Navigate(new LoginPage()); //Возврат на страницу авторизации
        }

        private void LogLink_Click(object sender, RoutedEventArgs e) => Navigation.F1.Navigate(new LoginPage()); //Возврат к странице авторизации
    }
}

