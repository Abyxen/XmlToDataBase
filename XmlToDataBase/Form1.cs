using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XmlToDataBase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();


            openFileDialog1.Filter = "XML Files (*.xml)|*.xml";


            string xmlPath = "";

            openFileDialog1.Multiselect = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
