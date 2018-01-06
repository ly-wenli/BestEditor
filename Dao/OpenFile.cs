using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Model;

namespace Dao
{
    class OpenFile
    {
        List<string> list_path = new List<string>();//定义list变量，存放获取到的路径
        List<string> list_classity_path = new List<string>();//定义list变量，存放获取到的路径
        List<string> list_file = new List<string>();//定义list变量，存放获取到的日记txt文件
        Boolean judge = true;

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
         //* **/
        public List<String> getFile(string path)
        {
            List<String> fileName = new List<String>();
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles();
            if (fil.Length != 0)
            {
                foreach (FileInfo f in fil)
                {
                    list_file.Add(f.FullName);//添加文件的路径到列表 
                    String files = f.FullName;
                    string[] sArray = Regex.Split(files, "sj", RegexOptions.IgnoreCase);
                    string[] sarray = Regex.Split(sArray[1], "\\.", RegexOptions.IgnoreCase);
                    fileName.Add(sarray[0]);
                }
            }
            else
            {
                if (judge)
                {
                    fileName.Add("无文件");
                  //  judge = false;
                }
            }
            return fileName;
        }

        public TextContent GetFileALLInformation(string classify, string fileName)
        {
            TextContent text = new TextContent();
            string path = "C:\\BestEditor\\js" + classify + "js\\sj" + fileName + ".txt";
            string content = File.ReadAllText(@"C:\\BestEditor\\js" + classify + "js\\sj" + fileName + ".txt");
            text.Classify = classify;
            text.Content = content;
            text.Path = path;
            text.Writer = Environment.UserName;
            return text;
        }

    }
}
