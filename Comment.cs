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
    public partial class Comment : Form
    {
        MySqlHelper mySqlHelper = new MySqlHelper();

        int pid;

        int uid;

        Exercise exercise = null;

        public Comment()
        {
            InitializeComponent();
        }

        public Comment(Exercise exercise , string op , string answer , int uid , int pid)
        {
            InitializeComponent();

            this.exercise = exercise;

            this.uid = uid;

            this.pid = pid;

            richTextBox1.Text = "正确选项为：" + op + "\n" + answer;
        }


        private void Comment_Load(object sender, EventArgs e)
        {
           
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT user_id , content,content,date " +
                "FROM `comment` " +
                "WHERE problem_id = {0}", pid);

            //清空数据
            dataGridView1.Columns.Clear();

            //二者的名字要一致
            dataGridView1.DataSource = mySqlHelper.selectDataToDataSource(sql, "list");
            dataGridView1.DataMember = "list";


            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                int user_id = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value.ToString());

                dataGridView1.Rows[i].Cells[0].Value = i + 1;

                sql.Clear();

                sql.AppendFormat("SELECT name FROM user " +
                    "WHERE id = {0} ", user_id);

                MySqlDataReader reader = mySqlHelper.selectDataForRead(sql);
                reader.Read();

                dataGridView1.Rows[i].Cells[2].Value = reader.GetString(0);

                //MessageBox.Show(dataGridView1.Rows[i].Cells[3].Value.ToString());
            }

            dataGridView1.Columns[0].HeaderCell.Value = "编号";
            dataGridView1.Columns[1].HeaderCell.Value = "评论";
            dataGridView1.Columns[2].HeaderCell.Value = "姓名";
            dataGridView1.Columns[3].HeaderCell.Value = "时间";


            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 220;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 150;

            //dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //dataGridView1.AutoResizeRow(0,DataGridViewAutoSizeRowsMode.AllCells);
            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
        }

        private void Comment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Exercise.autoNextProblem)
            {
                exercise.button2_Click(null, null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Comment_FormClosing(sender, null);

            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(richTextBox2.Text.Trim() == "")
            {
                MessageBox.Show("评论不能为空！" , "评论提示");
                return;
            }
            else
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("insert into comment values(null , {0} , {1} , '{2}' , now())",
                    uid, pid, richTextBox2.Text);

                mySqlHelper.insertOrDeleteOrupdate(sql);

                Comment_Load(sender, e);

                richTextBox2.Text = "";

                MessageBox.Show("评论成功！", "评论提示");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Feedback feedback = new Feedback(uid, pid);

            feedback.ShowDialog();
        }

        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    switch (keyData)
        //    {
        //        case Keys.Escape: this.Dispose(); break;
               
        //    }


        //    return true;
        //    //return base.ProcessCmdKey(ref msg, keyData);
        //}
    }
}
