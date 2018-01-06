using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dao
{
    class HandleFile
    {
        List<string> list_path = new List<string>();//定义list变量，存放获取到的路径
        List<string> list_classity_path = null;//定义list变量，存放获取到的路径
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
         * 获取文件分类
         * **/
        public  List<String> getClassify(string path)
        {
            List<String> classify = new List<String>();
            /**
             * 首先执行一遍寻找文件路径
             * **/
            getClssityPath(path);
            if (list_classity_path.Count != 0)
            {
                foreach (String d in list_classity_path)
                {
                    string[] sArray = Regex.Split(d, "js", RegexOptions.IgnoreCase);
                    classify.Add(sArray[1]);
                }
            }         
            return classify;
        }
    }
}
