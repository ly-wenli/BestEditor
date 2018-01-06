using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandleImpl;
using Model;
using System.IO;


namespace Dao
{
    public class HandleDao : Handle
    {
        public void save(String classity_content,String file_name,String content)
        {
            string pathout = "C:\\BestEditor\\js" + classity_content + "js\\sj" + file_name + ".txt";
            StreamWriter sw = new StreamWriter(pathout, true);
            sw.WriteLine(content);
            sw.Close();
            sw.Dispose();      
        }
        public void delete() { 
        
        }
        public void update(TextContent text)
        {
            StreamWriter sw = new StreamWriter(text.Path, false);
            sw.WriteLine(text.Content);
            sw.Close();
            sw.Dispose();
        }
        public void InitFile()
        { 

        }
        public List<String> HandleClassify(String path)
        { 
            HandleFile handFile = new HandleFile();
            return handFile.getClassify(path);
        }
        public void SaveFileJudge(String classity_content)
        {
            string path = "C:\\BestEditor\\js" + classity_content + "js\\";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);//不存在就创建目录 
            }
        }


       public List<String> GetFile(String path) {
           OpenFile openFile = new OpenFile();
           return openFile.getFile(path);
        }


       public TextContent GetFileALL(string classify, string fileName)
       {
           OpenFile openFile = new OpenFile();
           return openFile.GetFileALLInformation(classify,fileName);
       }
    }
}
