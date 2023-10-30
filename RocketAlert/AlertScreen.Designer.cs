namespace RocketAlert
{
    partial class AlertScreen
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNamePlace = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Stencil", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(678, 323);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(588, 76);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rocket Alert In";
            // 
            // lblNamePlace
            // 
            this.lblNamePlace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNamePlace.AutoSize = true;
            this.lblNamePlace.Font = new System.Drawing.Font("Stencil", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamePlace.Location = new System.Drawing.Point(886, 442);
            this.lblNamePlace.Name = "lblNamePlace";
            this.lblNamePlace.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblNamePlace.Size = new System.Drawing.Size(225, 76);
            this.lblNamePlace.TabIndex = 1;
            this.lblNamePlace.Text = "ירושלים";
            // 
            // timer1
            // 
            this.timer1.Interval = 120000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Stencil", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(12, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // AlertScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(1278, 610);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblNamePlace);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AlertScreen";
            this.Text = "AlertScreen";
            this.Load += new System.EventHandler(this.AlertScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNamePlace;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnClose;
    }
}