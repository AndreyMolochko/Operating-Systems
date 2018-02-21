using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
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
            mainFunction();
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

            try { Process currentProcess = Process.GetProcessById(processId);
                count = currentProcess.Threads.Count;
                return count.ToString();
            }
            catch { return "-----"; }

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

        public String[] getPrioriteThreads(int i)
        {
            String[] arr = new String[listProcesses[i].Threads.Count];
            for (int j = 0; j < arr.Length; j++)
            {
                arr[j] = "prior = " + listProcesses[i].Threads[j].CurrentPriority.ToString();
            }
            return arr;
        }

        public String[] getIdThreads(int i)
        {
            String[] arr = new String[listProcesses[i].Threads.Count];
            for (int j = 0; j < arr.Length; j++)
            {
                arr[j] = "  id = " + listProcesses[i].Threads[j].Id.ToString();
            }
            return arr;
        }

        public String getCurrentPrior(int i) {
            String str;
            try { return listProcesses[i].PriorityClass.ToString(); }
            catch
            {
                return "------";
            }
        }
        public string getProcessOwner(int processId)
        {
            string query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList)
            {
                string[] argList = new string[2];
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    // return DOMAIN\user
                    return argList[1] + "\\" + argList[0];
                }
            }

            return "There isn't owner";
        }        

        public void mainFunction()
        {
            long counterSizeMemory = 0;
            Process ps;
            for (int i = 0; i < listProcesses.Length; i++)  
                listBox1.Items.Add("name = " + getName(i) +
                    " id = " + getId(i) +
                    " thread = " + getCountThreads(listProcesses[i].Id) +
                    " sizeVirtualMemory = " + getSizeVirtMemory(i) + " ( kb )" +
                    " sizePhysicalMemory = " + getSizePhysicMemory(i) + " ( kb ) " +
                    //" curPrioritet(T) = " + getCurPrioritet(i) +
                    " curPrioritet(P) = " + getCurrentPrior(i) +
                    " owner = " + getProcessOwner(listProcesses[i].Id));
            label1.Text = "Current progresses " + listProcesses.Length;
        }       
    
            
        


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox2.Items.AddRange(getPrioriteThreads(listBox1.SelectedIndex));
            listBox3.Items.Clear();
            listBox3.Items.AddRange(getIdThreads(listBox1.SelectedIndex));
            
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            mainFunction();
        }
    }
}
