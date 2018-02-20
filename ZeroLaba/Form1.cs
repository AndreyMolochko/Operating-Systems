using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZeroLaba
{
    public partial class Form1 : Form
    {
        Process[] listProcesses;
        public Form1()
        {
            InitializeComponent();
            listProcesses = Process.GetProcesses();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            long counterSizeMemory = 0;
            Process ps;
            for (int i = 0; i < listProcesses.Length; i++)
            {

                listBox1.Items.Add("name = " + getName(i) +
                    " id = " + getId(i) +
                    " thread = " + getCountThreads(listProcesses[i].Id) +
                    " sizeVirtualMemory = " + getSizeVirtMemory(i) + " ( kb )" +
                    " sizePhysicalMemory = " + getSizePhysicMemory(i) + " ( kb ) " +
                    " curPrioritet(T) = " + getCurPrioritet(i) +
                    " idThread = ");

            }
            label1.Text = "Current progresses " + listProcesses.Length;
            //label1.Text= 

            //listBox1.Items.Add(listProcesses[0]);

        }
        public String getName(int i)
        {
            return listProcesses[i].ProcessName.ToString();
        }

        public String getId(int i)
        {
            return listProcesses[i].Id.ToString();
        }

        public String getCountThreads(int processId)
        {
            int count = 0;
            Process currentProcess = Process.GetProcessById(processId);
            count = currentProcess.Threads.Count;
            return count.ToString();
        }

        public String getSizeVirtMemory(int i)
        {
            return (listProcesses[i].PagedMemorySize64 / 1024).ToString();
        }

        public String getSizePhysicMemory(int i)
        {
            return (listProcesses[i].WorkingSet64 / 1024).ToString();            
        }

        public String getCurPrioritet(int i)
        {
            return listProcesses[i].BasePriority.ToString();
        }
        

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }
}
