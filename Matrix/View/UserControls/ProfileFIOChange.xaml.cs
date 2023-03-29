using System.Windows;
using System.Windows.Controls;

namespace Matrix.View.UserControls
{
    public partial class ProfileFIOChange : UserControl
    {
        public static readonly DependencyProperty userID = DependencyProperty.Register("UserID", typeof(int), typeof(ProfileFIOChange), new FrameworkPropertyMetadata(null));

        public ProfileFIOChange() => InitializeComponent();

        public int UserID { get => (int)GetValue(userID); set => SetValue(userID, value); }

        private Account? account;

        private void FIOChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            using CosmeticshopContext dc = new();
            if (HashClass.HashPassword(PasswordBox.Password) == account.Password)
            {
                if (string.IsNullOrWhiteSpace(SurnameBox.Text)) MessageBoxs.Show("Введите фамилию!", "Ошибка", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
                else if (string.IsNullOrWhiteSpace(NameBox.Text)) MessageBoxs.Show("Введите имя!", "Ошибка", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
                else
                {
                    account.Surname = SurnameBox.Text;
                    account.Midname = MidnameBox.Text;
                    account.Name = NameBox.Text;
                    dc.Accounts.Update(account);
                    dc.SaveChanges();
                    MessageBoxs.ShowDialog("Изменение прошло успешно", "Изменение данных учетной записи", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Info);
                    this.Visibility = Visibility.Collapsed;
                }
            }
            else MessageBoxs.Show("Неверный пароль", "Ошибка", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
        }

        private void ProfileFIOGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Отображение данных текущего аккаунта в профиле
            using CosmeticshopContext dc = new();
            if (Visibility == Visibility.Visible)
            {
                account = dc.Accounts.Find(UserID);
                SurnameBox.Text = account.Surname;
                NameBox.Text = account.Name;
                MidnameBox.Text = account.Midname;
            }
            else UserID = 0;
        }
        //Закрытие окна
        private void CloseBtn_Click(object sender, RoutedEventArgs e) => this.Visibility = Visibility.Collapsed;
    }
}
