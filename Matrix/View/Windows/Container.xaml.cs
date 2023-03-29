using Matrix.Class;
using Matrix.Setting;
using System;
using System.Windows;
using System.Windows.Input;

namespace Matrix.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для Container.xaml
    /// </summary>
    public partial class Container : Window
    {
        public Container()
        {
            InitializeComponent();
            // Set up hotkey for help button
            var helpCommand = new RoutedCommand();
            helpCommand.InputGestures.Add(new KeyGesture(Key.F1, ModifierKeys.None));
            CommandBindings.Add(new CommandBinding(helpCommand, HelpBtn_Click));
            Navigation.F1 = WFrame;
            // Установка выбранной темы
            Application.Current.Resources.MergedDictionaries[0].Source = new Uri($"/Style/Themes/{AppSettings.Default.Theme}.xaml", UriKind.RelativeOrAbsolute); ;
        }

        //Данное событие открывает файл справки расположенный в директории программы
        private void HelpBtn_Click(object sender, RoutedEventArgs e) => Navigation.OpenHelp();

        private void MaximizeRestoreClick(object sender, RoutedEventArgs e) => this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;

        private void MinimizeClick(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

        private void CloseClick(object sender, RoutedEventArgs e) => this.Close();
    }
}
