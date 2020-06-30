using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

//абстрактный товар зоомагазина, от которого наследуются все конкретные товары

namespace petShopModel
{
    public abstract class Product
    {
        private int amount;

        public Product(int _amount)
        {
            Amount = _amount;
        }

        public int Amount
        {
            get => amount;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество товара не может быть отрицательным.");
                amount = value;
            }
        }
    }
}