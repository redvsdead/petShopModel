using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace petShopModel
{
    class House
    {
        private int amount;

        public House(int _amount)
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

    class Aquarium : House
    {
        public Aquarium(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"{Amount} aquarium";
        }
    }

    class Terrarium : House
    {
        public Terrarium(int _amount) : base(_amount)
        {
            Amount = _amount;
        }

        public override string ToString()
        {
            return $"{Amount} terrarium";
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
            return $"{Amount} bird cage";
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
            return $"{Amount} cage";
        }
    }

}
