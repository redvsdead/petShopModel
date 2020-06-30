﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace petShopModel
{
    public class Presenter
    {
        private readonly IView _view;

        private readonly PetShop petshop;

        public Presenter(IView view)
        {
            _view = view;
            _view.Start += OnStart;
            petshop = new PetShop();
            petshop.NewPurchase += _view.OnPurchaseAdded;
            petshop.PurchaseToDep += _view.OnPurchaseProcessed;
            petshop.PostponePurchase += _view.OnPurchasePostponed;
            petshop.DeliveryFinished += _view.OnPurchaseDelivered;
            petshop.FinishWork += _view.OnSimulationFinished;
        }

        private void OnStart(int size)
        {
            var context = _view.Context;
            petshop.Manage(size, context);
        }

        ~Presenter()
        {
            Environment.Exit(0);
        }
    }
}