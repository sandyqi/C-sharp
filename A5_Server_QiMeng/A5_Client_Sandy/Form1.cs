using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using A5_Client_Sandy.SS;

namespace A5_Client_Sandy
{
    public partial class Form1 : Form
    {
        public SandyServiceInterface sandyService;
        public string cid;
        public Form1()
        {
            InitializeComponent();
            sandyService = Program.getClient;
            List<string> customers = sandyService.readTable("customer");
            foreach (string s in customers) 
            {
                listBox1.Items.Add(s);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if (listBox1.SelectedIndex != -1)
            {
                cid = (listBox1.SelectedIndex).ToString();
                Console.WriteLine(cid);
                List<string> orders = sandyService.listPurchases(cid);
                foreach (string s in orders) 
                {
                    listBox2.Items.Add(s);
                }
            }
            else
            {
                MessageBox.Show("Please complete the request");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                cid = (listBox1.SelectedIndex).ToString();
                Form2 f2 = new Form2();
                f2.setCid(cid);
                f2.Show();
            }
            else
            {
                MessageBox.Show("Please complete the request");
            }
        }
    }
}
