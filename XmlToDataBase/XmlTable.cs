using System;
using System.Collections.Generic;
using System.Text;

namespace XmlToDataBase
{
    public class XmlTable
    {
        public string TableName { get; set; }
        public List<XmlTableRecord> Records { get; set; }

        public XmlTable()
        {
            this.Records = new List<XmlTableRecord>();
        }
    }
}
