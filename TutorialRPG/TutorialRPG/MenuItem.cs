namespace TutorialRPG
{
    public class MenuItem
    {
        public string LinkType { get; set; }
        public string LinkID { get; set; }
        public Image Image { get; set; }

        public override string ToString()
        {
            return Image.Text;
        }
    }
}
