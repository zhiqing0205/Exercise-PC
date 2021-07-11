using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace exercise
{
    public partial class Simulate : Form
    {
        MySqlHelper mySqlHelper = new MySqlHelper();


        public Simulate()
        {
            InitializeComponent();
        }

        public void Simulate_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 4;
            comboBox2.SelectedIndex = 4;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT count(*) FROM `user`,solve " +
                    "WHERE `user`.id = solve.user_id " +
                    "AND `user`.id = {0} " +
                    "AND solve.state < 0 ", Login.userId);

            MySqlDataReader reader = mySqlHelper.selectDataForRead(sql);
            reader.Read();
            int count = reader.GetInt32(0);

            //if (count > 50)
            //{
            //    MessageBox.Show("你的错题数量超过50题，请先强化错题！", "刷题提示");
            //    return;
            //}


            this.MdiParent.Hide();

            Exercise exercise = new Exercise(this.MdiParent, Login.userId, Convert.ToInt32(comboBox1.SelectedItem),
                Convert.ToInt32(comboBox2.SelectedItem), Convert.ToInt32(comboBox4.SelectedIndex), Convert.ToInt32(comboBox3.SelectedIndex + 2));

            exercise.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //this.MdiParent.Hide();

            SimulateLog simulateLog = new SimulateLog();

            simulateLog.ShowDialog();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox4.SelectedIndex == 1)
            {
                comboBox3.Items.Clear();

                comboBox3.Items.Add("C语言");

                comboBox3.Items.Add("C++");

                comboBox3.Items.Add("Java");

                comboBox3.Items.Add("JavaScript");

                comboBox3.Items.Add("C#");

                comboBox3.SelectedIndex = 0;
            }
            else
            {
                comboBox3.Items.Clear();

                comboBox3.Items.Add("C++");

                comboBox3.Items.Add("Java");

                comboBox3.Items.Add("JavaScript");

                comboBox3.Items.Add("C#");

                comboBox3.SelectedIndex = 0;
            }
        }
    }
}
