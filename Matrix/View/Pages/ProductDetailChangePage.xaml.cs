using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Matrix.Model;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;

namespace Matrix.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductDetailChangePage.xaml
    /// </summary>
    public partial class ProductDetailChangePage : Page
    {
        Product product = new Product();
        public ProductDetailChangePage(Product pr1)
        {
            InitializeComponent();
            product = pr1;
            LoadData();
        }

        private void LoadData()
        {
            using CosmeticshopContext dc = new();
            var Brand = dc.Brands.ToList();
            var Manufacturer = dc.Manufacturers.ToList();
            var Category = dc.ProductCategories.ToList();
            txtBrand.ItemsSource = Brand;
            txtManufacturer.ItemsSource = Manufacturer;
            txtCategory.ItemsSource = Category;
            txtManufacturer.SelectedIndex = (product.Manufacturer - 1);
            txtCategory.SelectedIndex = (product.Category - 1);
            txtBrand.SelectedIndex = (product.Brand - 1);
            txtCount.Text = product.Count.ToString();
            txtName.Text = product.ProductName;
            txtPrice.Text = $"{product.Price:F2}";
            txtDiscount.Text = product.Discount.ToString();
            TxtDescription.Text = product.Description;
            ProductTitle.Text = product.ProductName;
            using (MemoryStream ms = new MemoryStream(product.Image))
            {
                // Создаем BitmapImage из MemoryStream
                BitmapImage bmpImage = new BitmapImage();
                bmpImage.BeginInit();
                bmpImage.StreamSource = ms;
                bmpImage.CacheOption = BitmapCacheOption.OnLoad;
                bmpImage.EndInit();

                // Устанавливаем свойство Source элемента Image в созданный BitmapImage
                ProductImg.Source = bmpImage;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            byte[] imageData;

            var m = (Manufacturer)txtManufacturer.SelectedItem;
            var b = (Brand)txtBrand.SelectedItem;
            var c = (ProductCategory)txtCategory.SelectedItem;
                BitmapImage bmpImage = (BitmapImage)ProductImg.Source;

                using (MemoryStream ms = new MemoryStream())
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bmpImage));
                    encoder.Save(ms);

                    // Получаем массив байтов из MemoryStream
                    imageData = ms.ToArray();
                }
            using CosmeticshopContext dc = new();
            if (product == null)
            {
                Product pr2 = new Product()
                {
                    Discount = Convert.ToInt32(txtDiscount.Text),
                    Manufacturer = m.ManufacturerId,
                    Brand = b.BrandId,
                    Category = c.CategoryId,
                    Price = Convert.ToDecimal(txtPrice.Text),
                    TotalPrice = Convert.ToDecimal(txtPrice.Text) - (Convert.ToDecimal(txtPrice.Text) * (Convert.ToDecimal(txtDiscount.Text) / 100)),
                    Image = imageData,
                    Description = TxtDescription.Text,
                    Count = Convert.ToInt32(txtDiscount.Text),
                    ProductName = txtName.Text
                };
                dc.Products.Add(pr2);
            }
            else
            {
                product.Discount = Convert.ToInt32(txtDiscount.Text);
                product.Manufacturer = m.ManufacturerId;
                product.Brand = b.BrandId;
                product.Category = c.CategoryId;
                product.Price = Convert.ToDecimal(txtPrice.Text);
                product.TotalPrice = Convert.ToDecimal(txtPrice.Text) - (Convert.ToDecimal(txtPrice.Text) * (Convert.ToDecimal(txtDiscount.Text) / 100));
                product.Image = imageData;
                product.Description = TxtDescription.Text;
                product.Count = Convert.ToInt32(txtDiscount.Text);
                product.ProductName = txtName.Text;
                dc.Products.Update(product);
            }
                dc.SaveChanges();
            Navigation.F2.GoBack();
            MessageBoxs.ShowDialog("Изменения прошли успешно", "Изменение каталога продуктов");
        }

        private void ChangeImgBtn_Click(object sender, RoutedEventArgs e)
        {
            // Открываем диалог выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // Создаем BitmapImage из выбранного файла
                BitmapImage bmpImage = new BitmapImage();
                bmpImage.BeginInit();
                bmpImage.UriSource = new Uri(openFileDialog.FileName);
                bmpImage.EndInit();

                // Устанавливаем свойство Source элемента Image в созданный BitmapImage
                ProductImg.Source = bmpImage;
            }
        }
    }
}
