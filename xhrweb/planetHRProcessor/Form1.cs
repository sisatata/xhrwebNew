using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace planetHRProcessor
{
    public partial class Form1 : Form
    {
        // Static form. Null if no form created yet.
        public static Form1 form = null;
        public static Boolean IsRunning;
        private delegate void EnableDelegate(bool enable);
        string IsDBBatch = ConfigurationManager.AppSettings["IsDBBatch"];
        private delegate void UpdateDelegate(string pMsg);
        public Form1()
        {
            InitializeComponent();
            form = this;
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            JobStart();
        }
        private void JobStart()
        {
            if (btnStart.Text == "S T A R T")
            {
                listBox1.Items.Add(string.Format("{0} : Running-- good day!!!", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
                IsRunning = true;
                JobManager jobManager = new JobManager();
                jobManager.ExecuteAllJobs();
                btnStart.Text = "S T O P";
                btnStart.BackColor = Color.Red;
                btnStart.ForeColor = Color.Black;
            }
            else if (btnStart.Text == "S T O P")
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure to close?", "Confirm", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    IsRunning = false;
                    btnStart.Text = "S T A R T";
                    btnStart.BackColor = Color.LightGray;
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }

            }
        }
        // Static method, call the non-static version if the form exist.
        public static void EnableStaticTextBox(bool enable)
        {
            if (form != null)
                form.EnableTextBox(enable);

        }

        private void EnableTextBox(bool enable)
        {
            // If this returns true, it means it was called from an external thread.
            if (InvokeRequired)
            {
                // Create a delegate of this method and let the form run it.
                this.Invoke(new EnableDelegate(EnableTextBox), new object[] { enable });
                return; // Important
            }

            // Set textBox
            //textBox1.Enabled = enable;
        }

        // Static method, call the non-static version if the form exist.
        public static void UpdateStaticTextBox(string pMsg)
        {
            if (form != null)
                form.UpdateText(pMsg);

        }

        private void UpdateText(string pMsg)
        {
            // If this returns true, it means it was called from an external thread.
            if (InvokeRequired)
            {
                // Create a delegate of this method and let the form run it.
                this.Invoke(new UpdateDelegate(UpdateText), new object[] { pMsg });
                //textBox1.Text = textBox1.Text + "\nhello";
                return; // Important
            }

            // Set textBox
            if (listBox1.Items.Count > 399)
                listBox1.Items.Clear();
            listBox1.Items.Add(pMsg);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string IsAutoStart = string.IsNullOrEmpty(ConfigurationManager.AppSettings["IsAutoStart"]) ? "false" : ConfigurationManager.AppSettings["IsAutoStart"].ToString();
            if (IsAutoStart.ToLower() == "true")
            {
                JobStart();
            }
        }

        //private void BindGrid()
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection myConnection = new SqlConnection(ConfigurationManager.AppSettings["RumConnectionString"]))
        //    {
        //        SqlDataAdapter myCommand = new SqlDataAdapter("SELECT [ProcessCode] [Process Code],[ProcessName] [Process Name],[Frequency],[Runevery] [Run Every] FROM [T_BatchProcess] WHERE Active = 'A'", myConnection);
        //        myCommand.SelectCommand.CommandType = CommandType.Text;

        //        myConnection.Open();
        //        myCommand.Fill(dt);
        //    }
        //    //dataGridView1.DataSource = dt;
        //    //   dataGridView1.AutoGenerateColumns = false;
        //}

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnStart.Text != "S T A R T")
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure to close?", "Confirm", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
