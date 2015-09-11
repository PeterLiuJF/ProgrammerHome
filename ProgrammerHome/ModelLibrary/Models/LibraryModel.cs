using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelLibrary
{
    public class LibraryModel
    {
    }

    public class LibraryTypeModel
    {
        public int ID{ get; set; }
        public int Level{ get; set; }
        public int ParentLevel{ get; set; }
        public string Name{ get; set; }
    }
}