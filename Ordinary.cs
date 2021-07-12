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
    public partial class Ordinary : Form
    {
        List<string> refer_id = new List<string>();

        MySqlHelper mySqlHelper = new MySqlHelper();

        public static bool isRandom = false;
        
        
        public Ordinary()
        {
            InitializeComponent();
        }

        public void Ordinary_Load(object sender, EventArgs e)
        {
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("select id,content, " +
                "(SELECT count(DISTINCT(problem.id)) FROM type, solve, problem WHERE type.id = a.id and problem.type = a.id " +
                "AND solve.problem_id = problem.id AND solve.user_id = {0}),count from type as a", Login.userId);

            //清空数据
            dataGridView1.Columns.Clear();

            //二者的名字要一致
            dataGridView1.DataSource = mySqlHelper.selectDataToDataSource(sql, "list");
            dataGridView1.DataMember = "list";

            //先清空，再塞一个数进去，使得该列表下标从1开始
            refer_id.Clear();

            //将行号与数据库编号对应
            MySqlDataReader reader = mySqlHelper.selectDataForRead(sql);
            while (reader.Read())
                refer_id.Add(reader.GetString(0));
            //MySqlHelper.conn.Close();


            for (int i = 0; i < dataGridView1.RowCount ; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(i + 1);
                //MessageBox.Show("" + i);
                //dataGridView1.Rows[i].Height = 50;
            }

            dataGridView1.Columns[0].HeaderCell.Value = "编号";
            dataGridView1.Columns[1].HeaderCell.Value = "类别";
            dataGridView1.Columns[2].HeaderCell.Value = "完成数";
            dataGridView1.Columns[3].HeaderCell.Value = "总数";


            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 160;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 80;


            //列宽自适应
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                //dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //dataGridView1.Columns[i].DefaultCellStyle.Font.Size = 16;
            }

            
            // 添加折扣按钮
            DataGridViewButtonColumn discount = new DataGridViewButtonColumn();
            discount.Text = "刷题";
            discount.Name = "buttondiscount";
            discount.HeaderText = "操作";
            discount.UseColumnTextForButtonValue = true;
            discount.Width = 88;

            //按钮加入表格
            dataGridView1.Columns.Add(discount);

            int a = 0, b = 0;
            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                a += Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                b += Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString());
            }

            label2.Text = a + "/" + b;
            progressBar1.Value = 100 * a / b;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ordinary_Load(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //isRandom = checkBox1.Checked;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1 && e.RowIndex != dataGridView1.Rows.Count)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() != "0")
                {
                    StringBuilder sql = new StringBuilder();

                    sql.AppendFormat("SELECT count(*) FROM `user`,solve " +
                            "WHERE `user`.id = solve.user_id " +
                            "AND `user`.id = {0} " +
                            "AND solve.state < 0 ", Login.userId);

                    MySqlDataReader reader = mySqlHelper.selectDataForRead(sql);
                    reader.Read();
                    int count = reader.GetInt32(0);

                    if(count > 50)
                    {
                        MessageBox.Show("你的错题数量超过50题，请先强化错题！", "刷题提示");
                        return;
                    }


                    DialogResult result =
                        MessageBox.Show("检测到你已经完成一部分，是否继续完成？", "刷题", MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question);

                    bool flag = false;
                    if (result == DialogResult.Yes)
                    {
                        flag = true;
                        
                        if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() ==
                            dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString())
                        {
                            MessageBox.Show("你已完成该部分的题目！");
                            return;
                        }
                    }

                    //MessageBox.Show("" + flag);
                    Exercise exercise = new Exercise(this.MdiParent, refer_id[e.RowIndex], Login.userId,
                        flag);
                    this.MdiParent.Hide();
                    exercise.ShowDialog();

                    return;
                }
                else
                {
                    Exercise exercise = new Exercise(this.MdiParent, refer_id[e.RowIndex], 
                        Login.userId, false);

                    this.MdiParent.Hide();
                    exercise.ShowDialog();

                    return;
                }

                
            }
        }
    }
}
