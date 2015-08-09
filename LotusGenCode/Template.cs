using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;

namespace LotusGenCode
{
    class Template
    {
        public Table aTable ;
        public string TemplateBeforeProcess { set; get; }


        public Template(string TemplateBeforeProcess)
        {
            this.TemplateBeforeProcess = TemplateBeforeProcess;

        }
        public Template(string TemplateBeforeProcess , Table aTable)
        {
            this.TemplateBeforeProcess = TemplateBeforeProcess;
            this.aTable = aTable;
        }

    }
}
