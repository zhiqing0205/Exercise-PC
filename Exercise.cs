using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace exercise
{
    public partial class Exercise : Form
    {
        int uid;

        int type;
        
        int k = 0;

        int sum = 0;

        int num;

        int time;

        int temp_time;

        bool isExit = true;

        bool end = false;

        DateTime date;

        string op;
        
        Form form = null;

        public static bool autoOpenComment = true;

        public static bool autoNextProblem = true;

        public static bool hasComment = false;

        List<Problem> problem = new List<Problem>();

        MySqlHelper MySqlHelper = new MySqlHelper();

        [DllImport("user32.dll")]
        private static extern int SetCursorPos(int x, int y);

        public void SetMouseAtCenterScreen()
        {
            int winHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            int winWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            Point centerP = new Point(winWidth / 2, winHeight / 2);
            MoveMouseToPoint(centerP);
        }

        public void MoveMouseToPoint(Point p)
        {
            SetCursorPos(p.X, p.Y);
        }

        public Exercise()
        {
            InitializeComponent();
        }

        public Exercise(Form form , string type , int uid , bool flag)
        {
            InitializeComponent();

            this.form = form;

            this.uid = uid;

            this.type = 0;
            
            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT * " +
                "FROM problem " +
                "WHERE problem.type = '{0}' " , type);

            if(flag)
            {
                sql.AppendFormat("AND problem.id NOT IN " +
                    "(SELECT problem_id FROM solve WHERE user_id = {0}) " , uid);
            }

            //MessageBox.Show("" + sql.ToString());

            textBox1.Text = sql.ToString();

            textBox1.Text = sql.ToString();

            MySqlDataReader reader = MySqlHelper.selectDataForRead(sql);

            while(reader.Read())
            {
                problem.Add(new Problem(reader.GetInt32(0),reader.GetString(2), reader.GetString(3), reader.GetString(4)
                    , reader.GetString(5), reader.GetString(6) , reader.GetString(7)));

                sum++;
            }


            label3.Visible = false;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            //button4.Visible = false;
            timer1.Enabled = false;
            button4.Text = "反馈";

            var random = new Random();
            var newList = new List<Problem>();
            foreach (var item in problem)
            {
                newList.Insert(random.Next(newList.Count), item);
            }

            problem = newList;

            refresh();

            SetMouseAtCenterScreen();
            //MessageBox.Show(k + "" + sum);
        }

        public Exercise(Form form, int uid,  int num , int time , int group , int language)
        {
            InitializeComponent();

            this.form = form;

            this.uid = uid;

            this.type = 1;

            this.num = num;

            this.time = time * 60;

            this.temp_time = this.time;

            this.date = DateTime.Now;


            StringBuilder sql = new StringBuilder();

            if(group == 0)
            {
                int x1 = num * 8 / 10;
                sql.AppendFormat("(SELECT * FROM problem " +
                    "where type in ('1' , 'J', 'K','L','Q','R','S','W' , 'Y','Z','{0}') " +
                    "ORDER BY RAND() LIMIT {1})", language , x1);

                sql.AppendFormat(" UNION ALL (SELECT * FROM problem " +
                    "where type in ('C','D','H') ORDER BY RAND() LIMIT {0})" , num -  x1);
            }
            else
            {
                int x1 = num * 3 / 10;
                sql.AppendFormat("(SELECT * FROM problem " +
                    "where type in ( 'J', 'K','L','Q','R','S','W' , 'Y','Z','{0}') " +
                    "ORDER BY RAND() LIMIT {1})", language - 1 , x1 );

                sql.AppendFormat("UNION ALL (SELECT * FROM problem " +
                    "where type in ('C','D','H') ORDER BY RAND() LIMIT {0})", num - x1);
            }

            textBox1.Text = sql.ToString();

            //MessageBox.Show(num + "  " + time);

            MySqlDataReader reader = MySqlHelper.selectDataForRead(sql);

            while (reader.Read())
            {
                problem.Add(new Problem(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(4)
                    , reader.GetString(5), reader.GetString(6), reader.GetString(7)));

                sum++;
            }


            checkBox1.Visible = false;
            checkBox2.Text = "答完题自动到下一题";
            checkBox2.Checked = true;
            button3.Visible = false;

            var random = new Random();
            var newList = new List<Problem>();
            foreach (var item in problem)
            {
                newList.Insert(random.Next(newList.Count), item);
            }

            problem = newList;

            refresh();

            SetMouseAtCenterScreen();
        }

        public Exercise(Form form, int uid)
        {
            InitializeComponent();

            this.form = form;

            this.uid = uid;

            this.type = 2;

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT problem_id,`describe`, opA, opB, opC, opD, answer " +
                "FROM `user`, solve,problem " +
                "WHERE `user`.id = solve.user_id " +
                "AND `user`.id = {0} " +
                "AND problem_id = problem.id " +
                "AND solve.state < 0 " +
                "Order By state desc ", uid);

            textBox1.Text = sql.ToString();

            MySqlDataReader reader = MySqlHelper.selectDataForRead(sql);

            while (reader.Read())
            {
                problem.Add(new Problem(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)
                    , reader.GetString(4), reader.GetString(5), reader.GetString(6)));

                sum++;
            }


            label3.Visible = false;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            //button4.Visible = false;
            timer1.Enabled = false;
            button4.Text = "反馈";

            var random = new Random();
            var newList = new List<Problem>();
            foreach (var item in problem)
            {
                newList.Insert(random.Next(newList.Count), item);
            }

            problem = newList;

            refresh();
            //MessageBox.Show(k + "" + sum);
            SetMouseAtCenterScreen();
        }

        public Exercise(Form form, int uid, bool flag)
        {
            InitializeComponent();

            this.form = form;

            this.uid = uid;

            this.type = 0;

            this.form.Hide();

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat("SELECT * FROM problem ");

            if (flag)
            {
                sql.AppendFormat("where problem.id NOT IN " +
                    "(SELECT DISTINCT(problem_id) FROM solve WHERE user_id = {0}) ", uid);
            }

            //MessageBox.Show("" + sql.ToString());

            textBox1.Text = sql.ToString();

            

            textBox1.Text = sql.ToString();

            MySqlDataReader reader = MySqlHelper.selectDataForRead(sql);

            while (reader.Read())
            {
                problem.Add(new Problem(reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetString(4)
                    , reader.GetString(5), reader.GetString(6), reader.GetString(7)));

                sum++;
            }


            label3.Visible = false;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            //button4.Visible = false;
            timer1.Enabled = false;
            button4.Text = "反馈";

            var random = new Random();
            var newList = new List<Problem>();
            foreach (var item in problem)
            {
                newList.Insert(random.Next(newList.Count), item);
            }

            problem = newList;

            refresh();

            SetMouseAtCenterScreen();
            //MessageBox.Show(k + "" + sum);
        }

        private void Exercise_Load(object sender, EventArgs e)
        {
            //button2_Click(sender, e);

            //button1_Click(sender, e);
        }

        private void refresh()
        {
            label1.Text = "进度：" + Convert.ToString(k + 1) + "/" + sum;

            button1.Enabled = k != 0;

            button2.Enabled = k != sum - 1;

            textBox2.Text = "";

            richTextBox1.Text = "(id:" + Convert.ToString(problem[k].id) + ")题目：" + problem[k].content;

            radioButton1.Text = "A." + problem[k].ops[0];

            radioButton2.Text = "B." + problem[k].ops[1];

            radioButton3.Text = "C." + problem[k].ops[2];

            radioButton4.Text = "D." + problem[k].ops[3];

            radioButton1.ForeColor = radioButton2.ForeColor = radioButton3.ForeColor = radioButton4.ForeColor = Color.Black;

            radioButton1.Checked = radioButton2.Checked = radioButton3.Checked = radioButton4.Checked = false;

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select count(*) from comment where problem_id = {0}", problem[k].id);

            MySqlDataReader reader = MySqlHelper.selectDataForRead(sql);
            reader.Read();

            int count = reader.GetInt32(0);

            button3.Text = "评论(" + count + "条评论)";

            hasComment = count != 0;

            if (problem[k].selectIndex == 1)
                radioButton1.Checked = true;
            else if (problem[k].selectIndex == 2)
                radioButton2.Checked = true;
            else if (problem[k].selectIndex == 3)
                radioButton3.Checked = true;
            else if (problem[k].selectIndex == 4)
                radioButton4.Checked = true;

            string ops = "ABCD";
            op = "" + ops[problem[k].rightIndex - 1];

            if (problem[k].selectIndex != 0 && type != 1)
            {

                switch (problem[k].selectIndex)
                {
                    case 1: radioButton1.ForeColor = Color.Red; break;
                    case 2: radioButton2.ForeColor = Color.Red; break;
                    case 3: radioButton3.ForeColor = Color.Red; break;
                    case 4: radioButton4.ForeColor = Color.Red; break;
                }

                switch (problem[k].rightIndex)
                {
                    case 1: radioButton1.ForeColor = Color.Blue; break;
                    case 2: radioButton2.ForeColor = Color.Blue; break;
                    case 3: radioButton3.ForeColor = Color.Blue; break;
                    case 4: radioButton4.ForeColor = Color.Blue; break;
                }
            }

        }

        private void judge(string op)
        {
            string ans = op.Remove(0, 2);
            int x = Convert.ToInt32(op[0] - 'A') + 1;

            //MessageBox.Show(ans + "\n" + problem[k].answer + "\n" + x);

            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select count(*) from solve where user_id = '{0}' and  problem_id = {1}"
                                , this.uid, problem[k].id);

            MySqlDataReader reader = MySqlHelper.selectDataForRead(sql);
            reader.Read();

            int count = reader.GetInt32(0);

            if (ans == problem[k].answer)
            {
                sql.Clear();
                if(count == 0)
                {
                    sql.AppendFormat("insert into solve values(null , '{0}' , {1} , 0)", this.uid, problem[k].id);
                }
                else
                {
                    sql.AppendFormat("update solve set state = state + 1 where user_id = '{0}' and  problem_id = {1}"
                        , this.uid, problem[k].id);

                    //MySqlHelper.insertOrDeleteOrupdate(sql);         
                }
            }
            else
            {
                sql.Clear();
                if (count == 0)
                {
                    sql.AppendFormat("insert into solve values(null , '{0}' , {1} , -2)", this.uid, problem[k].id);
                }
                else
                {
                    sql.AppendFormat("update solve set state = -2 where user_id = '{0}' and  problem_id = {1}"
                        , this.uid, problem[k].id);
                }

            }

            textBox1.Text = sql.ToString();

            MySqlHelper.insertOrDeleteOrupdate(sql);


            refresh();


            int state;
            if (this.type == 2)
            {
                //refresh();

                sql.Clear();
                sql.AppendFormat("select state from solve  where user_id = '{0}' and  problem_id = {1}"
                    , this.uid, problem[k].id);

                reader = MySqlHelper.selectDataForRead(sql);
                reader.Read();

                state = reader.GetInt32(0);
                if (state >= 0)
                {
                    //refresh();

                    Thread.Sleep(300);
                    
                    MessageBox.Show("此题已从错题中移除！", "刷题提示");
                    
                    button2_Click(null, null);
                    return;
                }
            }

            //refresh();

            if (autoOpenComment && hasComment)
            {
                refresh();

                //MessageBox.Show(k + "");

                Thread.Sleep(300);

                button3_Click(null, null);
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //judge(radioButton1.Text);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //judge(radioButton2.Text);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //judge(radioButton3.Text);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            //judge(radioButton4.Text);
        }

        public void button2_Click(object sender, EventArgs e)
        {
            if(k + 1 >= sum)
            {
                MessageBox.Show("已经到达最后一题！", "刷题提示");
            }
            else
            {
                //MessageBox.Show("" + k);
                
                k++;
                refresh();
            }          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (k - 1 < 0)
            {
                MessageBox.Show("已经到达第一题！", "刷题提示");
            }
            else
            {
                //MessageBox.Show("" + k);

                k--;
                refresh();
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (problem[k].selectIndex == 0)
            {
                problem[k].selectIndex = 1;
                refresh();                
                
                if (this.type != 1)
                    judge(radioButton1.Text);
            }
            else
            {
                if(this.type == 1)
                {
                    problem[k].selectIndex = 1;
                    refresh();
                }
                    
            }

            if (this.type == 1 && autoNextProblem)
            {
                button2_Click(sender, e);
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (problem[k].selectIndex == 0)
            {
                problem[k].selectIndex = 2;
                refresh();               
                
                if (this.type != 1)
                    judge(radioButton2.Text);

            }
            else
            {
                if (this.type == 1)
                {
                    problem[k].selectIndex = 2;
                    refresh();
                }

            }

            if(this.type == 1 && autoNextProblem)
            {
                button2_Click(sender, e);
            }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (problem[k].selectIndex == 0)
            {
                problem[k].selectIndex = 3;
                refresh();

                if (this.type != 1)
                    judge(radioButton3.Text);         
            }
            else
            {
                if (this.type == 1)
                {
                    problem[k].selectIndex = 3;
                    refresh();
                }
            }

            if (this.type == 1 && autoNextProblem)
            {
                button2_Click(sender, e);
            }
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            if(problem[k].selectIndex == 0)
            {
                problem[k].selectIndex = 4;
                refresh();               
                
                if (this.type != 1)
                    judge(radioButton4.Text);
            }
            else
            {
                if (this.type == 1)
                {
                    problem[k].selectIndex = 4;
                    refresh();
                }

            }

            if (this.type == 1 && autoNextProblem)
            {
                button2_Click(sender, e);
            }
        }

        private void Exercise_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main main = new Main();

            if (type == 1)
            {
                if (!end)
                { 
                    button4_Click(sender, e);
                    if (!isExit)
                    {
                        e.Cancel = true;
                        return;
                    }
                
                }
                    
                main.模拟练习ToolStripMenuItem_Click(sender, e);
            }
            else if(type == 2)
            {
                main.错题练习ToolStripMenuItem_Click(sender, e);
            }

            this.Hide();
            
            main.ShowDialog();

            timer1.Enabled = false;

            this.form.Dispose();

            this.Dispose();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Comment comment = new Comment(this , this.op, problem[k].answer, uid, problem[k].id);

            comment.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            autoOpenComment = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            autoNextProblem = checkBox2.Checked;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(type == 0 || type == 2)
            {
                Feedback feedback = new Feedback(uid, problem[k].id);

                feedback.ShowDialog();

                return;
            }
            
            
            end = false;
            int a = 0 , target = -1;
            for(int i = 0; i < sum; i++)
            {
                if (problem[i].selectIndex != 0)
                {
                    a++;    
                }
                else
                {
                    if (target == -1)
                        target = i;
                }
            }

            if (a < sum && time != 0)
            {
                DialogResult result =
                       MessageBox.Show("你还有" + Convert.ToInt32(sum - a) + "题未完成" +
                       " ，是否继续完成？", "模拟提示", MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if(target != -1)
                        k = target;
                    isExit = false;
                    refresh();
                    return;
                }
            }


            if (time != 0)
            {
                int minute = time / 60;
                int second = time % 60;

                DialogResult result =
                       MessageBox.Show("你还有" + minute + "分钟" + second + "秒" +
                       " ，是否继续完成？", "模拟提示", MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if(a < sum && target != -1)
                    {
                        k = target;
                        isExit = false;
                        refresh();
                    }

                    return;
                }
            }

            StringBuilder allSql = new StringBuilder();
            allSql.AppendFormat("select count(*) from solve where user_id = '{0}' and  problem_id = {1} "
                , this.uid, problem[0].id);

            for(int i = 1; i < sum; i++)
            {
                allSql.AppendFormat(" union select count(*) from solve where user_id = '{0}' and  problem_id = {1} "
               , this.uid, problem[i].id);
            }
            MySqlDataReader reader = MySqlHelper.selectDataForRead(allSql);


            int count = 0;
            StringBuilder sql = new StringBuilder();
            for (int i = 0; i < sum; i++)
            {
                reader.Read();
                if(problem[i].selectIndex != 0)
                {
                    int x = reader.GetInt32(0);

                    if (problem[i].selectIndex == problem[i].rightIndex)
                    {
                        count++; 
                        if (x == 0)
                        {
                            sql.AppendFormat("insert into solve values(null , '{0}' , {1} , 0);", this.uid, problem[i].id);
                        }
                        else
                        {
                            x++;
                            x = Math.Max(x, 0);
                            sql.AppendFormat("update solve set state = {0} where user_id = '{1}' and  problem_id = {2};"
                                ,x , this.uid, problem[i].id);

                            //MySqlHelper.insertOrDeleteOrupdate(sql);         
                        }
                    }
                    else
                    {
                        if (x == 0)
                        {
                            sql.AppendFormat("insert into solve values(null , '{0}' , {1} , -2);", this.uid, problem[i].id);
                        }
                        else
                        {
                            sql.AppendFormat("update solve set state = -2 where user_id = '{0}' and  problem_id = {1};"
                                , this.uid, problem[i].id);
                        }
                    }
                }
            }

            MySqlHelper.insertOrDeleteOrupdate(sql);

            end = true;

            timer1.Enabled = false;

            int cost = temp_time - time;

            int minute1 = cost / 60;

            int second1 = cost % 60;

            string cost_string = minute1 + "分" + second1 + "秒";

            int x2 = 0;
            if(num == 300 && temp_time == 90 * 60 && time >= 30 * 60)
            {
                x2 = time * count / 4 / 300 / 60;
            }

            count += x2;
            MessageBox.Show("恭喜！你获得" + count + "分！(其中奖励分:" + x2 + ")\n" + "用时：" + cost_string , "模拟提示");

            sql.Clear();
            //sql.AppendFormat("insert into simulatelog values(null , {0} , {1} , {2} , '{3}' ," +
            //    "'{4}')", uid, count , num , cost_string , date.ToLocalTime().ToString());

            sql.AppendFormat("insert into simulatelog values(null , {0} , {1} , {2} , '{3}' ," +
            " now())", uid, count, num, cost_string);

            textBox1.Text = sql.ToString();

            MySqlHelper.insertOrDeleteOrupdate(sql);

            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == "")
            {
                MessageBox.Show("请输入需要跳转的题号！", "刷题提示");
                return;
            }

            int num = 0;
            try
            {
                num = Convert.ToInt32(textBox2.Text);
            }catch
            {                
                textBox2.Text = "";
                MessageBox.Show("请输入合法的题号！", "刷题提示");               
                return;
            }

            if(num <= 0 || num > sum)
            {
                textBox2.Text = "";
                MessageBox.Show("请输入大于0小于" + sum +  "的数字！", "刷题提示");
                
            }              
            else
            {
                k = num - 1;
                refresh();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(time != 0)
            {
                int minute = time / 60;
                int second = time % 60;

                label3.Text = "剩余：" + minute.ToString().PadLeft(2, '0') + ":"
                    + second.ToString().PadLeft(2, '0');

                time--;
            }
            else
            {
                if(type == 2 && timer1.Enabled)
                {
                    MessageBox.Show("时间结束，自动交卷！", "模拟提示");
                    button4_Click(sender, e);
                    timer1.Enabled = false;
                }           
            }

            //MessageBox.Show("123");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.A: radioButton1_Click(null, null);break;
                case Keys.B: radioButton2_Click(null, null); break;
                case Keys.C: radioButton3_Click(null, null); break;
                case Keys.D: radioButton4_Click(null, null); break;
                case Keys.Right: button2_Click(null, null); break;
                case Keys.Left: button1_Click(null, null); break;
                case Keys.Down:
                    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -100, 0);
                    break;
                case Keys.Up:
                    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, 100, 0);
                    break;
                case Keys.Escape: this.Dispose(); break;
            }


            return true;
            //return base.ProcessCmdKey(ref msg, keyData);
        }

        [DllImport("user32.dll")]
        static extern void mouse_event(int flags, int dX, int dY, int buttons, int extraInfo);

        const int MOUSEEVENTF_MOVE = 0x1;
        const int MOUSEEVENTF_LEFTDOWN = 0x2;
        const int MOUSEEVENTF_LEFTUP = 0x4;
        const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        const int MOUSEEVENTF_RIGHTUP = 0x10;
        const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        const int MOUSEEVENTF_MIDDLEUP = 0x40;
        const int MOUSEEVENTF_WHEEL = 0x800;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;

    }

    class Problem
    {
        public int id;
        
        public string content;

        public List<string> ops = new List<string>();

        public string answer;

        public int selectIndex;

        public int rightIndex;

        public string getString(string s)
        {
            string x = "&amp;";

            while(s.IndexOf(x) != -1)
            {
                //MessageBox.Show()
                int index = s.IndexOf(x);
                s.Remove(index, 5);
                s.Insert(index, "&");

            }

            return s;
        }

        public Problem()
        {

        }

        public Problem(int id , string content , string opA , string opB , string opC , string opD , string answer)
        {
            content = content.Replace("&amp;", "zzq");
            
            content = content.Replace("zzq", "&");
            

            //content = getString(content);
            //opA = getString(opA);
            //opB = getString(opB);
            //opC = getString(opC);
            //opD = getString(opD);


            content = content.Trim();
            opA = opA.Trim();
            opB = opB.Trim();
            opC = opC.Trim();
            opD = opD.Trim();

            this.id = id;
            this.content = content;
            this.selectIndex = 0;


            if (answer == "A")
                this.answer = opA;
            else if (answer == "B")
                this.answer = opB;
            else if (answer == "C")
                this.answer = opC;
            else
                this.answer = opD;


            ops.Add(opA);
            
            ops.Add(opB);
            
            ops.Add(opC);
            
            ops.Add(opD);

            var random = new Random();
            var newList = new List<string>();
            foreach (var item in ops)
            {
                newList.Insert(random.Next(0,newList.Count), item);
            }

            ops = newList;

            

            if (ops[0] == this.answer)
                rightIndex = 1;
            else if (ops[1] == this.answer)
                rightIndex = 2;
            else if (ops[2] == this.answer)
                rightIndex = 3;
            else
                rightIndex = 4;

            

            for (int i = 0; i < 4; i++)
            {
                ops[i] = ops[i].Replace("&amp;", "zzq520");
                ops[i] = ops[i].Replace("zzq520", "&&");
            }

            this.answer = ops[rightIndex - 1];
        }
    }
}
