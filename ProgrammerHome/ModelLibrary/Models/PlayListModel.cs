using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModelLibrary
{
    public class PlayListModel
    {
        public List<PlayDetailModel> MusicList = new List<PlayDetailModel>();
        public List<PlayDetailModel> MovieList = new List<PlayDetailModel>();
    }
    public class PlayDetailModel
    {
        public int ID{ get; set; }
        public string title{ get; set; }
        public string artist{ get; set; }
        public string mp3{ get; set; }
        public string oga{ get; set; }
        public string poster{ get; set; }
    }
}