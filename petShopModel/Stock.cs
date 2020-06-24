using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace petShopModel
{

    class Stock
    {
        private readonly Queue<Purchase> Requests;
        public int RequestCounter { get; set; }

        public event Action<Purchase> RequestProcessed;
        public event Action<Purchase> RequestPostponed;
        public event Action SimulationFinished;

        public void Manage(int size, object context)
        {
            RequestCounter = 0;
            var syncContext = context as SynchronizationContext;
            var rand = new Random(83);
            while (RequestCounter < size)
            {
                CheckRequests(syncContext);
                Thread.Sleep(rand.Next(1000, 2000));
            }
            syncContext?.Send(obj => SimulationFinished?.Invoke(), null);
        }

        private void CheckRequests(SynchronizationContext context)
        {
            lock (Requests)
            {
                //ну и тут что-нибудь происходит
            }
        }
    }
}
