using Matrix.Model;
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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private int UserID;
        private int UserType;
        public OrderPage(int userID, int userType)
        {
            InitializeComponent();
            UserID = userID;
            UserType = userType;
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
            var orders = dc.Orders.Include(q => q.StatusNavigation).Include(w => w.DeliveryTypeNavigation).ToList();
            if (UserType == 1) { orders = orders.Where(u => u.Client == UserID).ToList(); }
            if (StatusCombo.SelectedIndex > 0) orders = orders.Where(u=> u.Status == (StatusCombo.SelectedItem as OrderStatus).OrderStatusId).ToList();
            if (DeliveryCombo.SelectedIndex > 0) orders = orders.Where(u => u.DeliveryType == (DeliveryCombo.SelectedItem as DeliveryType).TypeiD).ToList();
            OrderTable.ItemsSource = orders.ToList();
        }

        private void OrderTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var o = OrderTable.SelectedItem as Order;
            if(o != null)
            Navigation.F2.Navigate(new OrderDetailPage(UserID, o, UserType));
        }

        private void OrderEnterBtn_Click(object sender, RoutedEventArgs e)
        {
            var o = OrderTable.SelectedItem as Order;
            if (o != null) Navigation.F2.Navigate(new OrderDetailPage(UserID, o, UserType));
        }
        
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => Update();
    }
}
