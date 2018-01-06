using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandleImpl;
using Model;


namespace Dao
{
    public class HandleDao : Handle
    {
        public void save(){
        
        }
        public void delete() { 
        
        }
        public void update() { 
        
        }
        public void InitFile()
        { 

        }
        public List<String> HandleClassify(String path)
        { 
            HandleFile handFile = new HandleFile();
            return handFile.getClassify(path);
        }
    }
}
