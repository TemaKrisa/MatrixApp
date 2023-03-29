using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductDetailPage.xaml
    /// </summary>
    public partial class ProductDetailPage : Page
    {
        Product product;
        Cart? CurCart;
        private int UserID;
        public ProductDetailPage(Product pr1, int userID)
        {
            InitializeComponent();
            product = pr1;
            UserID = userID;
            // Устанавливаем контекст данных для страницы
            DataContext = product;
            if (userID > 0) LoadData();
            else CartAddBtn.IsEnabled = false;
        }

        private async void LoadData()
        {
            using CosmeticshopContext dc = new();

            // Получаем элемент корзины для текущего пользователя и выбранного продукта
            CurCart = dc.Carts.Where(u => u.UserId == UserID && u.ProductId == product.ProductId).SingleOrDefault();

            // Если элемент корзины существует, скрываем кнопку добавления в корзину и отображаем панель корзины
            if (CurCart != null)
            {
                CartAddBtn.Visibility = Visibility.Collapsed;
                CartAddPanel.Visibility = Visibility.Visible;
                txtProductCart.Text = CurCart.Count.ToString();
            }

            if (product.Discount == 0)
            {
                txtTotalPrice.Visibility = Visibility.Collapsed;
            }
            else { txtTotalPrice.Visibility = Visibility.Visible; }
            // Получаем данные о бренде, категории и производителе продукта
            txtBrand.Text = dc.Brands.FirstOrDefault(u => u.BrandId == product.Brand)?.BrandName;
            txtCategory.Text = dc.ProductCategories.FirstOrDefault(u => u.CategoryId == product.Category)?.CategoryName;
            txtManufacturer.Text = dc.Manufacturers.FirstOrDefault(u => u.ManufacturerId == product.Manufacturer)?.ManufacturerName;
        }

        private async void CartAddBtn_Click(object sender, RoutedEventArgs e)
        {
            CartAddBtn.IsEnabled = false;
            if(UserID == 0) { MessageBoxs.Show("Для покупки товара необходимо авторизоваться!","Покупка товара", MessageBoxs.Buttons.OK, MessageBoxs.Icon.Error); }
            else
            {
            using CosmeticshopContext dc = new();
            if (product.Count > 0)
            {
                Cart c1 = new() { Count = 1, ProductId = product.ProductId, UserId = UserID };
                CurCart = c1;
                CartAddBtn.Visibility = Visibility.Collapsed;
                CartAddPanel.Visibility = Visibility.Visible;
                await dc.Carts.AddAsync(c1);
                await dc.SaveChangesAsync();
                CartAddBtn.IsEnabled = true;
            }
            else MessageBoxs.Show("Данный продукт закончился!", "Покупка товара");
            }
        }

        private async void CartDecBtn_Click(object sender, RoutedEventArgs e)
        {
            using CosmeticshopContext dc = new();
            CartDecBtn.IsEnabled = false;
            if(CurCart.Count != 1)  
            {
                CurCart.Count -= 1;
                txtProductCart.Text = CurCart.Count.ToString();
                await dc.SaveChangesAsync();
            }
            else
            {
                dc.Carts.Remove(CurCart);
                CartAddBtn.Visibility = Visibility.Visible;
                CartAddPanel.Visibility = Visibility.Collapsed;
                await dc.SaveChangesAsync();
            }
            CartDecBtn.IsEnabled = true;
        }

        private async void CartIncBtn_Click(object sender, RoutedEventArgs e)
        {
            using CosmeticshopContext dc = new();
            CartIncBtn.IsEnabled = false;
            if (CurCart.Count > product.Count)
            {
                CurCart.Count = product.Count;
            }
            else
            {
                CurCart.Count += 1;
                txtProductCart.Text = CurCart.Count.ToString();
                dc.Carts.Update(CurCart);
                await dc.SaveChangesAsync();
            }
            CartIncBtn.IsEnabled = true;
        }

        private void CartEnterBtn_Click(object sender, RoutedEventArgs e) => Navigation.F2.Navigate(new CartPage(UserID));
    }
}
