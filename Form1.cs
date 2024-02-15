

namespace HostsFileEditor
{
    public partial class Form1 : Form
    {
        private List<HostEntry> hosts;
        private StreamReader sr;

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(List<HostEntry> hosts)
        {
            this.hosts = hosts;
            InitializeComponent();

        }

        public Form1(List<HostEntry> hosts, StreamReader sr) : this(hosts)
        {
            this.sr = sr;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }
    }
}
