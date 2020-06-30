using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace petShopModel
{
    class House : Product
    {
        public House(int _amount) : base(_amount)
        {
            Amount = _amount;
        }
    }

    class Aquarium : House
    {
        public Aquarium(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"Аквариум {Amount} шт.";
        }
    }

    class BirdCage : House
    {
        public BirdCage(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"Птичья клетка {Amount} шт.";
        }
    }

    class Cage : House
    {
        public Cage(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"Клетка для грызунов {Amount} шт.";
        }
    }

}
