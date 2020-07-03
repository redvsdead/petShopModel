using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

//здесь в кач-ве обработчиков событий petshop подключаются методы формы (которые выводят логи и рисуют анимацию),
//далее по цепочки эти события становятся обработчиками событий экземпляров своих полей, пока в конечном
//классе они не будут вызваны; таким образом событие доводится до формы
//например, рисование и вывод логов при доставке -- обработчик события доставки в petshop, который является обработчиком
//события доставки в department, который, в свою очередь, тоже является обработчиком события доставки в deliveryman,
//вызываемого в методе доставки

namespace petShopModel
{
    public class Presenter
    {
        private readonly IView _view;
        private readonly PetShop petshop;
        public Presenter(IView view)
        {
            _view = view;
            //подключаем обработчик события старт в view
            _view.Start += OnStart;
            petshop = new PetShop();
            //подключаем в кач-ве обработчиков методы view
            petshop.StockChanges += _view.OnStockChanges;
            petshop.NewPurchase += _view.OnPurchaseAdded;
            petshop.PurchaseToDep += _view.OnPurchaseProcessed;
            petshop.PostponePurchase += _view.OnPurchasePostponed;
            petshop.DeliveryFinished += _view.OnPurchaseDelivered;
            petshop.ContractionFinished += view.OnContracted;
            petshop.FinishWork += _view.OnSimulationFinished;
        }
        //запуск симуляции зоомагазина с заданным количеством покупок
        private void OnStart(int size)
        {
            var context = _view.Context;
            petshop.AcceptPurchase(size, context);
        }
    }
}