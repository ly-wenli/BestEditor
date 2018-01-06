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
using System.Text.RegularExpressions;
using Model;
using Dao;
using HandleImpl;
namespace BestEditor
{
    public partial class Classify : Form
    {
        List<string> list_path = new List<string>();//定义list变量，存放获取到的路径
   //     List<string> list_classity_path = null;//定义list变量，存放获取到的路径
        String content = null;//保存的文件内容
        String classity_content = null; //获取前台选择的分类内容

        HandleDao handleImpl = new HandleDao();
        List<String> classify = new List<String>();
        public Classify(String content )
        {
            InitializeComponent();
            this.content = content;
            //先判断文件是否存在
            //初始化类别
          classify = handleImpl.HandleClassify("C:\\BestEditor");
          ClassifyToView();
        }

        private void ClassifyToView() {
            if (classify.Count != 0)
            {
                foreach (String content in classify)
                {
                    comboBox1.Items.Add(content);           
                }
                comboBox1.SelectedIndex = 0;//设置comboBox1的首选项
            }
        }

        private void classify_Load(object sender, EventArgs e)
        {
           // Main main = (Main)this.Owner;
           
        }

        //取消按钮
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //添加分类按钮
        private void button3_Click(object sender, EventArgs e)
        {
            //点击按钮之后改变comboBox的属性
            comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
        }
        //保存按钮
        private void button1_Click(object sender, EventArgs e)
        {
            classity_content = comboBox1.Text;
            String file_name = textBox1.Text; // 保存的文件名
            /**
             * 判断文件是否存在
             * **/
            sevaFileJudge();
            string pathout = "C:\\BestEditor\\js" + classity_content + "js\\sj" + file_name + ".txt";
            StreamWriter sw = new StreamWriter(pathout, true);
            sw.WriteLine(content);
            sw.Close();
            sw.Dispose();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

    
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
           
        }

        /**
         * 文件BestEditor的初始化
         * **/
        private void sevaFileJudge()
        {
            string path = "C:\\BestEditor\\js"+classity_content+"js\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);//不存在就创建目录 
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
