namespace Warsztat
{
    partial class Setting_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting_Form));
            this.LangUA = new System.Windows.Forms.Button();
            this.LangPL = new System.Windows.Forms.Button();
            this.NameCompany = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Konto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AdresCompany = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.NumerBDD = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LangUA
            // 
            this.LangUA.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.LangUA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LangUA.Image = ((System.Drawing.Image)(resources.GetObject("LangUA.Image")));
            this.LangUA.Location = new System.Drawing.Point(12, 12);
            this.LangUA.MaximumSize = new System.Drawing.Size(200, 200);
            this.LangUA.Name = "LangUA";
            this.LangUA.Size = new System.Drawing.Size(98, 34);
            this.LangUA.TabIndex = 1;
            this.LangUA.UseVisualStyleBackColor = false;
            this.LangUA.Click += new System.EventHandler(this.UA_Click);
            // 
            // LangPL
            // 
            this.LangPL.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.LangPL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LangPL.Image = ((System.Drawing.Image)(resources.GetObject("LangPL.Image")));
            this.LangPL.Location = new System.Drawing.Point(125, 12);
            this.LangPL.MaximumSize = new System.Drawing.Size(200, 200);
            this.LangPL.Name = "LangPL";
            this.LangPL.Size = new System.Drawing.Size(98, 34);
            this.LangPL.TabIndex = 3;
            this.LangPL.UseVisualStyleBackColor = false;
            this.LangPL.Click += new System.EventHandler(this.PL_Click);
            // 
            // NameCompany
            // 
            this.NameCompany.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.NameCompany.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NameCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameCompany.Location = new System.Drawing.Point(12, 77);
            this.NameCompany.Multiline = true;
            this.NameCompany.Name = "NameCompany";
            this.NameCompany.Size = new System.Drawing.Size(211, 20);
            this.NameCompany.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nazwa firmy";
            // 
            // NIP
            // 
            this.NIP.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.NIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NIP.Location = new System.Drawing.Point(12, 158);
            this.NIP.Multiline = true;
            this.NIP.Name = "NIP";
            this.NIP.Size = new System.Drawing.Size(211, 20);
            this.NIP.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "NIP";
            // 
            // Konto
            // 
            this.Konto.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Konto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Konto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Konto.Location = new System.Drawing.Point(12, 198);
            this.Konto.Multiline = true;
            this.Konto.Name = "Konto";
            this.Konto.Size = new System.Drawing.Size(211, 20);
            this.Konto.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 182);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Konto";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Adres firmy";
            // 
            // AdresCompany
            // 
            this.AdresCompany.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.AdresCompany.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AdresCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AdresCompany.Location = new System.Drawing.Point(12, 119);
            this.AdresCompany.Multiline = true;
            this.AdresCompany.Name = "AdresCompany";
            this.AdresCompany.Size = new System.Drawing.Size(211, 20);
            this.AdresCompany.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Numer BDD";
            // 
            // NumerBDD
            // 
            this.NumerBDD.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.NumerBDD.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumerBDD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NumerBDD.Location = new System.Drawing.Point(12, 243);
            this.NumerBDD.Multiline = true;
            this.NumerBDD.Name = "NumerBDD";
            this.NumerBDD.Size = new System.Drawing.Size(211, 20);
            this.NumerBDD.TabIndex = 6;
            // 
            // Setting_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(234, 361);
            this.Controls.Add(this.NumerBDD);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Konto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LangPL);
            this.Controls.Add(this.LangUA);
            this.Controls.Add(this.NIP);
            this.Controls.Add(this.AdresCompany);
            this.Controls.Add(this.NameCompany);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MaximumSize = new System.Drawing.Size(250, 400);
            this.MinimumSize = new System.Drawing.Size(250, 400);
            this.Name = "Setting_Form";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button LangUA;
        public System.Windows.Forms.Button LangPL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        protected internal System.Windows.Forms.TextBox Konto;
        protected internal System.Windows.Forms.TextBox AdresCompany;
        protected internal System.Windows.Forms.TextBox NumerBDD;
        protected internal System.Windows.Forms.TextBox NameCompany;
        protected internal System.Windows.Forms.TextBox NIP;
        public System.Windows.Forms.Label label2;
    }
}