using Matrix.Setting;
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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        Account? account = new();

        readonly CosmeticshopContext dc = new();

        private int userID;

        public ProfilePage(int id)
        {
            InitializeComponent();
            userID = id;
        }

        private void UpdateData()
        {
            account = dc.Accounts.Where(u => u.UserId == userID).SingleOrDefault();
            txtFIO.DataContext = account;
            txtEmail.DataContext = account;
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxs.ShowDialog("Вы точно хотите выйти?", "Профиль", MessageBoxs.Buttons.YesNo, MessageBoxs.Icon.Default) == "1")
            {
                AppSettings.Default.IsPasSaved = false;
                AppSettings.Default.Save();
                Navigation.F1.Navigate(new LoginPage());
            }
        }

        private void ProfilePage_Loaded(object sender, RoutedEventArgs e) => UpdateData();
        //Передача данных текущего аккаунта окну изменения ФИО
        private void FIOChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            FIOChangeControl.UserID = userID;
            FIOChangeControl.Visibility = Visibility.Visible;
        }
        //Обновление данных при закрытии окна изменения ФИО
        private void FIOChangeControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (FIOChangeControl.Visibility == Visibility.Collapsed) NavigationService.Navigate(new ProfilePage(userID));
        }
        //Обновление данных при закрытии окна изменения пароля
        private void PasChangeControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (PasChangeControl.Visibility == Visibility.Collapsed) NavigationService.Navigate(new ProfilePage(userID));
        }
        //Передача данных текущего аккаунта окну изменения пароля
        private void ChangePasBtn_Click(object sender, RoutedEventArgs e)
        {
            PasChangeControl.UserID2 = userID;
            PasChangeControl.Visibility = Visibility.Visible;
        }
    }
}
