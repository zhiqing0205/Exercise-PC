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
    public partial class Feedback : Form
    {
        int uid;

        int pid;

        public Feedback()
        {
            InitializeComponent();
        }

        public Feedback(int uid , int pid)
        {
            InitializeComponent();

            this.uid = uid;

            this.pid = pid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text == "")
            {
                MessageBox.Show("请输入反馈内容！", "反馈提示");
                return;
            }

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("insert into feedback values(null , {0} , {1} , {2} , now())", uid, pid, richTextBox1.Text);

            MySqlHelper mySqlHelper = new MySqlHelper();

            mySqlHelper.insertOrDeleteOrupdate(sql);

            MessageBox.Show("反馈成功！", "反馈提示");

            this.Dispose();
        }
    }
}
