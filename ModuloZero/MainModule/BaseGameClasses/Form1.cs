using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ModuloFramework.ItemSystem;

namespace ModuloZero.BaseGameClasses
{
    public partial class Form1 : Form
    {
        public List<Item> Items { get; set; }
        private Form GameWindow { get; set; }
        private Button EscapeButton { get; }

        public Form1(List<Item> items, Form gameWindow)
        {
            InitializeComponent();
            GameWindow = gameWindow;
            Items = items;
            EscapeButton = new Button()
            {
                Enabled = false,
                Visible = false
            };
            EscapeButton.Click += (sender, args) => Close();
            gameWindow.LocationChanged += (sender, args) =>
            {
                PutAtBottomLeftCornerOfGameWindow((Form)sender);
            };
        }

        private void PutAtBottomLeftCornerOfGameWindow(Form gameWindow)
        {
            Point pnt = gameWindow.Location;
            pnt.Offset(8, gameWindow.Height - Height - 8);
            DesktopLocation = pnt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CancelButton = EscapeButton;
            int y = 0;
            foreach (ItemControl control in Items.Select(item => new ItemControl(item)
            {
                Left = 0,
                Top = y
            }))
            {
                y += control.Height;
                panel1.Controls.Add(control);
            }
            PutAtBottomLeftCornerOfGameWindow(GameWindow);
        }
    }
}
