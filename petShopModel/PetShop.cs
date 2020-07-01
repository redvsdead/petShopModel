using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

//модель зоомагазина

namespace petShopModel
{
    public class PetShop
    {
        private PurchaseQueueCreation PurchaseCreation; //для создания запросов на покупку
        private ShoppingCart Cart;                      //корзина покупок
        private readonly Department DepartmentChain;    //цепочка отделов
        private Queue<Purchase> PurchaseQueue;          //очередь покупок
        public event Action<Purchase> NewPurchase;      //поступление нового заказа в отдел
        public event Action<Purchase> PurchaseToDep;    //направить заказ в отдел
        public event Action<Purchase> PostponePurchase; //отложить заказ
        public event Action<Purchase, DeliveryMan> DeliveryFinished;
        public event Action<Purchase, Contractor> ContractionFinished;
        //работа окончена, завершение симуляции
        public event Action FinishWork;

        public PetShop()
        {
            PurchaseQueue = new Queue<Purchase>();
            PurchaseCreation = new PurchaseQueueCreation(PurchaseQueue);
            PurchaseCreation.AddPurchase += purchase => NewPurchase?.Invoke(purchase);

            DepartmentChain = CreateDepartments();
            DepartmentChain.SubscribeContraction((purchase, contractor) => ContractionFinished?.Invoke(purchase, contractor));
            DepartmentChain.SubscribeDelivery((purchase, deliverer) => DeliveryFinished?.Invoke(purchase, deliverer));

            Cart = new ShoppingCart(DepartmentChain, PurchaseQueue);
            Cart.PurchaseInProcess += purchase => PurchaseToDep?.Invoke(purchase);
            Cart.PostponePurchase += purchase => PostponePurchase?.Invoke(purchase);
            Cart.FinishWork += () => FinishWork?.Invoke();
        }

        //оформление и обработка покупок
        public void Manage(int maxPurchase, SynchronizationContext context)
        {
            var PurchaseThread = new Thread(syncContext => PurchaseCreation.Generate(maxPurchase, syncContext));
            var CartThread = new Thread(syncContext => Cart.DistributeToDeps(maxPurchase, syncContext));
            PurchaseThread.Start(context);
            CartThread.Start(context);
        }

        //создание отделов магазина по цепочке
        private static Department CreateDepartments()
        {
            var RodentDep = new RodentDepartment();
            RodentDep.SetNext(new BirdDepartment()).SetNext(new FishDepartment());
            return RodentDep;
        }
    }
}
