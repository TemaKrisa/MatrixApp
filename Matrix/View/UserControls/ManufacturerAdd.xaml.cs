using System.Windows;
using System.Windows.Controls;

namespace Matrix.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ManufacturerAdd.xaml
    /// </summary>
    public partial class ManufacturerAdd : UserControl
    {
        public ManufacturerAdd()
        {
            InitializeComponent();
        }

        public Manufacturer Manufacture  { get; set; }


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            using CosmeticshopContext dc = new();
            if (txtManufacturer.Text?.Trim() == "") { MessageBoxs.Show("Введите производителя!"); }
            else
            {
                if (Manufacture == null)
                {
                    Manufacturer manufacturer = new Manufacturer() { ManufacturerName = txtManufacturer.Text };
                    dc.Manufacturers.Add(manufacturer);
                }
                else
                {
                    Manufacture.ManufacturerName = txtManufacturer.Text;
                    dc.Update(Manufacture);
                }
                dc.SaveChanges();
                this.Visibility = Visibility.Collapsed;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void Grid_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) => txtManufacturer.Text = Manufacture?.ManufacturerName;
    }
}
