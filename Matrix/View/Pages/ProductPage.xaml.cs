using Matrix.Class;
using Matrix.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private int UserID;
        private int UserType;
        public ProductPage(int userID, int userType)
        {
            InitializeComponent();
            LoadData();
            UserID = userID;
            UserType = userType;
        }

        private async void LoadData()
        {
            using CosmeticshopContext dc = new();
            var Brand = dc.Brands.ToList();
            var Category = dc.ProductCategories.ToList();    
            
            Brand.Insert(0, new Brand { BrandName = "Все бренды" });
            Category.Insert(0, new ProductCategory { CategoryName = "Все категории" });
            
            BrandCombo.ItemsSource = Brand;
            FilterCombo.ItemsSource = new List<string>() { "По наименованию", "Сначала акционные" };
            CategoryCombo.ItemsSource = Category;

            BrandCombo.SelectedIndex = 0;
            CategoryCombo.SelectedIndex = 0;
            FilterCombo.SelectedIndex = 0;

            await UpdateData();
        }
        private async Task UpdateData()
        {
            ProductAddBtn.Visibility = UserType != 2 ? Visibility.Collapsed : Visibility.Visible;
            using CosmeticshopContext dc = new();
            var Prod = dc.Products.ToList();
            if(FilterCombo.SelectedIndex > 0)
            {
                switch (FilterCombo.SelectedIndex)
                {
                    case 0: Prod = Prod.OrderBy(u=> u.ProductName).ToList();     break;
                    case 1: Prod = Prod.OrderByDescending(u => u.Discount).ToList(); break;
                }
            }
            if(BrandCombo.SelectedIndex > 0)
            {
                Prod = Prod.Where(u => u.Brand == 
                (BrandCombo.SelectedItem as Brand).BrandId).ToList();
            }
            if(CategoryCombo.SelectedIndex > 0)
            {
                Prod = Prod.Where(u=> u.Category == 
                (CategoryCombo.SelectedItem as ProductCategory)
                .CategoryId).ToList();
            }
            
            ProductsList.ItemsSource = Prod.ToList();
        }

        private void ProductItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var q = (sender as ListViewItem)?.DataContext as Product;
            if (UserType > 1) Navigation.F2.Navigate(new ProductDetailChangePage(q));
            else Navigation.F2.Navigate(new ProductDetailPage(q,UserID));
        }

        private void BrandCombo_SelectionChanged(object sender, SelectionChangedEventArgs e) => UpdateData();

        private void Page_Loaded(object sender, RoutedEventArgs e) => UpdateData();

        private void ProductAddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UserType > 1) Navigation.F2.Navigate(new ProductDetailChangePage(null));
        }
    }
}
