using System.Windows.Forms;

namespace ModuloZero.BaseGameClasses
{
    public partial class ItemControl : UserControl
    {
        public ModuloFramework.ItemSystem.Item Item { get; set; }

        public ItemControl(ModuloFramework.ItemSystem.Item item)
        {
            InitializeComponent();
            Item = item;
        }

        private void ItemControl_Load(object sender, System.EventArgs e)
        {
            itemNameLabel.Text = Item.Name;
            itemTypeLabel.Text = Item.Type.ToString();
            itemPriceLabel.Text = Item.Price.ToString();
            descriptionBox.Text = Item.Description;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Control prev = Parent;
            while (!(prev is Form))
                prev = prev.Parent;
            
            ((Form)prev).Close();
        }
    }
}
