using Matrix.Class;
using Matrix.Model;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Matrix.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для AccountAdd.xaml
    /// </summary>
    public partial class AccountAdd : UserControl
    {
        CosmeticshopContext dc = new();
        public AccountAdd()
        {
            InitializeComponent();
            UsertypeCombo.ItemsSource = dc.Usertypes.ToList();
            UsertypeCombo.SelectedIndex = 0;
        }
        //Добавление аккаунта
        private void AccountAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text)) MessageBoxs.Show("Введите имя", "Ошибка", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else if (string.IsNullOrWhiteSpace(SurnameBox.Text)) MessageBoxs.Show("Введите фамилию", "Ошибка", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else if (string.IsNullOrWhiteSpace(EmailBox.Text)) MessageBoxs.Show("Введите адрес электронной почты!", "Ошибка", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else if (string.IsNullOrWhiteSpace(PasswordBox.Password)) MessageBoxs.Show("Введите пароль", "Ошибка", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else
            {
                if (dc.Accounts.Any(x => x.Email == EmailBox.Text)) //Проверка занятости электронной почты
                    MessageBoxs.Show("Данная почта уже занята", "Добавление аккаунта", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
                else
                {
                    Usertype? q = UsertypeCombo.SelectedItem as Usertype;
                    Account ac = new() { Email = EmailBox.Text, Phone = ConverterClass.PhoneConverter(txtPhoneBox.Text), Password = PasswordBox.Password, Name = NameBox.Text, Surname = SurnameBox.Text, Midname = MidnameBox.Text, Usertype = q.UsertypeId,
                        Street = txtStreetBox.Text,
                        House = txtHouseBox.Text,
                        Flat = txtFlatBox.Text,
                    }; 
                    dc.Accounts.Add(ac);
                    dc.SaveChanges();
                    Visibility = Visibility.Collapsed;
                    MessageBoxs.ShowDialog("Добавление прошло успешно!", "Добавление аккаунта");
                }
            }
        }
        //Закрытие окна
        private void CloseBtn_Click(object sender, RoutedEventArgs e) => this.Visibility = Visibility.Collapsed;
        //Сброс введенных данных при закрытии
        private void AccAddGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            EmailBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
            NameBox.Text = string.Empty;
            SurnameBox.Text = string.Empty;
            MidnameBox.Text = string.Empty;
            txtPhoneBox.Text = string.Empty;
            txtHouseBox.Text = string.Empty;
            txtFlatBox.Text = string.Empty;
            txtStreetBox.Text = string.Empty;
        }
    }
}
