using System.Windows;
using System.Windows.Controls;

namespace Matrix.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ProfilePasswordChange.xaml
    /// </summary>
    public partial class ProfilePasswordChange : UserControl
    {
        public static readonly DependencyProperty userID2 = DependencyProperty.Register("UserID2", typeof(int), typeof(ProfileFIOChange), new FrameworkPropertyMetadata(null));

        public ProfilePasswordChange() => InitializeComponent();

        public int UserID2 { get => (int)GetValue(userID2); set => SetValue(userID2, value); }

        private Account? account;

        private void ProfPasGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            using CosmeticshopContext dc = new(); 
            if (this.Visibility == Visibility.Visible) account = dc.Accounts.Find(UserID2);
        }

        private void PasChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            using CosmeticshopContext dc = new();
            if (HashClass.HashPassword(PasswordBox.Password) == account.Password)
            {
                if (string.IsNullOrWhiteSpace(NewPasswordBox.Password)) MessageBoxs.ShowDialog("Введите новый пароль!", "Изменение пароля", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
                else
                {
                    account.Password = HashClass.HashPassword(NewPasswordBox.Password);
                    dc.Update(account);
                    dc.SaveChanges();
                    MessageBoxs.ShowDialog("Изменение прошло успешно", "Изменение пароля", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Info);
                    this.Visibility = Visibility.Collapsed;
                }
            }
            else MessageBoxs.ShowDialog("Неверный текущий пароль!", "Изменение пароля", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
        }
        //Закрытие окна
        private void CloseBtn_Click(object sender, RoutedEventArgs e) => this.Visibility = Visibility.Collapsed;
    }
}
