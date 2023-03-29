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
    /// Логика взаимодействия для OrderDetailPage.xaml
    /// </summary>
    public partial class OrderDetailPage : Page
    {
        Order Order;
        public OrderDetailPage(int userID, Order order, int userType)
        {
            InitializeComponent();
            using CosmeticshopContext dc = new();
            var o = dc.OrderProducts.Include(u => u.ProductNavigation).Include(u => u.ProductNavigation.CategoryNavigation).ToList();
            OrderList.ItemsSource = o.ToList();
            if (userType != 1)
            {
                DeliveryPanel.Visibility = Visibility.Visible;
                var d = dc.Accounts.Find(order.Client);
                ClientName.Text = "Клиент: " + d.Surname + " " + d.Name;
                if (order.DeliveryType == 2)
                {
                    ClientAdress.Text = "Адресс доставки" + d.Street + " " + d.House + " " + d.House;
                    ClientPhone.Text = "Телефон: " + d.Phone;
                }
                else
                {
                    var w = dc.DeliveryPoints.FirstOrDefault();
                    DeliveryTxt.Text = "Точка доставки: " + w.Street + " " + w.House;
                }
                StatusCombo.ItemsSource = dc.OrderStatuses.ToList();

                Order = order;
                StatusCombo.SelectedIndex = (order.Status - 1);
            }
            else DeliveryPanel.Visibility = Visibility.Collapsed;
        }

        private async void StatusCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private async void Update()
        {
            using (CosmeticshopContext dc = new CosmeticshopContext())
            {
                var q = dc.Orders.Find(Order.OrderId);
                q.Status = (StatusCombo.SelectedItem as OrderStatus).OrderStatusId;
                dc.Orders.Update(q);
                await dc.SaveChangesAsync();
            }
        }
    }
}
