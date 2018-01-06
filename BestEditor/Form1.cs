using Dao;
using HandleImpl;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BestEditor
{
    public partial class Main : Form
    {   
        public static Main form1;
        private bool isTextChanged;
        public static string path;//记录文件路径（刚新建的文件路径为""，打开的文件路径为原路径）
        public static TextContent textContent = null;

        Handle handleImpl = new HandleDao();
        public Main()
        {
            InitializeComponent();
            this.Text = "记事本";
            handleImpl.SaveFileJudge("默认存储");
            path = "";//初始化path
            form1 = this;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //初始化，撤销、剪切、复制、删除 不可用
            撤消UToolStripMenuItem.Enabled = false;
            剪切TToolStripMenuItem.Enabled = false;
            复制CToolStripMenuItem.Enabled = false;
            删除LToolStripMenuItem.Enabled = false;

            if (richTextBoxBoard.Equals(""))
            {
                查找FToolStripMenuItem.Enabled = false;
            }
            else
            {
                查找FToolStripMenuItem.Enabled = true;
            }

            if (Clipboard.ContainsText())
                粘贴PToolStripMenuItem.Enabled = true;
            else
                粘贴PToolStripMenuItem.Enabled = false;

            toolStripStatusLabel2.Text = "第 1 行，第 1 列";
        }

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果输入框文字发生变动
            if (isTextChanged)
            {
                saveFileDialog1.FileName = "*.txt";
                //用于将文字传递到前台
                DialogResult dr = MessageBox.Show("是否将更改保存到 " + this.Text + "?", "记事本", 
                    MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    string content = richTextBoxBoard.Text;
                    Classify classify = new Classify(content);
                    classify.Owner = this;
                    classify.Show();
                }
                else if(dr == DialogResult.No)
                {
                    richTextBoxBoard.Text = "";
                    path = "";
                }
            }
            else
            {
                richTextBoxBoard.Text = "";
                this.Text = " 记事本";
                path = "";
            }
        }

        private void richTextBoxBoard_TextChanged(object sender, EventArgs e)
        {
            isTextChanged = true;
        }
        /**
         * 打开时先判断
         * 若文本框已经编辑先进行保存选择
         * 保存时判断文件是否为源文件
         * 若没有再进行打开
         * **/
        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isTextChanged)
            {

                if ("".Equals(path))
                {
                    /**
                       * 这是个不存在的文件
                        * **/
                    DialogResult dr = MessageBox.Show("是否将更改保存???", "记事本", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        String a = richTextBoxBoard.Text;
                        Classify classify = new Classify(a);
                        classify.Owner = this;
                        classify.ShowDialog();
                    }
                    else if (dr == DialogResult.No)
                    {/**
                          * 正常启动打开界面
                          * **/
                        Main.form1.richTextBoxBoard.Text = "";
                        OpenFile openFile = new OpenFile();
                        openFile.Owner = this;
                        openFile.Show();
                    }
                }
                else
                {
                    string content = richTextBoxBoard.Text;
                    /**
                     * 这是个已经存在的文件
                     * **/
                    textContent.Content = content;                  
                    handleImpl.update(textContent);
                    Main.form1.richTextBoxBoard.Text = "";
                    OpenFile openFile = new OpenFile();
                    openFile.Owner = this;
                    openFile.Show();
                }
                          
             }else
             {
                    /**
                     * 没有更改编辑区，正常启动打开界面
                     * **/
                    Main.form1.richTextBoxBoard.Text = "";
                    OpenFile openFile = new OpenFile();
                    openFile.Owner = this;
                    openFile.Show();
            }           
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String a = richTextBoxBoard.Text;
            Classify classify = new Classify(a);
            classify.Owner = this;
            classify.Show();

        }

        private void 页面设置UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.Document = printDocument1;
            pageSetupDialog1.ShowDialog();
        }

        private void 打印PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            printDialog1.ShowDialog();
        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 撤消UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxBoard.Undo();
        }

        private void 编辑EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxBoard.CanUndo)
                撤消UToolStripMenuItem.Enabled = true;

            if (richTextBoxBoard.SelectionLength > 0)
            {
                剪切TToolStripMenuItem.Enabled = true;
                复制CToolStripMenuItem.Enabled = true;
                删除LToolStripMenuItem.Enabled = true;
            }
            else
            {
                剪切TToolStripMenuItem.Enabled = false;
                复制CToolStripMenuItem.Enabled = false;
                删除LToolStripMenuItem.Enabled = false;
            }

            if (richTextBoxBoard.Equals(""))
            {
                查找FToolStripMenuItem.Enabled = false;
            }
            else
            {
                查找FToolStripMenuItem.Enabled = true;
            }

            if (Clipboard.ContainsText())
                粘贴PToolStripMenuItem.Enabled = true;
            else
                粘贴PToolStripMenuItem.Enabled = false;
        }

        private void 剪切TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxBoard.Cut();
        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxBoard.Copy();
        }

        private void 粘贴PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxBoard.Paste();
        }

        private void 删除LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxBoard.SelectedText = "";
        }

        /// <summary>
        /// 不同窗体之间通讯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 查找FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search search = new Search();
            search.Owner = this;
            search.Show();
        }

        private void 查找下一个NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search search = new Search();
            search.Owner = this;
            search.Show();
        }

        private void 替换RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change change = new Change();
            change.Owner = this;
            change.Show();
        }

        private void 转到GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Goto gt = new Goto();
            gt.Owner = this;
            gt.Show();
        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxBoard.SelectAll();
        }

        private void 时间日期DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string front = richTextBoxBoard.Text.Substring(0, richTextBoxBoard.SelectionStart);
            string back = richTextBoxBoard.Text.Substring(richTextBoxBoard.SelectionStart, 
            richTextBoxBoard.Text.Length - richTextBoxBoard.SelectionStart);
            richTextBoxBoard.Text = front + DateTime.Now.ToString() + back;
        }

        private void 自动换行WToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBoxBoard.WordWrap)
            {
                自动换行WToolStripMenuItem.Checked = false;
                richTextBoxBoard.WordWrap = false;
            }
            else
            {
                自动换行WToolStripMenuItem.Checked = true;
                richTextBoxBoard.WordWrap = true;
            }
        }

        private void 字体FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBoxBoard.SelectionFont = fontDialog1.Font;
        }


        private void 状态栏SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (状态栏SToolStripMenuItem.Checked)
            {
                状态栏SToolStripMenuItem.Checked = false;
                statusStrip1.Visible = false;
            }
            else
            {
                状态栏SToolStripMenuItem.Checked = true;
                statusStrip1.Visible = true;
            }
        }

        private void richTextBoxBoard_SelectionChanged(object sender, EventArgs e)
        {
            string[] str = richTextBoxBoard.Text.Split('\r', '\n');
            int row = 1, column = 1, pos = richTextBoxBoard.SelectionStart;

            foreach(string s in str)
                Console.WriteLine(s);
            Console.WriteLine("pos={0}",pos);

            for (int i = 0; i < str.Length && pos - str[i].Length > 0; i++)
            {
                pos = pos - str[i].Length - 1;
                row = i + 2;
            }
            column = pos + 1;
            toolStripStatusLabel2.Text = "第 " + row + " 行，第 " + column + " 列";
        }

        private void 关于记事本AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.Show();
        }

        private void 查看帮助HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //调用系统内部的notepad.chm文件
        }

        /**
         * 选择关闭主视窗口时进行的操作
         * **/
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isTextChanged)
            {

                if (!("".Equals(path)))
                {
                    /**
                     * 这是个已经存在的文件
                     * **/
                    DialogResult dr = MessageBox.Show("是否将更改保存?","记事本", MessageBoxButtons.YesNoCancel);
                    if (dr == DialogResult.Yes)
                        richTextBoxBoard.SaveFile(path, RichTextBoxStreamType.PlainText);
                    else if (dr == DialogResult.No)
                        e.Cancel = false;
                    else
                        e.Cancel = true;//不关闭
                }
                else
                {
                    /**
                     * 这是个不存在的文件
                     * **/
                    DialogResult dr = MessageBox.Show("是否将更改保存???", "记事本", MessageBoxButtons.YesNoCancel);
                    if (dr == DialogResult.Yes)
                    {
                        String a = richTextBoxBoard.Text;
                        Classify classify = new Classify(a);
                        classify.Owner = this;
                        classify.ShowDialog();
                    }
                    else if (dr == DialogResult.No)
                        e.Cancel = false;
                    else
                        e.Cancel = true;    
                   
                }
            }
        }
    }
}