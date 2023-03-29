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
    /// Логика взаимодействия для CategoryControl.xaml
    /// </summary>
    public partial class CategoryControl : UserControl
    {
        public CategoryControl() => InitializeComponent();

        public ProductCategory Category { get; set; }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            using CosmeticshopContext dc = new();
            if (txtCategory.Text?.Trim() == "") { MessageBoxs.Show("Введите производителя!"); }
            else
            {
                if (Category == null)
                {
                    ProductCategory category = new() { CategoryName = txtCategory.Text };
                    dc.ProductCategories.Add(category);
                }
                else
                {
                    Category.CategoryName = txtCategory.Text;
                    dc.Update(Category);
                }
                dc.SaveChanges();
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e) => this.Visibility = Visibility.Collapsed;

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) => txtCategory.Text = Category?.CategoryName;
    }
}
