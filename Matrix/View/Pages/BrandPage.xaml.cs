using Microsoft.EntityFrameworkCore;
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

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для BrandPage.xaml
    /// </summary>
    public partial class BrandPage : Page
    {
        public BrandPage()
        {
            InitializeComponent();
            UpdateData();
        }
        private void UpdateData()
        {
            using CosmeticshopContext dc = new();
            BrandGrid.ItemsSource = dc.Brands.Include(u=> u.ManufacturerNavigation).ToList();
        }

        private void BrandCtrl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (BrandCtrl.Visibility == Visibility.Collapsed) { UpdateData(); }
        }

        private void BrandGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BrandCtrl.Brand = (Brand)BrandGrid.SelectedItem;
            BrandCtrl.Visibility = Visibility.Visible;
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            BrandCtrl.Brand = null;
            BrandCtrl.Visibility = Visibility.Visible;
        }

        private void ChangeItem_Click(object sender, RoutedEventArgs e)
        {
            BrandCtrl.Brand = (Brand)BrandGrid.SelectedItem;
            BrandCtrl.Visibility = Visibility.Visible;
        }
    }
}
