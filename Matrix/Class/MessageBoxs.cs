using Matrix.View.Windows;
using System.Windows;
using System.Windows.Media.Effects;

namespace Matrix.Class
{
    public class MessageBoxs
    {
        public enum Buttons { YesNo, OK }

        public enum Icon { Error, Info, Default }

        public static string Show(string Text) { return Show(Text, "", Buttons.OK, Icon.Default); }

        public static string Show(string text, string Title) { return Show(text, Title, Buttons.OK, Icon.Default); }

        public static string Show(string Text, string Title, Buttons buttons, Icon icon)
        {
            MessageBoxW messageBox = new(Text, Title, buttons, icon);
            messageBox.Show();
            return messageBox.ReturnString;
        }

        public static string ShowDialog(string Text) { return ShowDialog(Text, "", Buttons.OK, Icon.Default); }

        public static string ShowDialog(string Text, string Title) { return ShowDialog(Text, Title, Buttons.OK, Icon.Default); }

        public static string ShowDialog(string Text, string Title, Buttons buttons) { return ShowDialog(Text, Title, buttons, Icon.Default); }

        public static string ShowDialog(string Text, string Title, Buttons buttons, Icon icon)
        {
            ShowBlurEffectAllWindow();
            MessageBoxW messageBox = new(Text, Title, buttons, icon);
            messageBox.ShowDialog();
            StopBlurEffectAllWindow();
            return messageBox.ReturnString;
        }

        static readonly BlurEffect MyBlur = new();

        public static void ShowBlurEffectAllWindow() //Данный метод отвечает за включение эффекта размытия при отображении message box
        {
            MyBlur.Radius = 15;
            foreach (Window window in Application.Current.Windows)
                window.Effect = MyBlur;
        }
        public static void StopBlurEffectAllWindow() => MyBlur.Radius = 0; //Данный метод отвечает за отключение эффекта размытия
    }
}
