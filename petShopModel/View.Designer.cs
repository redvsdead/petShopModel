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
            this.textBoxPurchases = new System.Windows.Forms.TextBox();
            this.textBoxCart = new System.Windows.Forms.TextBox();
            this.textBoxDepartments = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(146, 581);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(268, 48);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "тык";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxPurchases
            // 
            this.textBoxPurchases.Location = new System.Drawing.Point(38, 64);
            this.textBoxPurchases.Name = "textBoxPurchases";
            this.textBoxPurchases.Size = new System.Drawing.Size(330, 22);
            this.textBoxPurchases.TabIndex = 5;
            // 
            // textBoxCart
            // 
            this.textBoxCart.Location = new System.Drawing.Point(384, 64);
            this.textBoxCart.Name = "textBoxCart";
            this.textBoxCart.Size = new System.Drawing.Size(330, 22);
            this.textBoxCart.TabIndex = 6;
            // 
            // textBoxDepartments
            // 
            this.textBoxDepartments.Location = new System.Drawing.Point(730, 64);
            this.textBoxDepartments.Name = "textBoxDepartments";
            this.textBoxDepartments.Size = new System.Drawing.Size(330, 22);
            this.textBoxDepartments.TabIndex = 7;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 674);
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
        private System.Windows.Forms.TextBox textBoxPurchases;
        private System.Windows.Forms.TextBox textBoxCart;
        private System.Windows.Forms.TextBox textBoxDepartments;
    }
}

