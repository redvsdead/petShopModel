using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

//класс корзины покупок, в которую поступают все оформленные заказы и из которой они отправляются по отделам

namespace petShopModel
{
    class ShoppingCart
    {
        Department ShoppingDepartments; //цепочка отделов зоомагазина
        Queue<Purchase> Purchases;  
        public event Action<Purchase> PurchaseInProcess;    //отправить покупку в отдел
        public event Action<Purchase> PostponePurchase;     //отложить покупку
        public event Action FinishWork; //окончание работы магазина
        public ShoppingCart(Department _DepartmentList, Queue<Purchase> _Purchases)
        {
            ShoppingDepartments = _DepartmentList;
            Purchases = _Purchases;
        }
        //процесс рассмотрения покупки и отправки ее в соответствующий отдел (если это возможно)
        private void TrySendPurchase(SynchronizationContext context)
        {
            lock (Purchases)
            {
                if (Purchases.Count != 0)   //если заявки есть, то первую в очереди пытаемся отправить в отдел
                { 
                    var purchase = Purchases.Dequeue();
                    //дальше смотрим: если отдел не занят продажей, направляем туда, иначе откладываем
                    if (ShoppingDepartments.HandlePurchase(purchase, context))
                    {
                        context.Send(obj => PurchaseInProcess?.Invoke(obj as Purchase), purchase);
                    }
                    else
                    {
                        context.Send(obj => PostponePurchase?.Invoke(obj as Purchase), purchase);
                        Purchases.Enqueue(purchase);
                    }
                }
                return;
            }
        }

        int Purchased = 0;
        //обрабатываем данное количество заявок, после чего обработка покупок прекращается
        public void DistributeToDeps(int PurchaseAmount, object context)
        {
            var syncContext = context as SynchronizationContext;
            var rand = new Random();
            while (Purchased < PurchaseAmount)
            {
                TrySendPurchase(syncContext);
                Thread.Sleep(rand.Next(5000, 7000));
                ++Purchased;
            }
            syncContext?.Send(obj => FinishWork?.Invoke(), null);
        }
    }
}
