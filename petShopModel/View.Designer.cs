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
            this.richTextBoxPurchases = new System.Windows.Forms.RichTextBox();
            this.richTextBoxCart = new System.Windows.Forms.RichTextBox();
            this.richTextBoxDepartments = new System.Windows.Forms.RichTextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBoxPurchases
            // 
            this.richTextBoxPurchases.Location = new System.Drawing.Point(12, 46);
            this.richTextBoxPurchases.Name = "richTextBoxPurchases";
            this.richTextBoxPurchases.Size = new System.Drawing.Size(312, 263);
            this.richTextBoxPurchases.TabIndex = 0;
            this.richTextBoxPurchases.Text = "";
            // 
            // richTextBoxCart
            // 
            this.richTextBoxCart.Location = new System.Drawing.Point(384, 46);
            this.richTextBoxCart.Name = "richTextBoxCart";
            this.richTextBoxCart.Size = new System.Drawing.Size(312, 263);
            this.richTextBoxCart.TabIndex = 1;
            this.richTextBoxCart.Text = "";
            // 
            // richTextBoxDepartments
            // 
            this.richTextBoxDepartments.Location = new System.Drawing.Point(755, 65);
            this.richTextBoxDepartments.Name = "richTextBoxDepartments";
            this.richTextBoxDepartments.Size = new System.Drawing.Size(312, 263);
            this.richTextBoxDepartments.TabIndex = 3;
            this.richTextBoxDepartments.Text = "";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(146, 581);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(268, 48);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "button1";
            this.buttonStart.UseVisualStyleBackColor = true;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 674);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.richTextBoxDepartments);
            this.Controls.Add(this.richTextBoxCart);
            this.Controls.Add(this.richTextBoxPurchases);
            this.Name = "View";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxPurchases;
        private System.Windows.Forms.RichTextBox richTextBoxCart;
        private System.Windows.Forms.RichTextBox richTextBoxDepartments;
        private System.Windows.Forms.Button buttonStart;
    }
}

