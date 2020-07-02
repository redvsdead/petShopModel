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
            ScenePanel = Scene.CreateGraphics();
            ScenePanel.Clip = new Region(new Rectangle(0, 0, Scene.Width, Scene.Height));
            ScenePanel.InterpolationMode = InterpolationMode.High;
        }
        public event Action<int> Start;
        private readonly Graphics ScenePanel; //здесь рисуется визуальная часть       
        //вспомогательные функции для рисования
        //для задержки анимации
        const int DelayTime = 1;
        private async Task DelayAnimation(int duration)
        {
            await Task.Delay(duration);
        }
        //для того, чтобы понять, в каком месте рисовать отдел
        private Point GetDepartmentPos(Purchase purchase)
        {
            switch (purchase)
            {
                case RodentPurchase _:
                    return new Point(400, 70);
                case BirdPurchase _:
                    return new Point(400, 180);
                default:
                    return new Point(400, 290);
            }
        }
        //возвращает картинку отдела
        private Image GetDepatmentPic(Purchase purchase)
        {
            switch (purchase)
            {
                case RodentPurchase _:
                    return RodentPng;
                case BirdPurchase _:
                    return BirdPng;
                default:
                    return FishPng;
            }
        }
        //сравнивает точки, чтобы рисовать анимацию до тех пор пока она не дойдет до нужной точки
        private bool Equals(PointF p1, PointF p2)
        {
            const float epsilon = 10;
            return Math.Abs(p1.X - p2.X) < epsilon && Math.Abs(p1.Y - p2.Y) < epsilon;
        }
        //картинки:
        private readonly Image RodentPng = Image.FromFile("../../pics/rodent.png");
        private readonly Image BirdPng = Image.FromFile("../../pics/bird.png");
        private readonly Image FishPng = Image.FromFile("../../pics/fish.png");
        private readonly Image ContrPng = Image.FromFile("../../pics/contraction.png");
        private readonly Image DelivPng = Image.FromFile("../../pics/delivery.png");
        private readonly Image CartPng = Image.FromFile("../../pics/cart.png");
        private readonly Image ProcessPng = Image.FromFile("../../pics/processing.jpg");
        private readonly Image PurchasePng = Image.FromFile("../../pics/purchase.png");
        //рисует зоомагазин
        private void DrawPetShop()
        {
            ScenePanel.DrawImage(CartPng, new Rectangle(0, 0, 800, 450));
            ScenePanel.DrawImage(RodentPng, new Rectangle(400, 70, 100, 100));
            ScenePanel.DrawImage(BirdPng, new Rectangle(400, 180, 100, 100));
            ScenePanel.DrawImage(FishPng, new Rectangle(400, 290, 100, 100));
        }
        //рисует анимацию поступившей покупки
        private async void DrawPurchase()
        {
            var speed = 3;
            var imgSize = 50;
            var position = new Point(70, 40);

            ScenePanel.DrawImage(PurchasePng, new Rectangle(position.X, position.Y, imgSize, imgSize));

            while (position.Y < 230)
            {
                ScenePanel.DrawImage(PurchasePng, new Rectangle(position.X, position.Y, imgSize, imgSize));
                await DelayAnimation(DelayTime);
                ScenePanel.FillRectangle(Brushes.White, new Rectangle(position.X, position.Y, imgSize, imgSize));
                position.Y += speed;
            }

            ScenePanel.FillRectangle(Brushes.White, new Rectangle(position.X, position.Y, imgSize, imgSize));
        }
        //рисует анимацию отправки в отдел
        private async void DrawToDep(Purchase purchase)
        {
            var speed = 3;
            var imgSize = 50;
            var position = new PointF(250, 500);

            PointF TargetDepartment = GetDepartmentPos(purchase);
            PointF TargetPos = new PointF { X = TargetDepartment.X - imgSize, Y = TargetDepartment.Y + 20 };

            float deltaX = TargetPos.X - position.X;
            float deltaY = TargetPos.Y - position.Y;
            float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            float coeff = speed / distance;
            while (!Equals(position, TargetPos))
            {
                ScenePanel.DrawImage(PurchasePng, new RectangleF(position.X, position.Y, imgSize, imgSize));
                await DelayAnimation(DelayTime);
                ScenePanel.FillRectangle(Brushes.White, new RectangleF(position.X, position.Y, imgSize, imgSize));
                position.X += deltaX * coeff;
                position.Y += deltaY * coeff;
            }

            ScenePanel.FillRectangle(Brushes.White, new RectangleF(position.X, position.Y, imgSize, imgSize));
            ScenePanel.DrawImage(ProcessPng, new RectangleF(TargetDepartment.X, TargetDepartment.Y, 100, 100));
        }

        private async void DrawContraction(Purchase purchase)
        {
            var speed = 3;
            var imgSize = 50;
            PointF TargetDepartment = GetDepartmentPos(purchase);
            PointF position = new PointF { X = TargetDepartment.X + 550, Y = TargetDepartment.Y + 20};

            Image DepPic = GetDepatmentPic(purchase);
            ScenePanel.DrawImage(DepPic, new RectangleF(TargetDepartment.X, TargetDepartment.Y, 100, 100));

            var targetPosition = new PointF(TargetDepartment.X + imgSize, TargetDepartment.Y);

            float deltaX = position.X - targetPosition.X;
            float deltaY = position.Y - targetPosition.Y;
            float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            float coeff = speed / distance;

            while (position.X > 550)
            {
                ScenePanel.DrawImage(ContrPng, new RectangleF(position.X, position.Y, imgSize, imgSize));
                await DelayAnimation(DelayTime);
                ScenePanel.FillRectangle(Brushes.White, new RectangleF(position.X, position.Y, imgSize, imgSize));
                position.X -= deltaX * coeff;
            }

            ScenePanel.FillRectangle(Brushes.White, new RectangleF(position.X, position.Y, imgSize, imgSize));
        }

        private async void DrawDelivery(Purchase purchase)
        {
            var speed = 3;
            var imgSize = 100;
            PointF TargetDepartment = GetDepartmentPos(purchase);
            PointF position = new PointF { X = TargetDepartment.X + imgSize, Y = TargetDepartment.Y + 20 };

            Image DepPic = GetDepatmentPic(purchase);
            ScenePanel.DrawImage(DepPic, new RectangleF(TargetDepartment.X, TargetDepartment.Y, 100, 100));

            var targetPosition = new PointF(TargetDepartment.X + 550, TargetDepartment.Y);

            float deltaX = targetPosition.X - position.X;
            float deltaY = targetPosition.Y - position.Y;
            float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            float coeff = speed / distance;
            
            while (position.X < 800)
            {
                ScenePanel.DrawImage(DelivPng, new RectangleF(position.X, position.Y, imgSize, imgSize));
                await DelayAnimation(DelayTime);
                ScenePanel.FillRectangle(Brushes.White, new RectangleF(position.X, position.Y, imgSize, imgSize));
                position.X += deltaX * coeff;
            }

            ScenePanel.FillRectangle(Brushes.White, new RectangleF(position.X, position.Y, imgSize, imgSize));
        }

        public void OnSimulationFinished()
        {
            buttonStart.Enabled = true;
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            textBoxCart.Clear();
            textBoxDepartments.Clear();
            textBoxPurchases.Clear();
            DrawPetShop();
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
                DrawPurchase();
            }));
        }
        public void OnPurchaseProcessed(Purchase purchase)
        {
            textBoxCart.Text += $"Отправлено в отдел: {purchase}\n";
            DrawToDep(purchase);
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
                DrawDelivery(purchase);
            }));
        }
        public void OnContracted(Purchase purchase, Contractor contractor)
        {
            textBoxDepartments.Invoke(new Action(() =>
            {
                textBoxDepartments.Text += $"{contractor} доставил товары на склад.\n";
                DrawContraction(purchase);
            }));
        }
    }
}
