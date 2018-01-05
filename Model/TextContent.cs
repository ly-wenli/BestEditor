using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class TextContent
    {
        private String content; // 定义文本内容

        public String Content
        {
            get { return content; }
            set { content = value; }
        }
        private String writer;//定义文本作者

        public String Writer
        {
            get { return writer; }
            set { writer = value; }
        }
        private String classify;//定义文本类别

        public String Classify
        {
            get { return classify; }
            set { classify = value; }
        }

    }
}
