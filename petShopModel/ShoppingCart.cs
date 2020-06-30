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
        Department ShoppingDepartments;
        Queue<Purchase> Purchases;
        public event Action<Purchase> PurchaseInProcess;
        public event Action<Purchase> PostponePurchase;
        public event Action FinishWork;

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
                    var request = Purchases.Dequeue();
                    //дальше смотрим: если отдел не занят продажей, направляем туда, иначе откладываем
                    if (ShoppingDepartments.HandleRequest(request, context))
                    {
                        context.Send(obj => PurchaseInProcess?.Invoke(obj as Purchase), request);
                    }
                    else
                    {
                        context.Send(obj => PostponePurchase?.Invoke(obj as Purchase), request);
                        Purchases.Enqueue(request);
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
                Thread.Sleep(rand.Next(1000, 2000));
                ++Purchased;
            }
            syncContext?.Send(obj => FinishWork?.Invoke(), null);
        }
    }
}
