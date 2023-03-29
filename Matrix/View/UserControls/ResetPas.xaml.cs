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

namespace Matrix.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ResetPas.xaml
    /// </summary>
    public partial class ResetPas : UserControl
    {
        public ResetPas() => InitializeComponent();

        public static readonly DependencyProperty resEmail = DependencyProperty.Register("ResEmail", typeof(string), typeof(EmailChecker), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty resCode = DependencyProperty.Register("ResCode", typeof(string), typeof(EmailChecker), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty isSent = DependencyProperty.Register("IsSent", typeof(bool), typeof(EmailChecker), new FrameworkPropertyMetadata(null));

        public string ResEmail { get => (string)GetValue(resEmail); set => SetValue(resEmail, value); }

        public string ResCode { get => (string)GetValue(resCode); set => SetValue(resCode, value); }

        public bool IsSent { get => (bool)GetValue(isSent); set => SetValue(isSent, value); }

        // Отправка кода на указанную почту
        private void EmailEnterBtn_Click(object sender, RoutedEventArgs e)
        {
            CosmeticshopContext dc = new();
            if (dc.Accounts.Any(u => u.Email == EmailCheckTxt.Text))
            {
                ResEmail = EmailCheckTxt.Text;
                ResCode = EmailClass.ResetPas(ResEmail);
                IsSent = true;
                this.Visibility = Visibility.Collapsed;
            }
            else MessageBoxs.Show("Аккаунта с указанной почтой не существует!", "Востановление пароля", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
        }

        private void EmailGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) => EmailCheckTxt.Text = null;

        private void EmailCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            IsSent = false;
            this.Visibility = Visibility.Collapsed;
        }
    }
}
