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
    public partial class Main : Form
    {
        MySqlHelper mysql = new MySqlHelper();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

            模拟练习ToolStripMenuItem_Click(sender, e);
        }

        private void 普通练习ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in MdiChildren)
            {
                if (form.Name != "Ordinary")
                {
                    form.Close();
                }
            }

            if (MdiChildren.Length == 0)
            {
                Ordinary ordinary = new Ordinary();
                ordinary.MdiParent = this;

                Height = ordinary.Height + 28;
                Width = ordinary.Width;

                ordinary.Show();
            }
        }

        public void 模拟练习ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in MdiChildren)
            {
                if (form.Name != "Simulate")
                {
                    form.Close();
                }
            }

            if (MdiChildren.Length == 0)
            {
                Simulate simulate = new Simulate();
                simulate.MdiParent = this;

                Height = simulate.Height + 28;
                Width = simulate.Width;

                simulate.Show();
            }
        }

        public void 错题练习ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in MdiChildren)
            {
                if (form.Name != "Wrong")
                {
                    form.Close();
                }
            }

            if (MdiChildren.Length == 0)
            {
                Wrong wrong = new Wrong();
                wrong.MdiParent = this;

                Height = wrong.Height + 28;
                Width = wrong.Width;

                wrong.Show();
            }
        }

        private void 刷题排行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in MdiChildren)
            {
                if (form.Name != "Rank")
                {
                    form.Close();
                }
            }

            if (MdiChildren.Length == 0)
            {
                Rank rank = new Rank();
                rank.MdiParent = this;

                Height = rank.Height + 28;
                Width = rank.Width;

                rank.Show();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否要退出？", "关闭提示",
               MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.Cancel)
            {
                e.Cancel = true;

                
            }
            else
            {
                System.Environment.Exit(0);
            }
            //Application.Exit();
            //Environment.Exit(0);
            //Application.ExitThread();

            


        }

        private void 随机刷题ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in MdiChildren)
            {
                if (form.Name != "RandomProblems")
                {
                    form.Close();
                }
            }

            if (MdiChildren.Length == 0)
            {
                RandomProblems randomProblems = new RandomProblems();
                randomProblems.MdiParent = this;

                Height = randomProblems.Height + 28;
                Width = randomProblems.Width;

                randomProblems.Show();
            }


            //this.Hide();
        }
    }
}
