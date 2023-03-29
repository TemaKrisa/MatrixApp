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
    /// Логика взаимодействия для ManufacturerPage.xaml
    /// </summary>
    public partial class ManufacturerPage : Page
    {
        public ManufacturerPage()
        {
            InitializeComponent();
            UpdateData();
        }

        private void UpdateData()
        {
            using CosmeticshopContext dc = new();
            ManufacturerGrid.ItemsSource = dc.Manufacturers.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ManChange.Manufacture = null;
            ManChange.Visibility = Visibility.Visible;
        }

        private void ChangeItem_Click(object sender, RoutedEventArgs e)
        {
            ManChange.Manufacture = (Manufacturer)ManufacturerGrid.SelectedItem;
            ManChange.Visibility = Visibility.Visible;
        }

        private void ManufacturerGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ManChange.Manufacture = (Manufacturer)ManufacturerGrid.SelectedItem;
            ManChange.Visibility = Visibility.Visible;
        }

        private void ManChange_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(ManChange.Visibility == Visibility.Collapsed) { UpdateData(); }
        }
    }
}
