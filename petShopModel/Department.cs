using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

//отделы зоомагазина (связанные между собой цепочно по паттерну chain of command)
//отдел содержит поставщика, который привозит на склад отдела соответствубщие товары, и доставщика

namespace petShopModel
{
    public abstract class Department
    {
        protected Department Next;          //паттерн chain of command для удобства подключения обработчиков одним методом, 
                                            //а не для каждого отдела по очереди
        protected DeliveryMan deliverer;    //доставщик отдела
        protected Contractor contractor;
        public Animal Animals;
        public House Houses;
        public event Action<Purchase, Contractor> ContractionFinished;
        public event Action<Purchase, DeliveryMan> PurchaseDelivered;
        public event Action<Department> StockChanges;    //вот этот ивент

        //"подключение" всех отделов магазина к событию поставки (тк отделы связаны по цепочке, это можно сделать одним методом)
        public void AddContractionAction(Action<Purchase, Contractor> action)
        {
            ContractionFinished += action;
            Next?.AddContractionAction(action);
        }
        //"подключеие" всех отделов магазина к событию доставки
        public void AddDeliveryAtion(Action<Purchase, DeliveryMan> action)
        {
            PurchaseDelivered += action;
            Next?.AddDeliveryAtion(action);
        }

        public void AddStockChanges(Action<Department> action)
        {
            StockChanges += action;
            Next?.AddStockChanges(action);
        }

        protected Department()
        {
            contractor = CreateContractor();
            deliverer = CreateDeliverer();
            Animals = CreateAnimals();
            Houses = CreateHouses();
            //изначально в отделе максимальное к-во товаров
            Animals.SetMax();
            Houses.SetMax();
            contractor.ContractionCompleted += (purchase, contractor) => ContractionFinished?.Invoke(purchase, contractor);
            deliverer.Delivered += (purchase, deliverer) => PurchaseDelivered?.Invoke(purchase, deliverer);
        }
        public Department SetNext(Department department)
        {
            Next = department;
            return department;
        }
        //может ли отдел принять покупку
        protected abstract bool CanHandle(Purchase purchase);
        //создание сотрудников отдела и товаров (тк нужно использовать паттерн фабрика)
        protected abstract DeliveryMan CreateDeliverer();
        protected abstract Contractor CreateContractor();
        protected abstract Animal CreateAnimals();
        protected abstract House CreateHouses();

        //обработка заявки на покупку
        public bool HandlePurchase(Purchase purchase, SynchronizationContext context)
        {
            //если в этом отделе ее обработать нельзя, она идет в следующий
            if (!CanHandle(purchase))
                return Next != null && Next.HandlePurchase(purchase, context);
            //иначе пытаемся отправить ее на обработку
            if (Animals.Amount <= purchase.animalAmount || Houses.Amount <= purchase.houseAmount) {
                if (!contractor.Contract(purchase, context))
                {
                    return false;
                }
                else
                {
                    if (Animals.Amount <= purchase.animalAmount)
                    {
                        Animals.SetMax();
                    }
                    if (Houses.Amount <= purchase.houseAmount)
                    {
                        Houses.SetMax();
                    }
                    StockChanges?.Invoke(this);
                }
            }
            //возвращаем результат - была ли доставлена покупка, если нет, заказ вернется обратно в очередь
            if (deliverer.Deliver(purchase, context))
            {
                //если доставлена, то убираем проданный товар
                Animals.Amount -= purchase.animalAmount;
                Houses.Amount -= purchase.houseAmount;
                StockChanges?.Invoke(this);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class RodentDepartment : Department
    {
        public override string ToString()
        {
            return "отдела грызунов";
        }
        protected sealed override DeliveryMan CreateDeliverer()
        {
            return new RodentDepDelivery();
        }
        protected sealed override Contractor CreateContractor()
        {
            return new RodentDepContractor();
        }
        protected sealed override Animal CreateAnimals()
        {
            return new Rodent(0);
        }
        protected sealed override House CreateHouses()
        {
            return new Cage(0);
        }
        protected override bool CanHandle(Purchase purchase)
        {
            return purchase is RodentPurchase;
        }
    }

    class BirdDepartment : Department
    {
        public override string ToString()
        {
            return "отдела птиц";
        }
        protected sealed override DeliveryMan CreateDeliverer()
        {
            return new BirdDepDelivery();
        }
        protected sealed override Contractor CreateContractor()
        {
            return new BirdDepContractor();
        }
        protected sealed override Animal CreateAnimals()
        {
            return new Bird(0);
        }
        protected sealed override House CreateHouses()
        {
            return new BirdCage(0);
        }
        protected override bool CanHandle(Purchase purchase)
        {
            return purchase is BirdPurchase;
        }
    }

    class FishDepartment : Department
    {
        public override string ToString()
        {
            return "отдела рыбок";
        }
        protected sealed override DeliveryMan CreateDeliverer()
        {
            return new FishDepDelivery();
        }
        protected sealed override Contractor CreateContractor()
        {
            return new FishDepContractor();
        }
        protected sealed override Animal CreateAnimals()
        {
            return new Fish(0);
        }
        protected sealed override House CreateHouses()
        {
            return new Aquarium(0);
        }
        protected override bool CanHandle(Purchase purchase)
        {
            return purchase is FishPurchase;
        }
    }
}
 
