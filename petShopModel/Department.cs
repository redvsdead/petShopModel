using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace petShopModel
{
    //без понятия, как прикрутить несколько животин без фиксированного количества, пусть будет одна пока
    abstract class Department
    {
        protected Animal animal;
        protected House house;
        private Thread thread;
        public bool IsBusy => thread != null && thread.IsAlive;   //занят ли сейчас отдел рассмотрением заказа

        public event Action<Purchase, Department> PurchaseMade;
        public virtual bool Process(Purchase purchase, SynchronizationContext context)
        {
            bool canProcess = !IsBusy;
            if (canProcess)
                lock (purchase)
                {
                    thread = new Thread(() =>
                    {
                        Thread.Sleep(8000);
                        purchase.IsMade = true;
                        animal.Amount -= purchase.animalAmount;
                        house.Amount -= purchase.animalAmount;
                        //если к-во чего-то стало <=0, то надо будет отсылать рекевст на склад, как -- нипанятна
                        context.Send(obj => PurchaseMade?.Invoke(obj as Purchase, this), purchase);
                    });
                    thread.Start();
                }

            return canProcess;
        }
    }

    //ну и тут наследники-отделы
}