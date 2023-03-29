using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateOrderPage.xaml
    /// </summary>
    public partial class CreateOrderPage : Page
    {
        private int UserID;
        public CreateOrderPage(int userID)
        {
            InitializeComponent();
            using CosmeticshopContext dc = new();
            UserID = userID;
            DeliveryPointCombo.ItemsSource = dc.DeliveryPoints.ToList();
            DeliveryPointCombo.SelectedIndex = 0;
            DeliveryTypeCombo.ItemsSource = dc.DeliveryTypes.ToList();
            DeliveryTypeCombo.SelectedIndex = 0;
        }

        private async void CreateOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            await CreateOrderAsync();
            //CreateOrderBtn.IsEnabled = false;
            //using CosmeticshopContext dc = new();
            //var cartItems = dc.Carts.Include(c => c.Product).Include(c => c.Product.CategoryNavigation).ToList();
            //decimal totalCost = cartItems.Sum(c => c.Product.TotalPrice * c.Count);
            //var q = (DeliveryType)DeliveryTypeCombo.SelectedItem;
            //var order = new Order()
            //{
            //    Client = UserID,
            //    OrderDate = DateOnly.FromDateTime(DateTime.Now),
            //    DeliveryType = q.TypeiD,
            //    Status = 1,
            //    TotalSum = totalCost
            //};
            //dc.Add(order);
            //foreach (var cartItem in cartItems)
            //{
            //    var orderProduct = new OrderProduct
            //    {
            //        Order = order.OrderId,
            //        Product = cartItem.ProductId,
            //        Count = cartItem.Count,
            //    };
            //    dc.OrderProducts.Add(orderProduct);
            //    dc.Remove(cartItem);
            //    var qq = dc.Products.Single(u => u.ProductId == cartItem.ProductId);
            //    qq.Count -= cartItem.Count;
            //}
            //dc.SaveChanges();
            //CreateOrderBtn.IsEnabled = true;
            //Navigation.F2.Navigate(new OrderPage(UserID));
        }

        private async Task CreateOrderAsync()
        {
            CreateOrderBtn.IsEnabled = false;
            using (CosmeticshopContext dc = new())
            {
                var cartItems = await dc.Carts.Include(c => c.Product).Include(c => c.Product.CategoryNavigation).ToListAsync();
                decimal totalCost = cartItems.Sum(c => c.Product.TotalPrice * c.Count);
                var deliveryType = (DeliveryType)DeliveryTypeCombo.SelectedItem;
                var order = new Order
                {
                    Client = UserID,
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    DeliveryType = deliveryType.TypeiD,
                    Status = 1,
                    TotalSum = totalCost
                };
                dc.Add(order);
                await dc.SaveChangesAsync();
                foreach (var cartItem in cartItems)
                {
                    var orderProduct = new OrderProduct
                    {
                        Order = order.OrderId,
                        Product = cartItem.ProductId,
                        Count = cartItem.Count,
                    };
                    dc.OrderProducts.Add(orderProduct);
                    var product = await dc.Products.FindAsync(cartItem.ProductId);
                    product.Count -= cartItem.Count;
                }
                dc.Carts.RemoveRange(cartItems);
                await dc.SaveChangesAsync();
            }
            CreateOrderBtn.IsEnabled = true;
            Navigation.F2.Navigate(new OrderPage(UserID, 1));
        }

        private void DeliveryTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(DeliveryTypeCombo.SelectedIndex)
            {
                case 1: DeliveryPointCombo.IsEnabled = false; break;
                case 0: DeliveryPointCombo.IsEnabled = true;  break;
            }
        }
    }
}
