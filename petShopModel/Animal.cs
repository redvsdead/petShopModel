using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace petShopModel
{
    class Animal : Product
    {
        public Animal(int _amount) : base(_amount)
        {
            Amount = _amount;
        }
    }

    class Rodent : Animal
    {
        const int Max = 20;
        public Rodent(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"Грызун {Amount} шт.";
        }
    }

    class Bird : Animal
    {
        const int Max = 40;
        public Bird(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"Птица {Amount} шт.";
        }
    }

    class Fish : Animal
    {
        const int Max = 100;
        public Fish(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"Рыбка {Amount} шт.";
        }
    }
}
