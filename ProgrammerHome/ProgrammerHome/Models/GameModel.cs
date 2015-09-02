using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerHome
{
    public class GameModel
    {
        public int ID;
        public string Name;
        public string FilePath;
        public string FileSize;
        public string ImageFilePath;
        public string Detail;
    }

    public class GameTypeModel
    {
        public int ID;
        public string MainType;
        public string TypeName;
        public List<GameModel> Games = new List<GameModel>();
    }
}