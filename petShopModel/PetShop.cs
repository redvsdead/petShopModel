using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace petShopModel
{
    public class PetShop
    {
        private static readonly Stock stock;
        private readonly rodentDepartment department;
        private readonly Queue<Purchase> rodentPurchase;
        private readonly Queue<Purchase> birdPurchase;
        private readonly Queue<Purchase> fishPurchase;
    }
}
