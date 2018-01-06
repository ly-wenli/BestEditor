using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleImpl
{
    public interface Handle
    {
        void save(String classity_content, String file_name, String content);
        void delete();
        void update();
        void InitFile();
        List<String> HandleClassify(String path);
        void SaveFileJudge(String classity_content);
    }
}
