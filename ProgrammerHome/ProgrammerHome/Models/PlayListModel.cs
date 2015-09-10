using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgrammerHome
{
    public class PlayListModel
    {
        public List<PlayDetailModel> MusicList = new List<PlayDetailModel>();
        public List<PlayDetailModel> MovieList = new List<PlayDetailModel>();
    }
    public class PlayDetailModel
    {
        public int ID;
        public string title;
        public string artist;
        public string mp3;
        public string oga;
        public string poster;
    }
}