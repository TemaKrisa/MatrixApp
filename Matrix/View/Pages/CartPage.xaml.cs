using Matrix.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        private int UserID;
        public CartPage(int userID)
        {
            InitializeComponent();
            UserID = userID;
            Update();
        }

        private async Task<List<Cart>> GetCartItemsAsync()
        {
            using CosmeticshopContext dc = new();
            return await dc.Carts.Include(c => c.Product).Include(c => c.Product.CategoryNavigation).Where(u=>u.UserId == UserID).ToListAsync();
        }

        private async Task<decimal> CalculateTotalCostAsync(List<Cart> cartItems)
        {
            decimal totalCost = await Task.Run(() => cartItems.Sum(c => c.Product.TotalPrice * c.Count));
            return totalCost;
        }

        private async void Update()
        {
            var cartItems = await GetCartItemsAsync();
            decimal totalCost = await CalculateTotalCostAsync(cartItems);
            CartList.ItemsSource = cartItems;
            txtCartTotal.Text = $"{totalCost:F2} ₽";
        }

        private TextBox? tb;
        private DispatcherTimer updateTimer;

        private void txtProductCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb = sender as TextBox;
            if (updateTimer == null)
            {
                updateTimer = new DispatcherTimer();
                updateTimer.Interval = TimeSpan.FromSeconds(0.5);
                updateTimer.Tick += UpdateTimer_Tick;
            }
            updateTimer.Stop();
            updateTimer.Start();
        }

        private async void UpdateTimer_Tick(object sender, EventArgs e)
        {
            updateTimer.Stop();
            if (tb == null || !(tb.DataContext is Cart selectedItem)) return;
            if (!int.TryParse(tb.Text, out int newQuantity)) return;
            using (CosmeticshopContext dc = new())
            {
                var q = dc.Products.Find(selectedItem.ProductId).Count;
                if (q < newQuantity) newQuantity = q;
                selectedItem.Count = newQuantity;

                var tt = dc.Carts.SingleOrDefault(u=> u.ProductId == selectedItem.ProductId && u.UserId == selectedItem.UserId);
                tt.Count = newQuantity;
                dc.Update(tt);
                dc.SaveChanges();
                Update();
            }
        }

        private async void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            using CosmeticshopContext dc = new();
            if (!(sender is Button button) || !(button.DataContext is Cart selectedItem)) return;
            var result = MessageBox.Show("Вы точно хотите удалить товар?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;
            dc.Carts.Remove(selectedItem);
            await dc.SaveChangesAsync();
            Update();
        }

        private void txtProductCount_PreviewTextInput(object sender, TextCompositionEventArgs e) => e.Handled = !char.IsDigit(e.Text[0]) ? true : false;

        private void CreateOrderBtn_Click(object sender, RoutedEventArgs e) => Navigation.F2.Navigate(new CreateOrderPage(UserID));
    }
}
