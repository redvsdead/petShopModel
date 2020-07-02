using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace petShopModel
{
    public partial class View : Form, IView
    {
        public SynchronizationContext Context { get; set; }
        public View()
        {
            InitializeComponent();
            Context = SynchronizationContext.Current;
        }
        public event Action<int> Start;

        public void OnSimulationFinished()
        {
            buttonStart.Enabled = true;
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            textBoxCart.Clear();
            textBoxDepartments.Clear();
            textBoxPurchases.Clear();
            Start?.Invoke(7);   //запускаем симуляцию с 7 заявками
            buttonStart.Enabled = false;
        }

        public void OnStockChanges(Department department)
        {
            textBoxDepartments.Invoke(new Action(() => 
            {
                textBoxDepartments.Text += $"На складе {department}: {department.Animals} и {department.Houses}.\n";
            }));
        }
        public void OnPurchaseAdded(Purchase purchase)
        {
            textBoxPurchases.Invoke(new Action(() =>
            {
                textBoxPurchases.Text += $"Добавлено в корзину: {purchase}\n";
            }));
        }
        public void OnPurchaseProcessed(Purchase purchase)
        {
            textBoxCart.Text += $"Отправлено в отдел: {purchase}\n";
        }
        public void OnPurchasePostponed(Purchase purchase)
        {
            textBoxCart.Text += $"Отложено: {purchase}\n";
        }
        public void OnPurchaseDelivered(Purchase purchase, DeliveryMan deliverer)
        {
            textBoxDepartments.Invoke(new Action(() =>
            {
                textBoxDepartments.Text += $"{deliverer} доставил покупку {purchase} по адресу {purchase.PurchaseAddress}.\n";
            }));
        }
        public void OnContracted(Purchase purchase, Contractor contractor)
        {
            textBoxDepartments.Invoke(new Action(() =>
            {
                textBoxDepartments.Text += $"{contractor} доставил товары на склад.\n";
            }));
        }
    }
}
