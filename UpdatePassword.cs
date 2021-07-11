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
    public partial class UpdatePassword : Form
    {
        MySqlHelper MySqlHelper = new MySqlHelper();

        int user_id;
 
        public UpdatePassword()
        {
            InitializeComponent();
        }

        public UpdatePassword(int user_id)
        {
            InitializeComponent();
            this.user_id = user_id;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string password = textBox2.Text;
            string repassword = textBox3.Text;

            if(password == "" || repassword == "")
            {
                MessageBox.Show("请将信息填写完整再提交！", "修改提示");
            }
            else if(!password.Equals(repassword))
            {
                MessageBox.Show("请正确重复一次密码！", "修改提示");
            }
            else
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("update user set password = '{0}' where id = {1}", password, user_id);
                MySqlHelper.insertOrDeleteOrupdate(sql);
                
                
                MessageBox.Show("修改成功！", "修改提示");

                this.Hide();
                
                Main main = new Main();

                main.ShowDialog();

                this.Dispose();
            }
        }
    }
}
