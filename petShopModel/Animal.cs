using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace petShopModel
{
    class Animal
    {
        private int amount;

        public Animal(int _amount)
        {
            Amount = _amount;
        }

        public int Amount
        {
            get => amount;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Amount can not be negative.");
                amount = value;
            }
        }
    }

    class Fish : Animal
    {
        public Fish(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"{Amount} fish";
        }
    }

    class Hamster : Animal
    {
        public Hamster(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"{Amount} hamster(s)";
        }
    }

    class Mouse : Animal
    {
        public Mouse(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"{Amount} mouse(ice)";
        }
    }

    class Parrot : Animal
    {
        public Parrot(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"{Amount} parrot(s)";
        }
    }

    class Turtle : Animal
    {
        public Turtle(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"{Amount} turtle(s)";
        }
    }
}
