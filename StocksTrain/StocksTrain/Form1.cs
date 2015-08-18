using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StocksTrain
{
    public partial class Form1 : Form
    {
        BarchartParser barchartData;
        static int TIME_BETWEEN_UPDATES = 6;
        int secondsLeft;

        public Form1()
        {
            InitializeComponent();
            barchartData = new BarchartParser();
            secondsLeft = TIME_BETWEEN_UPDATES;
            updateTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.stocksGridView.DataSource = barchartData.Stocks;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            barchartData.UpdateBarchartData();
            this.stocksGridView.DataSource = barchartData.Stocks;
        }

        delegate void ChangeMyTextDelegate(Control ctrl, object text);
        public static void ChangeMyText(Control ctrl, object text)
        {
            if (ctrl.InvokeRequired)
            {
                ChangeMyTextDelegate del = new ChangeMyTextDelegate(ChangeMyText);
                ctrl.Invoke(del, ctrl, text);
            }
            else
            {
                if (ctrl is DataGridView)
                    ((DataGridView)ctrl).DataSource = text;
                else
                    ctrl.Text = (string)text;
            }
        }

        private void UpdateTable()
        {
            barchartData.UpdateBarchartData();
            ChangeMyText(stocksGridView, barchartData.Stocks);
            ChangeMyText(updateButton, "Updating in " + secondsLeft);
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            secondsLeft = ((secondsLeft - 1) + TIME_BETWEEN_UPDATES) % TIME_BETWEEN_UPDATES;
            if (secondsLeft == 0)
            {
                updateButton.Text = "Updating...";
                Thread updateList = new Thread(UpdateTable);
                updateList.Start();
            }
            else
            {
                updateButton.Text = "Updating in " + secondsLeft;
            }
        }
    }
}
