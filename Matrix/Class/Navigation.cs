using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.IO;

namespace Matrix.Class
{
    public static class Navigation
    {
        private static Frame f1 = new(); //фрейм главного окна контейнера
        private static Frame f2 = new(); //Фрейм страницы главного меню
        public static Frame F2 { get => f2; set => f2 = value; }
        public static Frame F1 { get => f1; set => f1 = value; }

        public static void OpenHelp() //Данный метод открывает файл справки расположенный в директории программы
        {
            try
            {
                using System.Diagnostics.Process? proc = new();
                proc.StartInfo.FileName = Path.Combine(Environment.CurrentDirectory, "Matrix.chm");
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
            catch (Exception ex)
            {
                MessageBoxs.Show(ex.ToString(), "Ошибка", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error);
            }
        }
    }
}
