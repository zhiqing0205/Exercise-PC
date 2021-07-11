namespace exercise
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.普通练习ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模拟练习ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.错题练习ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷题排行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.随机刷题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.模拟练习ToolStripMenuItem,
            this.随机刷题ToolStripMenuItem,
            this.错题练习ToolStripMenuItem,
            this.普通练习ToolStripMenuItem,
            this.刷题排行ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(11, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(554, 38);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 普通练习ToolStripMenuItem
            // 
            this.普通练习ToolStripMenuItem.Name = "普通练习ToolStripMenuItem";
            this.普通练习ToolStripMenuItem.Size = new System.Drawing.Size(100, 30);
            this.普通练习ToolStripMenuItem.Text = "分类练习";
            this.普通练习ToolStripMenuItem.Click += new System.EventHandler(this.普通练习ToolStripMenuItem_Click);
            // 
            // 模拟练习ToolStripMenuItem
            // 
            this.模拟练习ToolStripMenuItem.Name = "模拟练习ToolStripMenuItem";
            this.模拟练习ToolStripMenuItem.Size = new System.Drawing.Size(100, 30);
            this.模拟练习ToolStripMenuItem.Text = "全真模拟";
            this.模拟练习ToolStripMenuItem.Click += new System.EventHandler(this.模拟练习ToolStripMenuItem_Click);
            // 
            // 错题练习ToolStripMenuItem
            // 
            this.错题练习ToolStripMenuItem.Name = "错题练习ToolStripMenuItem";
            this.错题练习ToolStripMenuItem.Size = new System.Drawing.Size(100, 30);
            this.错题练习ToolStripMenuItem.Text = "错题强化";
            this.错题练习ToolStripMenuItem.Click += new System.EventHandler(this.错题练习ToolStripMenuItem_Click);
            // 
            // 刷题排行ToolStripMenuItem
            // 
            this.刷题排行ToolStripMenuItem.Name = "刷题排行ToolStripMenuItem";
            this.刷题排行ToolStripMenuItem.Size = new System.Drawing.Size(100, 30);
            this.刷题排行ToolStripMenuItem.Text = "刷题排行";
            this.刷题排行ToolStripMenuItem.Click += new System.EventHandler(this.刷题排行ToolStripMenuItem_Click);
            // 
            // 随机刷题ToolStripMenuItem
            // 
            this.随机刷题ToolStripMenuItem.Name = "随机刷题ToolStripMenuItem";
            this.随机刷题ToolStripMenuItem.Size = new System.Drawing.Size(100, 30);
            this.随机刷题ToolStripMenuItem.Text = "随机刷题";
            this.随机刷题ToolStripMenuItem.Click += new System.EventHandler(this.随机刷题ToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 496);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "主界面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 普通练习ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模拟练习ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 错题练习ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷题排行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 随机刷题ToolStripMenuItem;
    }
}