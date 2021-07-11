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
    public partial class SimulateLog : Form
    {
        MySqlHelper mySqlHelper = new MySqlHelper();

        public SimulateLog()
        {
            InitializeComponent();
        }

        private void SimulateLog_Load(object sender, EventArgs e)
        {
            this.Text = Login.userName + "的模拟记录";
            
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT id,score,sum,cost,date " +
                "FROM simulatelog " +
                "WHERE user_id = {0} order by date desc ", Login.userId);

            //清空数据
            dataGridView1.Columns.Clear();

            //二者的名字要一致
            dataGridView1.DataSource = mySqlHelper.selectDataToDataSource(sql, "list");
            dataGridView1.DataMember = "list";

            for(int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
            }

            dataGridView1.Columns[0].HeaderCell.Value = "编号";
            dataGridView1.Columns[1].HeaderCell.Value = "得分";
            dataGridView1.Columns[2].HeaderCell.Value = "总分";
            dataGridView1.Columns[3].HeaderCell.Value = "用时";
            dataGridView1.Columns[4].HeaderCell.Value = "时间";

            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 140;
        }
    }
}
