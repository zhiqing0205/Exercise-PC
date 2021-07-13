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
    public partial class Login : Form
    {
        MySqlHelper mysql = new MySqlHelper();

        public static int userId;

        public static string userName;

        public Login()
        {
            InitializeComponent();
        }

        //登录
        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text.ToString();
            string password = textBox2.Text.ToString();

            if (user == string.Empty || password == string.Empty)
            {
                MessageBox.Show("用户名或者密码不能为空！", "登录提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //select * from b_user where account = 'user';
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("select * from user " +
                    "where account = '{0}';", user);

                MySqlDataReader reader = mysql.selectDataForRead(sql);
                if (!reader.Read())
                {
                    MessageBox.Show("用户名不存在！", "登录提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (password != reader.GetString("password"))
                    {
                        MessageBox.Show("密码错误！", "登录提示",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        textBox2.Text = "";
                    }
                    else
                    {
                        userId = reader.GetInt32("id");

                        sql.Clear();
                        sql.AppendFormat("SELECT count(*) FROM loginlog " +
                            "WHERE user_id = {0}" , userId);

                        reader = mysql.selectDataForRead(sql);
                        reader.Read();
                        int count = reader.GetInt32(0);

                        //MessageBox.Show(count + "" + userId);

                        sql.Clear();
                        sql.AppendFormat("select name from user where id = {0}", userId);
                        reader = mysql.selectDataForRead(sql);
                        reader.Read();
                        userName = reader.GetString("name");


                        if(count == 0)
                        {
                            MessageBox.Show("检测到您是第一次登录系统，请修改密码！", "登录提示");

                            sql.Clear();

                            sql.AppendFormat("insert into loginlog values({0},1,now())", userId);
                            mysql.insertOrDeleteOrupdate(sql);

                            this.Hide();

                            UpdatePassword updatePassword = new UpdatePassword(userId);

                            updatePassword.ShowDialog();
                        }
                        else
                        {
                            this.Hide();

                            sql.Clear();

                            sql.AppendFormat("select days from loginlog where user_id = {0}", userId);
                            reader = mysql.selectDataForRead(sql);

                            reader.Read();
                            count = reader.GetInt32(0);


                            sql.Clear();
                            sql.AppendFormat("select last_time from loginlog where user_id = {0}", Login.userId);

                            reader = mysql.selectDataForRead(sql);

                            reader.Read();
                            DateTime time = reader.GetDateTime(0);

                            string str1 = "00:00";
                            DateTime start = Convert.ToDateTime(str1);
                            
                            if (time < start)
                            {
                                sql.Clear();
                                sql.AppendFormat("update loginlog set days = days + 1 " +
                                    "where user_id = {0}" , Login.userId);

                                mysql.insertOrDeleteOrupdate(sql);

                                count++;
                            }

                            sql.Clear();
                            sql.AppendFormat("update loginlog set last_time = now() " +
                                "where user_id = {0}", Login.userId);

                            mysql.insertOrDeleteOrupdate(sql);

                            MessageBox.Show(Login.userName + "你好！\n欢迎开始你的第" + Convert.ToString(count) 
                                + "天刷题！\n现支持键盘选择选项和切题哟~\n现有超过50道错题是不能继续刷题哟~", "登录提示");

                            Main main = new Main();

                            main.ShowDialog();
                        }

                        this.Dispose();
                    }
                }

                MySqlHelper.conn.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminLogin adminLogin = new AdminLogin(this);
            adminLogin.ShowDialog();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //textBox1.Text = "0184418";
            //textBox2.Text = "ch0604.2020";
            //button1_Click(sender, e);
        }
    }
}
