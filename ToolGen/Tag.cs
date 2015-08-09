using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGenCode
{
    class Attribute
    {
        string Name;
        string Value;
    }
    class Tag
    {
        public string StartTag { set; get; }
        public string EndTag { set; get; }
        public List<Attribute> aListAttribute = new List<Attribute>();
        public List<string> aListKeyContent = new List<string>();

    }

    class ForTag : Tag
    {
        public int LoopFrom = 0;
        public int LoopTo = 0;

        public ForTag()
        {

        }
    }
}
