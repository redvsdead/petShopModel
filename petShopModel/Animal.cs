using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace petShopModel
{
    public abstract class Animal : Product
    {
        public Animal(int _amount) : base(_amount)
        {
            Amount = _amount;
        }
        public abstract void SetMax();
    }

    public class Rodent : Animal
    {
        public const int max = 20;
        public Rodent(int _amount) : base(_amount)
        {
            Amount = _amount;
        }
        public override void SetMax()
        {
            Amount = max;
        }
        public override string ToString()
        {
            return $"Грызун {Amount} шт.";
        }
    }

    public class Bird : Animal
    {
        public const int max = 40;
        public Bird(int _amount) : base(_amount)
        {
            Amount = _amount;
        }
        public override void SetMax()
        {
            Amount = max;
        }
        public override string ToString()
        {
            return $"Птица {Amount} шт.";
        }
    }

    public class Fish : Animal
    {
        public const int max = 100;
        public Fish(int _amount) : base(_amount)
        {
            Amount = _amount;
        }
        public override void SetMax()
        {
            Amount = max;
        }
        public override string ToString()
        {
            return $"Рыбка {Amount} шт.";
        }
    }
}
