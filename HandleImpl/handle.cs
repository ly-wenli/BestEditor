using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleImpl
{
    public interface Handle
    {
        void save();
        void delete();
        void update();
        void InitFile();
        List<String> HandleClassify(String path);
    }
}
