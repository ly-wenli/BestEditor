using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace HandleImpl
{
    public interface Handle
    {
        void save(String classity_content, String file_name, String content);
        void delete();
        void update(TextContent text);
        void InitFile();
        List<String> HandleClassify(String path);
        void SaveFileJudge(String classity_content);
        List<String> GetFile(String path);
        TextContent GetFileALL(string classify, string fileName);//获取文件路径

    }
}
