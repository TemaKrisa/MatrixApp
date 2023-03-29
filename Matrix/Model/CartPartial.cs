using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix.Model
{
    partial class Cart
    {
        public decimal TotalCosts
        {
            get 
            {
                return Product.TotalPrice * Count;
            }
        }
    }
}
