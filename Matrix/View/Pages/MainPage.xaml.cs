using Matrix.Setting;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private int UserType { get; set; }
        private int UserId { get; set; }

        public MainPage(int usertype, int userId)
        {
            InitializeComponent();
            Navigation.F2 = RootFrame;
            UserType = usertype; UserId = userId;
            Logining();
        }
        public MainPage()
        {
            InitializeComponent();
            Navigation.F2 = RootFrame;
            UserType = 0; UserId = 0;
            Logining();
        }

        private void Logining()
        {
            if (AppSettings.Default.IsPasSaved) //Автоматическая авторизация в случае если пароль сохранен
            {
                try
                {
                    using CosmeticshopContext dc = new();
                    Account? userObj = dc.Accounts.FirstOrDefault(x => x.Email == AppSettings.Default.Login && x.Password == HashClass.HashPassword(AppSettings.Default.Password)); //Проверка введенного логина и почты
                    if (userObj != null)
                    {
                        UserType = userObj.Usertype;
                        UserId = userObj.UserId;
                    }
                }
                catch (Exception ex) { _ = MessageBoxs.ShowDialog(ex.ToString()); }
            }

            if (UserType == null)
            {
                UserId = 0;
            }
            switch (UserType)
            {
                case 0:
                    LoginBtn.Visibility = Visibility.Visible;
                    ProductBtn.Visibility = Visibility.Visible;
                    AccountBtn.Visibility = Visibility.Collapsed;
                    ProfileBtn.Visibility = Visibility.Collapsed;
                    CartBtn.Visibility = Visibility.Collapsed;
                    ManufacturerBtn.Visibility = Visibility.Collapsed;
                    BrandBtn.Visibility = Visibility.Collapsed;
                    CategoryBtn.Visibility = Visibility.Collapsed;
                    ReportBtn.Visibility = Visibility.Collapsed;
                    OrderBtn.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    LoginBtn.Visibility = Visibility.Collapsed;
                    ProductBtn.Visibility = Visibility.Visible;
                    AccountBtn.Visibility = Visibility.Collapsed;
                    ProfileBtn.Visibility = Visibility.Visible;
                    CartBtn.Visibility = Visibility.Visible;
                    ManufacturerBtn.Visibility = Visibility.Collapsed;
                    BrandBtn.Visibility = Visibility.Collapsed;
                    CategoryBtn.Visibility = Visibility.Collapsed;
                    ReportBtn.Visibility = Visibility.Collapsed;
                    OrderBtn.Visibility = Visibility.Visible;
                    break;
                case 2:
                    LoginBtn.Visibility = Visibility.Collapsed;
                    ProductBtn.Visibility = Visibility.Visible;
                    AccountBtn.Visibility = Visibility.Collapsed;
                    ProfileBtn.Visibility = Visibility.Visible;
                    CartBtn.Visibility = Visibility.Collapsed;
                    ManufacturerBtn.Visibility = Visibility.Visible;
                    BrandBtn.Visibility = Visibility.Visible;
                    CategoryBtn.Visibility = Visibility.Visible;
                    ReportBtn.Visibility = Visibility.Collapsed;
                    OrderBtn.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    LoginBtn.Visibility = Visibility.Collapsed;
                    ProductBtn.Visibility = Visibility.Collapsed;
                    AccountBtn.Visibility = Visibility.Collapsed;
                    ProfileBtn.Visibility = Visibility.Visible;
                    CartBtn.Visibility = Visibility.Collapsed;
                    ManufacturerBtn.Visibility = Visibility.Visible;
                    BrandBtn.Visibility = Visibility.Collapsed;
                    CategoryBtn.Visibility = Visibility.Collapsed;
                    ReportBtn.Visibility = Visibility.Collapsed;
                    OrderBtn.Visibility = Visibility.Visible;
                    break;
                case 4:
                    LoginBtn.Visibility = Visibility.Collapsed;
                    ProductBtn.Visibility = Visibility.Collapsed;
                    AccountBtn.Visibility = Visibility.Visible;
                    ProfileBtn.Visibility = Visibility.Visible;
                    CartBtn.Visibility = Visibility.Collapsed;
                    ManufacturerBtn.Visibility = Visibility.Collapsed;
                    BrandBtn.Visibility = Visibility.Collapsed;
                    CategoryBtn.Visibility = Visibility.Collapsed;
                    ReportBtn.Visibility = Visibility.Visible;
                    OrderBtn.Visibility = Visibility.Collapsed;
                    break;
            }
                 ProductBtn.IsChecked = true;
        }

        //Выделение кнопки раздела в случае перехода в соответсвующий раздел
        private void RootFrame_Navigated(object sender, NavigationEventArgs e)
        {
            switch (Navigation.F2.Content?.ToString())
            {
                case "AppealsService.View.Pages.RequestPage": break;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e) { if (Navigation.F2.CanGoBack) Navigation.F2.GoBack(); }

        //Определение доступности кнопки назад
        private void RootFrame_ContentRendered(object sender, EventArgs e) => BackBtn.IsEnabled = Navigation.F2.CanGoBack;

        //Определение типа пользователя и отображение соответсвующих типу пользователя разделов
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void LoginBtn_Checked(object sender, RoutedEventArgs e) => Navigation.F1.Navigate(new LoginPage());

        private void SettBtn_Checked(object sender, RoutedEventArgs e) => Navigation.F2.Navigate(new SettingsPage());

        private void ProductBtn_Checked(object sender, RoutedEventArgs e) => Navigation.F2.Navigate(new ProductPage(UserId, UserType));

        private void CartBtn_Checked(object sender, RoutedEventArgs e) => Navigation.F2.Navigate(new CartPage(UserId));

        private void AccountBtn_Checked(object sender, RoutedEventArgs e) => Navigation.F2.Navigate(new AccountPage());

        private void ProfileBtn_Checked(object sender, RoutedEventArgs e) => Navigation.F2.Navigate(new ProfilePage(UserId));

        private void CategoryBtn_Checked(object sender, RoutedEventArgs e) => Navigation.F2.Navigate(new CategoryPage());

        private void ManufacturerBtn_Checked(object sender, RoutedEventArgs e) => Navigation.F2.Navigate(new ManufacturerPage());

        private void BrandBtn_Checked(object sender, RoutedEventArgs e)
        {
            Navigation.F2.Navigate(new BrandPage());
        }

        private void OrderPage_Checked(object sender, RoutedEventArgs e)
        {
            Navigation.F2.Navigate(new OrderPage(UserId,UserType));

        }

        private void ReportBtn_Checked(object sender, RoutedEventArgs e)
        {
            Navigation.F2.Navigate(new ReportPage());
        }
    }
}
