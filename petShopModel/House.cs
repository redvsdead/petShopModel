using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace petShopModel
{
    public abstract class House : Product
    {
        public House(int _amount) : base(_amount)
        {
            Amount = _amount;
        }
        public abstract void SetMax();
    }

    class Aquarium : House
    {
        public const int max = 20;
        public Aquarium(int _amount) : base(_amount)
        {
            Amount = _amount;
        }
        public override void SetMax()
        {
            Amount = max;
        }
        public override string ToString()
        {
            return $"Аквариум {Amount} шт.";
        }
    }

    class BirdCage : House
    {
        public const int max = 40;
        public BirdCage(int _amount) : base(_amount)
        {
            Amount = _amount;
        }
        public override void SetMax()
        {
            Amount = max;
        }
        public override string ToString()
        {
            return $"Птичья клетка {Amount} шт.";
        }
    }

    class Cage : House
    {
        public const int max = 40;
        public Cage(int _amount) : base(_amount)
        {
            Amount = _amount;
        }
        public override void SetMax()
        {
            Amount = max;
        }
        public override string ToString()
        {
            return $"Клетка для грызунов {Amount} шт.";
        }
    }

}
