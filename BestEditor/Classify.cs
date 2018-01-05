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
using System.Text;
using System.Text.RegularExpressions;

namespace BestEditor
{
    public partial class Classify : Form
    {
        List<string> list_path = new List<string>();//定义list变量，存放获取到的路径
        List<string> list_classity_path = null;//定义list变量，存放获取到的路径
        String content = null;//保存的文件内容
        String classity_content = null; //获取前台选择的分类内容


        public Classify(String content )
        {
            InitializeComponent();
            this.content = content;
            //先判断文件是否存在
            //初始化类别
            getClassify("C:\\BestEditor");
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


        /**
         * 通过getPath()寻找文件夹下的path
         * **/
        public void getPath(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles();
            DirectoryInfo[] dii = dir.GetDirectories();
            foreach (FileInfo f in fil)
            {
                list_path.Add(f.FullName);//添加文件的路径到列表
            }
            //获取子文件夹内的文件列表，递归遍历
            foreach (DirectoryInfo d in dii)
            {
                getPath(d.FullName);
                list_path.Add(d.FullName);//添加文件夹的路径到列表
            }
        }
        /**
         *获取类别的路径
         * **/
        public  void getClssityPath(string path)
        {
            list_classity_path = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] dii = dir.GetDirectories();
            //获取子文件夹内的文件列表，递归遍历
            foreach (DirectoryInfo d in dii)
            {
                list_classity_path.Add(d.FullName);//添加文件夹的路径到列表
            }
        }
        /**
         * 获取文件分类
         * **/
        public void getClassify(string path)
        {
            /**
             * 首先执行一遍寻找文件路径
             * **/
            getClssityPath(path);
            foreach (String d in list_classity_path)
            {
                string[] sArray = Regex.Split(d, "js", RegexOptions.IgnoreCase);
                comboBox1.Items.Add(sArray[1]);
            }
            comboBox1.SelectedIndex = 0;//设置comboBox1的首选项
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
