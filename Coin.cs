using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Coin
    {
        private int _Value;
        public int Value
        {
            get
            {
                return _Value;
            }
            private set
            {
                _Value = value;
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

        public Coin(int coin_Value, int coin_Stock)
        {
            Value = coin_Value;
            Stock = coin_Stock;
        }
    }
}
