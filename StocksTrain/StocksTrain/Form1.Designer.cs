namespace StocksTrain
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stocksGridView = new System.Windows.Forms.DataGridView();
            this.updateButton = new System.Windows.Forms.Button();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stocksGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stocksGridView);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 380);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stocks";
            // 
            // stocksGridView
            // 
            this.stocksGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stocksGridView.Location = new System.Drawing.Point(6, 19);
            this.stocksGridView.Name = "stocksGridView";
            this.stocksGridView.Size = new System.Drawing.Size(608, 355);
            this.stocksGridView.TabIndex = 0;
            // 
            // updateButton
            // 
            this.updateButton.Enabled = false;
            this.updateButton.Location = new System.Drawing.Point(12, 10);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(110, 23);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "Updating in 5";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 1000;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 431);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Stocks";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stocksGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView stocksGridView;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Timer updateTimer;
    }
}

