using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Product
    {
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            private set
            {
                _Name = value;
            }
        }

        private int _Price;
        public int Price
        {
            get
            {
                return _Price;
            }
            private set
            {
                _Price = value;
            }
        }

        private int _Stock;
        public int Stock
        {
            get
            {
                return _Stock;
            }
            set
            {
                _Stock = value;
            }
        }

        public Product(string product_name, int product_price, int product_stock)
        {
            this.Name = product_name;
            this.Price = product_price;
            this.Stock = product_stock;
        }
    }
}
