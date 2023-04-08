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
    /// Логика взаимодействия для EmailChecker.xaml
    /// </summary>
    public partial class EmailChecker : UserControl
    {
        public static readonly DependencyProperty emailCheck = DependencyProperty.Register("EmailCheck", typeof(string), typeof(EmailChecker), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty LogFailedProp = DependencyProperty.Register("LogFailed", typeof(bool), typeof(EmailChecker), new PropertyMetadata(false));

        public EmailChecker() => InitializeComponent();

        public string EmailCheck { get => (string)GetValue(emailCheck); set => SetValue(emailCheck, value); }

        public bool LogFailed { get => (bool)GetValue(LogFailedProp); set => SetValue(LogFailedProp, value); }

        private void EmailEnterBtn_Click(object sender, RoutedEventArgs e)
        {
            //Проверка соответсвия отправленного на указанную электронную почту кода с введённым
            LogFailed = EmailCheckBox.Text == EmailCheck ? true : false;
            Visibility = LogFailed ? Visibility.Collapsed : Visibility.Visible;
            if (!LogFailed) MessageBoxs.Show("Введённый код подтверждения неверный!", "Регитрация", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);

        }

        private void EmailGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) { if (Visibility == Visibility.Visible) LogFailed = false; }

        private void EmailCancelBtn_Click(object sender, RoutedEventArgs e) => this.Visibility = Visibility.Collapsed;

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
