using Matrix.Class;
using Matrix.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Matrix.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ResetPasNewPas.xaml
    /// </summary>
    public partial class ResetPasNewPas : UserControl
    {
        public static readonly DependencyProperty resEmail = DependencyProperty.Register("GetResEmail", typeof(string), typeof(EmailChecker), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty resCode = DependencyProperty.Register("GetResCode", typeof(string), typeof(EmailChecker), new FrameworkPropertyMetadata(null));

        public string GetResEmail { get => (string)GetValue(resEmail); set => SetValue(resEmail, value); }

        public string GetResCode { get => (string)GetValue(resCode); set => SetValue(resCode, value); }

        public ResetPasNewPas() => InitializeComponent();

        private void EmailEnterBtn_Click(object sender, RoutedEventArgs e)
        {
            using CosmeticshopContext dc = new();
            //Проверка соответсвия паролей
            if (String.Compare(NewPas.Password, NewPasConfirm.Password) != 0) MessageBoxs.Show("Пароли не совпадают!", "Востановление пароля");
            else
            {
                //Смена пароля
                var q = dc.Accounts.Single(u => u.Email == GetResEmail);
                q.Password = NewPasConfirm.Password;
                dc.Accounts.Update(q);
                dc.SaveChanges();
                MessageBoxs.Show("Пароль успешно изменён!", "Востановление пароля");
                Visibility = Visibility.Collapsed;
            }
        }
        //Закрытие окна
        private void CloseBtn_Click(object sender, RoutedEventArgs e) => Visibility = Visibility.Collapsed;
        //Обнуление полей при закрытии окна
        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            NewPas.Password = null;
            NewPasConfirm.Password = null;
        }
    }
}
