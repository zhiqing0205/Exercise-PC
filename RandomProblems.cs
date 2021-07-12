﻿using MySql.Data.MySqlClient;
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
    public partial class RandomProblems : Form
    {
        MySqlHelper mysql = new MySqlHelper();

        public RandomProblems()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Exercise exercise = null;

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT count(*) FROM problem " +
                "where problem.id NOT IN (SELECT DISTINCT(problem_id) FROM solve WHERE user_id = {0})", Login.userId);

            MySqlDataReader reader = mysql.selectDataForRead(sql);
            reader.Read();
            int count = reader.GetInt32(0);

            if (count == 0)
            {
                MessageBox.Show("恭喜你你已完成全部题目！", "刷题提示");
                return;
            }

            this.MdiParent.Hide();
            exercise = new Exercise(this.MdiParent, Login.userId, true);
            exercise.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Exercise exercise = new Exercise(this, Login.userId, false);
            exercise.ShowDialog();
        }
    }
}
