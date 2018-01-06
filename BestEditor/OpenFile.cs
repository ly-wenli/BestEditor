using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Web;
using System.Windows;
using System.Text.RegularExpressions;
using Model;
using Dao;
using HandleImpl;

namespace BestEditor
{
    public partial class OpenFile : Form
    {
      //  Boolean judge = true;

        HandleDao handleImpl = new HandleDao();
        List<String> classify = new List<String>();
        List<String> classify_File = new List<String>();
        public OpenFile()
        {
            InitializeComponent();
            classify = handleImpl.HandleClassify("C:\\BestEditor");
            ClassifyToView();
        }

        private void OpenFile_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string content = comboBox1.Text;
            comboBox2.Items.Clear();
           // judge = true;
            String path = "C:\\BestEditor\\js" + content + "js";
            classify_File = handleImpl.GetFile(path);
            Classify_FileToView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            string classify = comboBox1.Items[index].ToString();
            int index2 = comboBox2.SelectedIndex;
            string fileName = comboBox2.Items[index2].ToString();
            TextContent text = handleImpl.GetFileALL(classify, fileName);
            Main.form1.richTextBoxBoard.Text = text.Content;
            Main.form1.Text = fileName;
            Main.path = text.Path;
            this.Close();
        }

        private void ClassifyToView()
        {
            if (classify.Count != 0)
            {
                foreach (String content in classify)
                {
                    comboBox1.Items.Add(content);
                }
                comboBox1.SelectedIndex = 0;//设置comboBox1的首选项
            }
        }
        private void Classify_FileToView()
        {
            if (classify.Count != 0)
            {
                foreach (String content in classify_File)
                {
                    comboBox2.Items.Add(content);
                }
                comboBox2.SelectedIndex = 0;//设置comboBox1的首选项
            }
        }
    }
}
