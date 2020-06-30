using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace petShopModel
{
    abstract class Department
    {
        protected Department Next;          //паттерн chain of command
        protected Contractor contractor;    //поставщик отдела
        protected DeliveryMan deliverer;    //доставщик отдела
        protected Animal Animals;
        protected House Houses;

        public event Action<Purchase, Contractor> RequestFinished;

        protected Department()
        {
            contractor = CreateContractor();
            deliverer = CreateDeliverer();
            contractor.ContractionCompleted += (request, employee) => RequestFinished?.Invoke(request, employee);
        }

        public Department SetNext(Department department)
        {
            Next = department;
            return department;
        }

        //добавляет действие на событие цепочке отделов
        public void Subscribe(Action<Purchase, Contractor> action)
        {
            RequestFinished += action;
            Next?.Subscribe(action);
        }

        //может ли отдел принять покупку
        protected abstract bool CanHandle(Purchase purchase);

        //создание сотрудников отдела (фабрика)
        protected abstract Contractor CreateContractor();
        protected abstract DeliveryMan CreateDeliverer();

        //обработка заявки на покупку
        public bool HandleRequest(Purchase purchase, SynchronizationContext context)
        {
            //если в этом отделе ее обработать нельзя, она идет в следующий
            if (!CanHandle(purchase))
                return Next != null && Next.HandleRequest(purchase, context);
            if (Animals.Amount <= purchase.animalAmount || Houses.Amount <= purchase.houseAmount) {
                //иначе пытаемся отправить ее на обработку (вернет false, если поставщик занят)
                if (!contractor.Process(purchase, context))
                    return false;   //в этом случае откладываем заказ
                if (Animals.Amount <= purchase.animalAmount)
                {
                    Animals.SetMax();
                }
                if (Houses.Amount <= purchase.houseAmount)
                {
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
        protected sealed override Contractor CreateContractor()
        {
            return new RodentDepContractor();
        }
        protected sealed override DeliveryMan CreateDeliverer()
        {
            return new RodentDepDelivery();
        }
        protected override bool CanHandle(Purchase purchase)
        {
            return purchase is RodentPurchase;
        }
    }

    class BirdDepartment : Department
    {
        protected sealed override Contractor CreateContractor()
        {
            return new BirdDepContractor();
        }
        protected sealed override DeliveryMan CreateDeliverer()
        {
            return new BirdDepDelivery();
        }
        protected override bool CanHandle(Purchase purchase)
        {
            return purchase is BirdPurchase;
        }
    }

    class FishDepartment : Department
    {
        protected sealed override Contractor CreateContractor()
        {
            return new FishDepContractor();
        }
        protected sealed override DeliveryMan CreateDeliverer()
        {
            return new FishDepDelivery();
        }
        protected override bool CanHandle(Purchase purchase)
        {
            return purchase is FishPurchase;
        }
    }
}
 
