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
    public partial class AdminLogin : Form
    {

        Form login = null;

        public AdminLogin()
        {
            InitializeComponent();
        }

        public AdminLogin(Form login)
        {
            InitializeComponent();
            this.login = login;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rightCode = DateTime.Now.ToString("HH")
                + DateTime.Now.ToString("mm");
            string inputCode = textBox1.Text.ToString();

            //MessageBox.Show(rightCode);

            if (inputCode != rightCode)
            {
                MessageBox.Show("效验码错误！", "登录提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                login.Dispose();
                this.Dispose();
            }
        }
    }
}
