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
    /// Логика взаимодействия для BrandControl.xaml
    /// </summary>
    public partial class BrandControl : UserControl
    {
        public BrandControl()
        {
            InitializeComponent();
            using CosmeticshopContext dc = new();
            ManucturerCombo.ItemsSource = dc.Manufacturers.ToList();
            ManucturerCombo.SelectedIndex = 0;
        }
        public Brand Brand { get; set; }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            using CosmeticshopContext dc = new();
            if (txtBrand.Text?.Trim() == "") { MessageBoxs.Show("Введите производителя!"); }
            else
            {
                var q = ManucturerCombo.SelectedItem as Manufacturer;
                if (Brand == null)
                {
                    Brand brand = new() { BrandName = txtBrand.Text, Manufacturer = q.ManufacturerId };
                    dc.Brands.Add(brand);
                    dc.SaveChanges();
                }
                else
                {
                    Brand.BrandName = txtBrand.Text;
                    Brand.Manufacturer = q.ManufacturerId;
                    dc.Update(Brand);
                    dc.SaveChanges();
                }
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e) => this.Visibility = Visibility.Collapsed;

        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            txtBrand.Text = Brand?.BrandName;
            if (Brand?.Manufacturer != null)
            {
                ManucturerCombo.SelectedIndex = (Brand.Manufacturer - 1);
            }
        }
    }
}
