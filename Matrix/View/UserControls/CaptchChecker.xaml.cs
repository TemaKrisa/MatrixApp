using Matrix.Class;
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
    /// Логика взаимодействия для CaptchChecker.xaml
    /// </summary>
    public partial class CaptchChecker : UserControl
    {
        string b; Random r = new();

        public static readonly DependencyProperty LogFailed_Prop = DependencyProperty.Register("LogFailed", typeof(bool), typeof(CaptchChecker), new PropertyMetadata(false));

        public CaptchChecker() => InitializeComponent();

        public bool LogFailed
        {
            get { return (bool)GetValue(LogFailed_Prop); }
            set { SetValue(LogFailed_Prop, value); }
        }
        //Обновление данных капчи
        public void Update()
        {
            CaptchBox.Text = "";
            SPanelSymbols.Children.Clear();
            CanvasNoise.Children.Clear();
            GenerateSymbols(4);
            GenerateNoise(15);
        }
        //Генерация символов для ввода капчи
        private void GenerateSymbols(int count)
        {
            b = "";
            var col = (SolidColorBrush)App.Current.Resources["TextColor"];
            string alphabet = "QWERTYUIPASDFGHJKLZXCVBNM23456789";
            StringBuilder bldr = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                string symbol = alphabet.ElementAt(r.Next(0, alphabet.Length)).ToString();
                TextBlock lbl = new()
                {
                    Foreground = col,
                    Text = symbol,
                    FontSize = r.Next(40, 80),
                    RenderTransform = new RotateTransform(r.Next(-45, 45)),
                    Margin = new Thickness(20, 0, 20, 0)
                };
                SPanelSymbols.Children.Add(lbl);
                bldr.Append(symbol);
            }
            b = bldr.ToString();
        }

        //Генерация шума капчи
        private void GenerateNoise(int volumeNoise)
        {
            for (int i = 0; i < volumeNoise; i++)
            {
                Border border = new Border
                {
                    Background = new SolidColorBrush(Color.FromArgb((byte)r.Next(100, 200), (byte)r.Next(0, 256), (byte)r.Next(0, 256), (byte)r.Next(0, 256))),
                    Height = r.Next(2, 10),
                    Width = r.Next(10, 150),
                    RenderTransform = new RotateTransform(r.Next(0, 360))
                };
                CanvasNoise.Children.Add(border);
                Canvas.SetLeft(border, r.Next(0, 300));
                Canvas.SetTop(border, r.Next(0, 150));

                Ellipse ellipse = new Ellipse
                {
                    Fill = new SolidColorBrush(Color.FromArgb((byte)r.Next(100, 200), (byte)r.Next(0, 256), (byte)r.Next(0, 256), (byte)r.Next(0, 256))),
                    Height = r.Next(20, 40),
                    Width = r.Next(20, 40)
                };
                CanvasNoise.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, r.Next(0, 200));
                Canvas.SetTop(ellipse, r.Next(0, 100));
            }
        }

        private void CaptchGrid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) => Update();

        private void UpdateCaptch_Click(object sender, RoutedEventArgs e) => Update();

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchBox.Text != b) MessageBoxs.Show("Капча введена неверно", "Ошибка ввода", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            else
            {
                LogFailed = true;
                this.Visibility = Visibility.Collapsed;
            }
        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e) => Visibility = Visibility.Collapsed;
    }
}
