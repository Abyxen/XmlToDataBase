using System;
using System.Collections.Generic;
using System.Text;

namespace XmlToDataBase
{
    public class XmlTableRecord
    {
        public Dictionary<string, string> Fields { get; set; }
        public XmlTableRecord()
        {
            this.Fields = new Dictionary<string, string>();
        }
    }
}
