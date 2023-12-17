namespace CorpLink
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var s = new SQLHelper("Server=localhost;port=3306;Database=phone_book;Uid=root;Pwd=12345;");
        }
    }
}