using System;
using System.Collections.Generic;
using System.Text;

namespace XmlToDataBase
{
    public class XmlBackup
    {
        public int AssociationID { get; set; }

        public List<XmlTable> Tables { get; set; }


        public XmlBackup()
        {
            this.Tables = new List<XmlTable>();
        }
    }
}

