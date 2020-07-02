namespace petShopModel
{
    partial class View
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStart = new System.Windows.Forms.Button();
            this.textBoxPurchases = new System.Windows.Forms.RichTextBox();
            this.textBoxCart = new System.Windows.Forms.RichTextBox();
            this.textBoxDepartments = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Scene = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 644);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(268, 48);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "старт";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxPurchases
            // 
            this.textBoxPurchases.Location = new System.Drawing.Point(1013, 60);
            this.textBoxPurchases.Name = "textBoxPurchases";
            this.textBoxPurchases.Size = new System.Drawing.Size(353, 148);
            this.textBoxPurchases.TabIndex = 5;
            this.textBoxPurchases.Text = "";
            // 
            // textBoxCart
            // 
            this.textBoxCart.Location = new System.Drawing.Point(1013, 245);
            this.textBoxCart.Name = "textBoxCart";
            this.textBoxCart.Size = new System.Drawing.Size(353, 162);
            this.textBoxCart.TabIndex = 6;
            this.textBoxCart.Text = "";
            // 
            // textBoxDepartments
            // 
            this.textBoxDepartments.Location = new System.Drawing.Point(1013, 449);
            this.textBoxDepartments.Name = "textBoxDepartments";
            this.textBoxDepartments.Size = new System.Drawing.Size(353, 220);
            this.textBoxDepartments.TabIndex = 7;
            this.textBoxDepartments.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1010, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "запросы на покупку:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1010, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "распределение по отделам:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1010, 421);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "процессы в отделе:";
            // 
            // Scene
            // 
            this.Scene.Location = new System.Drawing.Point(17, 19);
            this.Scene.Name = "Scene";
            this.Scene.Size = new System.Drawing.Size(956, 546);
            this.Scene.TabIndex = 11;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1393, 704);
            this.Controls.Add(this.Scene);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDepartments);
            this.Controls.Add(this.textBoxCart);
            this.Controls.Add(this.textBoxPurchases);
            this.Controls.Add(this.buttonStart);
            this.Name = "View";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.RichTextBox textBoxPurchases;
        private System.Windows.Forms.RichTextBox textBoxCart;
        private System.Windows.Forms.RichTextBox textBoxDepartments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel Scene;
    }
}

