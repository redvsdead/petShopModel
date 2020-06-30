using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace petShopModel
{
    interface IView
    {
        SynchronizationContext Context { get; set; }

        event Action<int> Start;

        void OnSimulationFinished();

        void OnRequestAdded(Purchase request);

        void OnRequestProcessed(Purchase request);

        void OnRequestPostponed(Purchase request);

        void OnRequestFinished(Purchase request, DeliveryMan employee);
    }
}
