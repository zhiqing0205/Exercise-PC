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
    public partial class Rank : Form
    {
        List<string> refer_id = new List<string>();

        MySqlHelper mySqlHelper = new MySqlHelper();

        int rank = 0;

        public Rank()
        {
            InitializeComponent();
        }

        private void Rank_Load(object sender, EventArgs e)
        {
             StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT u.id,NAME," +
                "(SELECT name FROM class WHERE class.id = u.classid),count(*)," +
                "(SELECT ifnull(max(score), 0) FROM simulatelog WHERE user_id = u.id),days,last_time " +
                "FROM `user` as u,loginlog,solve " +
                "WHERE loginlog.user_id = u.id AND loginlog.user_id = solve.user_id " +
                "GROUP BY u.id " +
                "HAVING count(*) != 0 " +
                "ORDER BY count(*) DESC, last_time DESC ");

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
                if(dataGridView1.Rows[i].Cells[0].Value.ToString().Equals(Convert.ToString(Login.userId)))
                {
                    //MessageBox.Show("你排在第" + Convert.ToString(i + 1) + "名！");
                    rank = i + 1;
                    //dataGridView1.Rows[i].Cells[1].Style.Font = new Font("宋体", 10, FontStyle.Underline);
                }                
                dataGridView1.Rows[i].Cells[0].Value = Convert.ToString(i + 1);
            }

            dataGridView1.Columns[0].HeaderCell.Value = "排名";
            dataGridView1.Columns[1].HeaderCell.Value = "姓名";
            dataGridView1.Columns[2].HeaderCell.Value = "班级";
            dataGridView1.Columns[3].HeaderCell.Value = "完成数";
            dataGridView1.Columns[4].HeaderCell.Value = "模拟最高分";
            dataGridView1.Columns[5].HeaderCell.Value = "登录天数";
            dataGridView1.Columns[6].HeaderCell.Value = "最后登录时间";


            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 140;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 140;
            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[6].Width = 200;

            MessageBox.Show("你排在第" + Convert.ToString(rank) + "名！", "排名提示");
        }

    }
}
