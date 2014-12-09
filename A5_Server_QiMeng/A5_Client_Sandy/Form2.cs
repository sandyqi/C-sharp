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
    public partial class Form2 : Form
    {
        public SandyServiceInterface sandyService;
        public string cid = "";
        public List<int> pids;
        public List<int> quantities;
        public List<string> forList2;
        public Form2()
        {
            InitializeComponent();
            sandyService = Program.getClient;
            List<string> pList = sandyService.readTable("product");
            pids = new List<int>();
            quantities = new List<int>();
            forList2 = new List<string>();
            string header = String.Format("{0,-20}{1,-20}{2,-20}{3, -20}","PId","Name","Price","Quantity");
            forList2.Add(header);
            foreach (string s in pList) 
            {
                listBox1.Items.Add(s);
            }
        }
        public void setCid(string cid) 
        {
            this.cid = cid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && textBox1.Text != "")
            {
                //check if the productid exists firstly.
                string pInfo = listBox1.Items[listBox1.SelectedIndex].ToString();
                int pid = Convert.ToInt32(pInfo.Substring(0, 4));

                if (pids.Contains(pid))
                {
                    int index = 0;
                    for (int i = 0; i < pids.Count; i++)
                    {
                        if (pid == pids.ElementAt(i))
                        {
                            index = i;
                            break;
                        }
                    }
                    quantities[index] = quantities.ElementAt(index) + Convert.ToInt32(textBox1.Text);
                    forList2[index + 1] = pInfo + "   " + quantities.ElementAt(index);
                }
                else
                {
                    pids.Add(pid);
                    quantities.Add(Convert.ToInt32(textBox1.Text));
                    forList2.Add(pInfo + "   " + quantities.ElementAt(quantities.Count - 1));
                }

                //clear list2 
                listBox2.Items.Clear();
                // update list2
                foreach (string s in forList2)
                {
                    listBox2.Items.Add(s);
                }
            }
            else 
            {
                MessageBox.Show("Please complete the request");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sandyService.placeOrder(cid, pids, quantities);
            MessageBox.Show("Successsfully Placed an Order!","Welcome!");
        }
    }
}
