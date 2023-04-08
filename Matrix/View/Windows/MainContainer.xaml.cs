using Matrix.Setting;
using Microsoft.Win32;
using System.Windows.Input;

namespace Matrix.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainContainer.xaml
    /// </summary>
    public partial class MainContainer
    {
        public MainContainer()
        {
            InitializeComponent();
            switch (AppSettings.Default.Theme) 
            {
                case "Dark": Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark); break;
                case "Light": Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light); break;
                default:
                    var value = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", null);
                    switch (value)
                    {
                        case "Dark": Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark); break;
                        case "Light": Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light); break;
                    }
                break;
            }
            var helpCommand = new RoutedCommand();
            helpCommand.InputGestures.Add(new KeyGesture(Key.F1, ModifierKeys.None));
            CommandBindings.Add(new CommandBinding(helpCommand, HelpButton_Click));
            Navigation.F1 = MainFrame;
        }

        private void HelpButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Navigation.OpenHelp();
        }
    }
}
