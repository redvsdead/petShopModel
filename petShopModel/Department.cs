using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

//отделы зоомагазина (связанные между собой цепочно по паттерну chain of command)
//отдел содержит поставщика, который привозит на склад отдела соответствубщие товары, и доставщика

namespace petShopModel
{
    abstract class Department
    {
        protected Department Next;          //паттерн chain of command
        protected DeliveryMan deliverer;    //доставщик отдела
        protected Contractor contractor;
        protected Animal Animals;
        protected House Houses;
        public event Action<Purchase, Contractor> ContractionFinished;
        public event Action<Purchase, DeliveryMan> PurchaseDelivered;

        protected Department()
        {
            contractor = CreateContractor();
            deliverer = CreateDeliverer();
            Animals = CreateAnimals();
            Houses = CreateHouses();
            //изначально в отделе максимальное к-во товаров
            Animals.SetMax();
            Houses.SetMax();
            contractor.ContractionCompleted += (request, contractor) => ContractionFinished?.Invoke(request, contractor);
            deliverer.Delivered += (request, deliverer) => PurchaseDelivered?.Invoke(request, deliverer);
        }

        public Department SetNext(Department department)
        {
            Next = department;
            return department;
        }

        //добавляет действие на событие цепочке отделов
        public void Subscribe(Action<Purchase, DeliveryMan> action)
        {
            PurchaseDelivered += action;  //заказ выполнен и доставлен
            Next?.Subscribe(action);   
        }

        //может ли отдел принять покупку
        protected abstract bool CanHandle(Purchase purchase);

        //создание сотрудников отдела и товаров (фабричные методы)
        protected abstract DeliveryMan CreateDeliverer();
        protected abstract Contractor CreateContractor();
        protected abstract Animal CreateAnimals();
        protected abstract House CreateHouses();

        //обработка заявки на покупку
        public bool HandleRequest(Purchase purchase, SynchronizationContext context)
        {
            //если в этом отделе ее обработать нельзя, она идет в следующий
            if (!CanHandle(purchase))
                return Next != null && Next.HandleRequest(purchase, context);
            //иначе пытаемся отправить ее на обработку
            if (Animals.Amount <= purchase.animalAmount || Houses.Amount <= purchase.houseAmount) {
                if (!contractor.Contract(purchase, context))
                {
                    return false;
                }
                if (Animals.Amount <= purchase.animalAmount)
                {
                    Thread.Sleep(100);  //в это время как бы происходит поставка животных
                    Animals.SetMax();
                }
                if (Houses.Amount <= purchase.houseAmount)
                {
                    Thread.Sleep(100);  //в это время как бы происходит поставка жилищ
                    Houses.SetMax();
                }
            }
            //возвращаем результат - была ли доставлена покупка, если нет, заказ вернется обратно в очередь
            if (deliverer.Deliver(purchase, context))
            {
                //если доставлена, то убираем проданный товар
                Animals.Amount -= purchase.animalAmount;
                Houses.Amount -= purchase.houseAmount;
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
 
