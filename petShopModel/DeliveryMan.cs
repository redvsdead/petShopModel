using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace petShopModel
{
    class DeliveryMan
    {
        //поток доставки
        private Thread DeliveringThread;
        //выполнение доставки товара
        public event Action<Purchase, DeliveryMan> Delivered;
        //процесс доставки заказчику
        public virtual bool Deliver(Purchase request, SynchronizationContext context)
        {
            bool CanDeliver = DeliveringThread == null || !DeliveringThread.IsAlive;
            //смотрим, может ли доставщик выполнить доставку 
            if (CanDeliver)
                lock (request)
                {
                    DeliveringThread = new Thread(() =>
                    {
                        Thread.Sleep(3000);
                        request.IsMade = true;
                        context.Send(obj => Delivered?.Invoke(obj as Purchase, this), request);
                    });
                    DeliveringThread.Start();
                }

            return CanDeliver;
        }
    }

    class RodentDepDelivery : DeliveryMan
    {
        public override string ToString()
        {
            return "Доставщик отдела грызунов";
        }
    }

    class BirdDepDelivery : DeliveryMan
    {
        public override string ToString()
        {
            return "Доставщик птичьего отдела";
        }
    }

    class FishDepDelivery : DeliveryMan
    {
        public override string ToString()
        {
            return "Доставщик отдела рыбок";
        }
    }
}