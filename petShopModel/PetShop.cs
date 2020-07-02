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
        public event Action<Purchase, DeliveryMan> DeliveryFinished;    //покупка доставлена
        public event Action<Purchase, Contractor> ContractionFinished;  //товар доставлен на склад
        public event Action<Department> StockChanges;  //изменения на складе
        public event Action FinishWork;         //работа магазина окончена

        public PetShop()
        {
            PurchaseQueue = new Queue<Purchase>();
            PurchaseCreation = new PurchaseQueueCreation(PurchaseQueue);
            DepartmentChain = CreateDepartments();
            Cart = new ShoppingCart(DepartmentChain, PurchaseQueue);
            //подключаем обработчики событий добавления покупки в корзину (откуда она пойдет в отделы),
            //откладывания покупки (если отдел занят) и окончания работы магазина
            Cart.PurchaseInProcess += purchase => PurchaseToDep?.Invoke(purchase);
            Cart.PostponePurchase += purchase => PostponePurchase?.Invoke(purchase);
            Cart.FinishWork += () => FinishWork?.Invoke();
            //подключаем обработчики событий добавления/откладывания (если доставщик занят)/изменений на складе/доставки покупки для отделов
            PurchaseCreation.AddPurchase += purchase => NewPurchase?.Invoke(purchase);
            DepartmentChain.MerchChange += department => StockChanges?.Invoke(department);
            DepartmentChain.AddContractionAction((purchase, contractor) => ContractionFinished?.Invoke(purchase, contractor));
            DepartmentChain.AddDeliveryAtion((purchase, deliverer) => DeliveryFinished?.Invoke(purchase, deliverer)); 
        }

        //оформление и обработка покупок
        public void AcceptPurchase(int maxPurchase, SynchronizationContext context)
        {
            var PurchaseThread = new Thread(syncContext => PurchaseCreation.Generate(maxPurchase, syncContext));
            var CartThread = new Thread(syncContext => Cart.DistributeToDeps(maxPurchase, syncContext));
            PurchaseThread.Start(context);
            CartThread.Start(context);
        }

        //создание отделов магазина по цепочке: грызуны, птицы, рыбы
        private static Department CreateDepartments()
        {
            var RodentDep = new RodentDepartment();
            RodentDep.SetNext(new BirdDepartment()).SetNext(new FishDepartment());
            return RodentDep;
        }
    }
}
