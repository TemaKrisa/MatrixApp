using Microsoft.Office.Interop.Excel;
using System;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;


namespace Matrix.Class
{
    internal class ExportClass
    {
        public static void ExportToExcel(DataGrid dt, int min) //Данный метод отвечает за экспорт DataGrid в Excel
        {
            try
            {
                Application excel = new();
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                //Заполнение ячеек
                for (int i = 0; i < dt.Columns.Count - min; i++)
                {
                    Excel.Range myRange = sheet1.Cells[1, i + 1];
                    myRange.Value2 = dt.Columns[i].Header;
                    for (int j = 0; j < dt.Items.Count; j++)
                    {
                        TextBlock? b = dt.Columns[i].GetCellContent(dt.Items[j]) as TextBlock;
                        Excel.Range myRange2 = sheet1.Cells[j + 2, i + 1];
                        myRange2.Value2 = b.Text;
                    }
                }
                //Установка рамок
                Excel.Range tRange = sheet1.UsedRange;
                tRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                tRange.Borders.Weight = XlBorderWeight.xlThick;
                //Установление автоматического размера ячеек
                sheet1.Columns.AutoFit();
                //Открытие Excel
                excel.Visible = true;
            }
            catch (Exception ex) { MessageBoxs.Show(ex.ToString()); }
        }
    }
}
