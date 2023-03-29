using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using CosmeticshopContext dc = new();
            var os = dc.OrderStatuses.ToList();
            os.Insert(0, new OrderStatus() { OrderStatusName = "Все" });
            StatusCombo.ItemsSource = os.ToList();
            StatusCombo.SelectedIndex = 0;
            var delC = dc.DeliveryTypes.ToList();
            delC.Insert(0, new DeliveryType() { TypeName = "Все" });
            DeliveryCombo.ItemsSource = delC;
            DeliveryCombo.SelectedIndex = 0;
            Update();
        }

        private void Update()
        {
            using CosmeticshopContext dc = new();
            var table = dc.Orders.Include(q => q.StatusNavigation).Include(w => w.DeliveryTypeNavigation).Include(r => r.ClientNavigation).ToList();
            if(StatusCombo.SelectedIndex > 0)
                table = table.Where(u=> u.Status == (StatusCombo.SelectedItem as OrderStatus).OrderStatusId).ToList();         
            if(DeliveryCombo.SelectedIndex > 0)
                table = table.Where(u=> u.DeliveryType == (DeliveryCombo.SelectedItem as DeliveryType).TypeiD).ToList();
            ReportTable.ItemsSource = table;
        }

        private void ExcelBtn_Click(object sender, RoutedEventArgs e) => ExportClass.ExportToExcel(ReportTable,0);

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => Update();
    }
}
