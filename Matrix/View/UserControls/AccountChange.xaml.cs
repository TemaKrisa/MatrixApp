using Matrix.Class;
using Matrix.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Matrix.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для AccountChange.xaml
    /// </summary>
    public partial class AccountChange : UserControl
    {
        public static readonly DependencyProperty AccountProp = DependencyProperty.Register("Account1", typeof(Account), typeof(AccountChange), new PropertyMetadata(null));

        public Account? Account1 { get => (Account)GetValue(AccountProp); set => SetValue(AccountProp, value); }

        readonly CosmeticshopContext dc = new();

        public AccountChange()
        {
            InitializeComponent();
            UsertypeCombo.ItemsSource = dc.Usertypes.ToList();
            UsertypeCombo.SelectedIndex = 0;
        }


        private void AccChangeGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)  //Событие заполнения полей
        {
            EmailBox.Text = Account1.Email;
            PasswordBox.Password = Account1.Password;
            MidnameBox.Text = Account1.Midname;
            NameBox.Text = Account1.Name;
            SurnameBox.Text = Account1.Surname;
            UsertypeCombo.SelectedIndex = (Account1.Usertype - 1);
        }

        private async Task ChangeAccount()
        {
            try
            {
                using CosmeticshopContext dc = new();
                if (EmailBox.Text != Account1.Email)
                {
                    if (dc.Accounts.Any(u => u.Email == EmailBox.Text))
                    {
                        MessageBoxs.Show("Данная почта уже занята!", "Изменение аккаунта", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error); return;
                    }
                }
                var q = (Usertype)UsertypeCombo.SelectedItem;
                Account? ac1 = await dc.Accounts.FindAsync(Account1.UserId);
                ac1.Email = EmailBox.Text;
                ac1.Password = PasswordBox.Password;
                ac1.Midname = MidnameBox.Text;
                ac1.Name = NameBox.Text;
                ac1.Surname = SurnameBox.Text;
                ac1.Usertype = q.UsertypeId;
                await dc.SaveChangesAsync();
                this.Visibility = Visibility.Collapsed;
                MessageBoxs.ShowDialog("Изменение прошло успешно!", "Изменение аккаунта");
            }
            catch (Exception ex)
            {
                MessageBoxs.Show(ex.ToString(), "Изменение аккаунта", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e) => this.Visibility = Visibility.Collapsed; //Закрытие окна изменения аккаунта

        private async void ChangeBtn_Click(object sender, RoutedEventArgs e) //Изменение аккаунта
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text)) MessageBoxs.Show("Введите имя", "Ошибка", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else if (string.IsNullOrWhiteSpace(SurnameBox.Text)) MessageBoxs.Show("Введите фамилию", "Ошибка", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else if (string.IsNullOrWhiteSpace(EmailBox.Text)) MessageBoxs.Show("Введите адрес электронной почты!", "Ошибка", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else if (string.IsNullOrWhiteSpace(PasswordBox.Password)) MessageBoxs.Show("Введите пароль", "Ошибка", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else await ChangeAccount();
        }
    }
}
