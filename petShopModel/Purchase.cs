using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace petShopModel
{
    public abstract class Purchase
    {
        private int purchaseID;
        public int animalAmount;
        public int houseAmount;

        public int PurchaseID
        {
            get => purchaseID;
            set
            {
                if (value < 1)
                    throw new ArgumentException("PurchaseID");
            }
        }
        public bool IsMade { get; set; }
        protected Purchase(int purchaseID, int animal, int house)
        {
            IsMade = false;
            PurchaseID = purchaseID;
            animalAmount = animal;
            houseAmount = house;
            //если покупают животное, добавляем сопутствующие товары
                if ((animalAmount > 0) && (houseAmount == 0))
                    houseAmount++;
           
        }
    }

    public class ParrotPurchase : Purchase
    {
        public ParrotPurchase(int purchaseID, int animal, int house) : base(purchaseID, animal, house)
        {
        }
        public override string ToString()
        {
            return $"Submitted purchase of {animalAmount} parrots and {houseAmount} cages";
        }
    }

    public class HamsterPurchase : Purchase
    {
        public HamsterPurchase(int purchaseID, int animal, int house) : base(purchaseID, animal, house)
        {
        }
        public override string ToString()
        {
            return $"Submitted purchase of {animalAmount} hamsters and {houseAmount} cages";
        }
    }

    public class MousePurchase : Purchase
    {
        public MousePurchase(int purchaseID, int animal, int house) : base(purchaseID, animal, house)
        {
        }
        public override string ToString()
        {
            return $"Submitted purchase of {animalAmount} mice and {houseAmount} cages";
        }
    }

    public class FishPurchase : Purchase
    {
        public FishPurchase(int purchaseID, int animal, int house) : base(purchaseID, animal, house)
        {
        }
        public override string ToString()
        {
            return $"Submitted purchase of {animalAmount} fish and {houseAmount} aquariums";
        }
    }

    public class TurtlePurchase : Purchase
    {
        public TurtlePurchase(int purchaseID, int animal, int house) : base(purchaseID, animal, house)
        {
        }
        public override string ToString()
        {
            return $"Submitted purchase of {animalAmount} turtles and {houseAmount} terrariums";
        }
    }

}
