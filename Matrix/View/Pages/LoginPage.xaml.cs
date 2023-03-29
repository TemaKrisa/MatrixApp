using Matrix.Setting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.Text;

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        //Данный метод выполняет авторизацию в случае успешного ввода капчи
        private async void Logining(string login, string password)
        {
            try
            {
                using CosmeticshopContext dc = new();
                Account? userObj = await dc.Accounts.FirstOrDefaultAsync(x => x.Email == login && x.Password == HashClass.HashPassword(password)); //Проверка введенного логина и почты
                if (userObj != null)
                {
                    if (SavePasCheck.IsChecked == true)
                    {
                        AppSettings.Default.Password = password;
                        AppSettings.Default.Login = login;
                        AppSettings.Default.IsPasSaved = true;
                    }
                    else
                    {
                        AppSettings.Default.Password = null;
                        AppSettings.Default.Login = null;
                        AppSettings.Default.IsPasSaved = false;
                    }
                    AppSettings.Default.Save();
                    Navigation.F1.Navigate(new MainPage(userObj.Usertype, userObj.UserId));
                }
                else
                    MessageBoxs.ShowDialog("Такого пользователя не существует!", "Авторизация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            }
            catch (Exception ex) { _ = MessageBoxs.ShowDialog(ex.ToString()); }
        }

        public void LoginCheck(string Password, string Email) //Данный метод проверяет заполненность данных авторизации, после чего отображает капчу
        {
            if (string.IsNullOrWhiteSpace(Email)) MessageBoxs.ShowDialog("Введите адрес электронной почты!", "Авторизация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else if (string.IsNullOrWhiteSpace(Password)) MessageBoxs.ShowDialog("Введите пароль!", "Авторизация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else CaptchControl.Visibility = Visibility.Visible;
        }

        private void PasBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pas = (PasswordBox)e.Source;
            if (pas != null && pas.Template.FindName("RevealedPassword", pas) is TextBox textBox)
                textBox.Text = pas.Password;
        }

        //Попытка авторизации после успешного ввода капчи
        private void CaptchControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (CaptchControl.Visibility == Visibility.Collapsed && CaptchControl.LogFailed) Logining(LoginBox.Text, PasBox.Password);
        }

        private void RegClick_Click(object sender, RoutedEventArgs e) => Navigation.F1.Navigate(new RegistrationPage()); //Открытие страницы регистрации

        private void LoginBtn_Click(object sender, RoutedEventArgs e) => LoginCheck(PasBox.Password, LoginBox.Text);

        private void FrgtLink_Click(object sender, RoutedEventArgs e) => resPas.Visibility = Visibility.Visible; //Открытие окна востановления пароля

        private void resPas_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (resPas.Visibility == Visibility.Collapsed && resPas.IsSent)
            {
                emailCheck.EmailCheck = resPas.ResCode;
                emailCheck.Visibility = Visibility.Visible;
            }
        }

        private void emailCheck_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (emailCheck.Visibility == Visibility.Collapsed && emailCheck.LogFailed)
            {
                resNewPas.GetResEmail = resPas.ResEmail;
                resNewPas.Visibility = Visibility.Visible;
            }
        }

        private void LogWithoutAccount_Click(object sender, RoutedEventArgs e)
        {
            Navigation.F1.Navigate(new MainPage(0,0));
        }
    }
}
