using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace petShopModel
{
    public interface IView
    {
        SynchronizationContext Context { get; set; }
        event Action<int> Start;
        void OnPurchaseAdded(Purchase purchase);
        void OnPurchaseProcessed(Purchase purchase);
        void OnPurchasePostponed(Purchase purchase);
        void OnContracted(Purchase purchase, Contractor contractor);
        void OnPurchaseDelivered(Purchase purchase, DeliveryMan deliverer);
        void OnSimulationFinished();
    }
}
