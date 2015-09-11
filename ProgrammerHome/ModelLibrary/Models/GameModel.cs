using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelLibrary
{
    public class GameModel
    {
        public int ID{ get; set; }
        public string Name{ get; set; }
        public string FilePath{ get; set; }
        public string FileSize{ get; set; }
        public string ImageFilePath{ get; set; }
        public string Detail{ get; set; }
    }

    public class GameTypeModel
    {
        public int ID{ get; set; }
        public string MainType{ get; set; }
        public string TypeName{ get; set; }
        public List<GameModel> Games = new List<GameModel>();
    }
}