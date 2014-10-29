using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public enum StatusCode
    {
        available,
        drink_doesnt_exist,
        out_of_stock,
        coin_accepted,
        coin_not_accepted
    }

    public class VendingMachine
    {
        private List<Product> _Products;
        public List<Product> Products
        {
            get
            {
                //ensures products are only loaded once
                if (_Products == null)
                {
                    _Products = LoadProducts();
                }
                return _Products;
            }
        }

        private static  List<Coin> _Coins;
        public List<Coin> Coins
        {
            get
            {
                //ensures products are only loaded once
                if (_Coins == null)
                {
                    _Coins = LoadCoins();
                }
                return _Coins;
            }
            set
            {
                _Coins = value;
            }
        }
     
        private List<Product> LoadProducts()
        {
            //product info may come from file to retain data in event of system crash.
            //assumed starting stock to be 10 for brevity
            var prodList = new List<Product>();
            prodList.Add(new Product("Sprite",0,10));
            prodList.Add(new Product("Coke", 13, 10));
            prodList.Add(new Product("Fanta", 15, 10));
            prodList.Add(new Product("Juice", 24, 10));

            return prodList;
        }
        private List<Coin> LoadCoins()
        {
            //product info may come from file to retain ata in event of system crash.
            //assumed starting stock to be 100 for brevity
            var coinList = new List<Coin>();
            
            coinList.Add(new Coin(1,100));
            coinList.Add(new Coin(2, 100));
            coinList.Add(new Coin(5, 100));
            coinList.Add(new Coin(10, 100));

            return coinList;
        }

        public Product currentDrink;

        //method for ordering a single drink
        public StatusCode selectDrink(string drinkName)
        {
            Product orderedDrink = Products.FirstOrDefault(p => p.Name == drinkName);

            //does drink exist in system?
            if (orderedDrink == null)
            {
                return StatusCode.drink_doesnt_exist;
            }

            //is this product in stock
            if (orderedDrink.Stock == 0)
            {
                return StatusCode.out_of_stock;
            }

            currentDrink = orderedDrink;
            return StatusCode.available;
        }

        private StatusCode CheckCoin(int entered_Coin)
        {
            if (entered_Coin == 1 ||
                entered_Coin == 2 ||
                entered_Coin == 5 ||
                entered_Coin == 10)
            {
                return StatusCode.coin_accepted;
            }

            return StatusCode.coin_not_accepted;
        }

        private void GiveChange(int overPayment, int depth = 0)
        {
            Coin changeCoin = Coins.OrderByDescending(c => c.Value).ElementAt(depth);

            while ((overPayment / changeCoin.Value) != 0)
            {
                if (changeCoin.Stock > 0)
                {
                    Console.WriteLine("Dispensing {0}p", changeCoin.Value);
                    changeCoin.Stock--;
                    overPayment -= changeCoin.Value;
                }
            }

            if (overPayment != 0)
            {
                GiveChange(overPayment, depth + 1);
            }
            return;
        }

        static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine();
            while (1 < 2)
            {
                // Keep the console window open in debug mode.
                Console.WriteLine("Name the drink");
                var drinkName = Console.ReadLine();

                if (drinkName == "Show stock")
                {
                    Console.WriteLine("-----------------------------------------");
                    foreach (Product prod in vm.Products)
                    {
                        Console.WriteLine("{0} : {1}",prod.Name,prod.Stock);
                    }
                }
                else if (drinkName == "Show coins")
                {
                    Console.WriteLine("-----------------------------------------");
                    foreach (Coin coin in vm.Coins)
                    {
                        Console.WriteLine("{0}p : {1}",coin.Value,coin.Stock);
                    }
                }
                else if (vm.selectDrink(drinkName) == StatusCode.available)
                {

                    int payment = 0;
                    while (payment < vm.currentDrink.Price)
                    {
                        Console.WriteLine("Payment remaining: {0}p", vm.currentDrink.Price - payment);

                        int entered_Coin;
                        int.TryParse(Console.ReadLine(), out entered_Coin);

                        if (vm.CheckCoin(entered_Coin) == StatusCode.coin_accepted)
                        {
                            payment += entered_Coin;
                            vm.Coins.First(c => c.Value == entered_Coin).Stock++;
                        }
                        else
                        {
                            Console.WriteLine("Coin not Accepted");
                        }
                    }

                    vm.GiveChange(payment - vm.currentDrink.Price);

                    Console.WriteLine("Dispensing {0}", vm.currentDrink.Name);
                    vm.currentDrink.Stock--;
                }
                else
                {
                    Console.WriteLine("Invalid Drink name: {0}", drinkName);
                }
            }
        }
    }
}
