using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {
        public CategoryPage()
        {
            InitializeComponent();
            UpdateData();
        }

        private void UpdateData()
        {
            using CosmeticshopContext dc = new();
            CategoryGrid.ItemsSource = dc.ProductCategories.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            CategoriesC.Category = null;
            CategoriesC.Visibility = Visibility.Visible;
        }

        private void ChangeItem_Click(object sender, RoutedEventArgs e)
        {
            CategoriesC.Category = (ProductCategory)CategoryGrid.SelectedItem;
            CategoriesC.Visibility = Visibility.Visible;
        }

        private void CategoryGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CategoriesC.Category = (ProductCategory)CategoryGrid.SelectedItem;
            CategoriesC.Visibility = Visibility.Visible;
        }

        private void CategoriesC_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (CategoriesC.Visibility == Visibility.Collapsed) { UpdateData(); }
        }
    }
}
