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

namespace Matrix.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для StarRatingControl.xaml
    /// </summary>
    public partial class StarRatingControl : UserControl
    {
        public int Rating { get; set; }

        public StarRatingControl()
        {
            InitializeComponent();
        }

        private void Star_Click(object sender, RoutedEventArgs e)
        {
            Button clickedStar = (Button)sender;
            int starNumber = int.Parse(clickedStar.Name.Substring(4));
            Rating = starNumber;
            UpdateStars(starNumber);
        }

        private void UpdateStars(int selectedStar)
        {
            star1.Content = selectedStar >= 1 ? "★" : "☆";
            star2.Content = selectedStar >= 2 ? "★" : "☆";
            star3.Content = selectedStar >= 3 ? "★" : "☆";
            star4.Content = selectedStar >= 4 ? "★" : "☆";
            star5.Content = selectedStar >= 5 ? "★" : "☆";
        }
    }
        
}
