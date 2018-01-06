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
namespace BestEditor
{
    public partial class OpenFile : Form
    {
        List<string> list_path = new List<string>();//定义list变量，存放获取到的路径
        List<string> list_classity_path = new List<string>();//定义list变量，存放获取到的路径
        List<string> list_file = new List<string>();//定义list变量，存放获取到的日记txt文件
        Boolean judge = true;
        public OpenFile()
        {
            InitializeComponent();
            getClassify("C:\\BestEditor");
        }

        private void OpenFile_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string content = comboBox1.Text;
            comboBox2.Items.Clear();
            judge = true;
            getFile("C:\\BestEditor\\js" + content + "js");
       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



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
            int index = comboBox1.SelectedIndex;
            string index_path = comboBox1.Items[0].ToString();
            getFile("C:\\BestEditor\\js" + index_path+"js");
        }
        /**
           *获取类别的路径
           * **/
        public void getClssityPath(string path)
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
         * 获取子文件夹,并将文件名加r到ComboBox中
         * **/
        public void getFile(string path) {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles();
            if (fil.Length != 0)
            {
                foreach (FileInfo f in fil)
                {
                    list_file.Add(f.FullName);//添加文件的路径到列表 
                    comboBox2.Items.Add(Path.GetFileNameWithoutExtension(f.FullName));
                }
                comboBox2.SelectedIndex = 0;
            }
            else
            {
                if (judge)
                {
                    comboBox2.Items.Add("无文件");
                    comboBox2.SelectedIndex = 0;
                    judge = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            string classify= comboBox1.Items[index].ToString();
            int index2 = comboBox2.SelectedIndex;
            string fileName = comboBox2.Items[index2].ToString();
            string path = "C:\\BestEditor\\js" + classify + "js\\" + fileName+".txt";
            string content = File.ReadAllText(@"C:\\BestEditor\\js" + classify + "js\\" + fileName+".txt");
            Main.form1.richTextBoxBoard.Text = content;
            Main.form1.Text = fileName;
            Main.path = path;
            this.Close();
        }
    }
}
