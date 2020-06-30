using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

//поставщик товара в определенный отдел 

namespace petShopModel
{
    public class Contractor
    {
        //поток доставки товара на склад 
        private Thread contractingThread;
        //выполнение поставки 
        public event Action<Purchase, Contractor> ContractionCompleted;
        //процесс поставки товара на склад, возвращает true, если поставку можно выполнить (т е поставщик не занят) 
        public virtual bool Contract(Purchase purchaseRequest, SynchronizationContext context)
        {
            bool ContractionAvialable = contractingThread == null || !contractingThread.IsAlive;
            //смотрим, доступен ли сейчас поставщик 
            if (ContractionAvialable)
                lock (purchaseRequest)
                {
                    contractingThread = new Thread(() =>
                    {
                        Thread.Sleep(7000);
                        context.Send(obj => ContractionCompleted?.Invoke(obj as Purchase, this), purchaseRequest);
                    });
                    contractingThread.Start();
                }

            return ContractionAvialable;
        }
    }

    class RodentDepContractor : Contractor
    {
        public override string ToString()
        {
            return "Поставщик отдела грызунов";
        }
    }

    class BirdDepContractor : Contractor
    {
        public override string ToString()
        {
            return "Поставщик птичьего отдела";
        }
    }

    class FishDepContractor : Contractor
    {
        public override string ToString()
        {
            return "Поставщик отдела рыбок";
        }
    }
}