using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

//заявка на покупку, включающая вид животного и соответствующее жилище
//заявки группируются по отделам
//я могу добавить корм/игрушки, но у меня начинает зависать компьютер от количества сущностей

namespace petShopModel
{
    public abstract class Purchase
    {
        public string PurchaseAddress;    //адрес доставки
        public int animalAmount;
        public int houseAmount;

        public string PurchaseID
        {
            get => PurchaseAddress;
            set
            {
                if (value == "")
                    throw new ArgumentException("Empty PurchaseAddress");
            }
        }
        public bool IsMade { get; set; }
        protected Purchase(string address, int animal, int house)
        {
            IsMade = false;
            PurchaseAddress = address;
            animalAmount = animal;
            houseAmount = house;
            //если покупают животное, добавляем сопутствующие товары
            if ((animalAmount > 0) && (houseAmount == 0))
                houseAmount++;
        }
    }

    public class BirdPurchase : Purchase
    {
        public BirdPurchase(string address, int animal, int house) : base(address, animal, house)
        {
        }
        public override string ToString()
        {
            return $"птица {animalAmount} шт. и птичья клетка {houseAmount} шт.";
        }
    }

    public class RodentPurchase : Purchase
    {
        public RodentPurchase(string address, int animal, int house) : base(address, animal, house)
        {
        }
        public override string ToString()
        {
            return $"грызун {animalAmount} шт. и клетка {houseAmount} шт.";
        }
    }

    public class FishPurchase : Purchase
    {
        public FishPurchase(string address, int animal, int house) : base(address, animal, house)
        {
        }
        public override string ToString()
        {
            return $"рыбки {animalAmount} шт. и аквариум {houseAmount} шт.";
        }
    }

    /////////////////////////////////////////////////////////////////////////

    public class PurchaseQueueCreation    //создание запросов на покупку
    {
        public event Action<Purchase> AddPurchase;
        private readonly Queue<Purchase> PurchaseLine;
        public PurchaseQueueCreation(Queue<Purchase> purchases)
        {
            PurchaseLine = purchases;
        }
        //генерация одного заказа в отделе
        private void GenerateOne(SynchronizationContext context, string address = "---", int animals = 0, int houses = 0)
        {
            lock (PurchaseLine)
            {
                var purchase = GenerateType(address, animals, houses);
                PurchaseLine.Enqueue(purchase);
                context.Send(obj => AddPurchase?.Invoke(obj as Purchase), purchase);
            }
        }
        readonly string[] Address = { "Ул. Моисеева, д. 4", "Ул. Московский пр-т, д. 9А", "Ул. 9-е января, д. 14", "Ул. Плехановская, д. 12", "Университетская п-дь, д. 4", "Ул. Лизюкова, д. 45", "Ул. Моисеева, д. 4", "Ул. Московский пр-т, д. 9А", "Ул. 9-е января, д. 14", "Ул. Плехановская, д. 12", "Университетская п-дь, д. 4", "Ул. Лизюкова, д. 45", "Ул. Моисеева, д. 4", "Ул. Московский пр-т, д. 9А", "Ул. 9-е января, д. 14", "Ул. Плехановская, д. 12", "Университетская п-дь, д. 4", "Ул. Лизюкова, д. 45" };
        //генерация покупок
        public void Generate(int size, object context)
        {
            Random rand = new Random();
            for (int i = 0; (i < Address.Length) && (i < size); ++i)
            {
                int animals = rand.Next(0, 41);
                int houses = rand.Next(0, 10);
                GenerateOne(context as SynchronizationContext, Address[i], animals, houses);
                Thread.Sleep(4000);
            }
        }
        //генерация покупки в случайном отделе
        private Purchase GenerateType(string address, int animals, int houses)
        {
            Random rand = new Random();
            int number = rand.Next(0, 3);
            switch (number)
            {
                case 0: return new FishPurchase(address, animals, houses);
                case 1: return new RodentPurchase(address, animals, houses);
                case 2: return new BirdPurchase(address, animals, houses);
                default: return null;
            }
        }
    }
}
