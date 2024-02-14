
namespace HostsFileEditor
{
    public partial class Form1 : Form
    {
        private List<HostEntry> hosts;

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(List<HostEntry> hosts)
        {
            InitializeComponent();
            this.hosts = hosts;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
