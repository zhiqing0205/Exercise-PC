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
    public partial class Wrong : Form
    {

        MySqlHelper MySqlHelper = new MySqlHelper();

        int count = 0;

        public Wrong()
        {
            InitializeComponent();
        }

        public void Wrong_Load(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();
            
            sql.AppendFormat("SELECT count(DISTINCT(problem_id)) FROM `user`,solve " +
                    "WHERE `user`.id = solve.user_id " +
                    "AND `user`.id = {0} " +
                    "AND solve.state < 0 " , Login.userId);

            MySqlDataReader reader = MySqlHelper.selectDataForRead(sql);
            reader.Read();
            count = reader.GetInt32(0);

            label1.Text = "错题数量：" + count;

            if(count == 0)
            {
                button1.Enabled = false;
                label3.Visible = true;
            }
            else
            {
                button1.Enabled = true;
                label3.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.MdiParent.Hide();

            Exercise exercise = new Exercise(this.MdiParent, Login.userId);

            exercise.ShowDialog();
        }

        private void Wrong_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认要清空做题记录(模拟记录和登录记录会保留)？", "清空提示",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendFormat("DELETE FROM solve WHERE user_id = {0}", Login.userId);
                MySqlHelper.insertOrDeleteOrupdate(sql);

                MessageBox.Show("清空成功!");
                Wrong_Load(sender, e);
            }
        }
    }
}
