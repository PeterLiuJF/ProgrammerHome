using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ToolLib.DbAccess;
using ToolLib.util;

namespace ProgrammerHome.DataBaseService
{
    public class RecreationService
    {
        public List<PlayDetailModel> GetMusicItems(int type)
        {
            string sql = string.Format("select * from PlayList where type={0}",type);
            List<PlayDetailModel> list = new List<PlayDetailModel>();
            DataTable dt = new DataTable();
            SqlAccess.QueryDt(dt, sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new PlayDetailModel()
                {
                    title = Normal.ListStr(dt.Rows[i]["Title"]),
                    artist = Normal.ListStr(dt.Rows[i]["Artist"]),
                    mp3 = Normal.ListStr(dt.Rows[i]["FileInfo"]),
                    poster = Normal.ListStr(dt.Rows[i]["ImageINfo"])
                });
            }
            return list;
        }

        public PlayListModel GetPlayListItems()
        {
            string sql = "select * from PlayList";
            PlayListModel model = new PlayListModel();
            DataTable dt = new DataTable();
            SqlAccess.QueryDt(dt, sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var type = Normal.ParseInt(dt.Rows[i]["Type"]);
                if (type == 2)
                {
                    model.MovieList.Add(new PlayDetailModel()
                    {
                        title = Normal.ListStr(dt.Rows[i]["Title"]),
                        artist = Normal.ListStr(dt.Rows[i]["Artist"]),
                        mp3 = Normal.ListStr(dt.Rows[i]["FileInfo"]),
                        poster = Normal.ListStr(dt.Rows[i]["ImageINfo"]),
                        ID = Normal.ParseInt(dt.Rows[i]["ID"]),

                    });
                }
                else
                {
                    model.MusicList.Add(new PlayDetailModel()
                    {
                        title = Normal.ListStr(dt.Rows[i]["Title"]),
                        artist = Normal.ListStr(dt.Rows[i]["Artist"]),
                        mp3 = Normal.ListStr(dt.Rows[i]["FileInfo"]),
                        poster = Normal.ListStr(dt.Rows[i]["ImageINfo"]),
                        ID = Normal.ParseInt(dt.Rows[i]["ID"]),

                    });
                }
            }
            return model;
        }

        public List<GameModel> GetGameItems()
        {
            string sql = "select * from Game";
            List<GameModel> list = new List<GameModel>();
            DataTable dt = new DataTable();
            SqlAccess.QueryDt(dt, sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list.Add(new GameModel()
                {
                    ID = Normal.ParseInt(dt.Rows[i]["ID"]),
                    Name = Normal.ListStr(dt.Rows[i]["Name"])
                });
            }
            return list;
        }

        public List<GameTypeModel> GetGameTypeItems()
        {

            string sql = @"select r.*,GameID=g.id,g.name,g.filepath,g.filesize,g.ImageFilePath,g.Detail 
from RecreationType r left join Game g on g.typeid=r.id where maintype='单机游戏'";
            List<GameTypeModel> list = new List<GameTypeModel>();
            DataTable dt = new DataTable();
            SqlAccess.QueryDt(dt, sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GameTypeModel typeModel = null;
                var typeName = Normal.ListStr(dt.Rows[i]["TypeName"]);
                var mainType = Normal.ListStr(dt.Rows[i]["MainType"]);
                var gameid = Normal.ParseInt(dt.Rows[i]["GameID"]);
                typeModel = list.SingleOrDefault(t => t.TypeName == typeName && t.MainType == mainType);
                if (typeModel == null)
                {
                    typeModel = new GameTypeModel()
                    {
                        ID = Normal.ParseInt(dt.Rows[i]["ID"]),
                        MainType = mainType,
                        TypeName = typeName
                    };

                    if (gameid != 0)
                        typeModel.Games.Add(new GameModel()
                        {
                            ID = gameid,
                            Name = Normal.ListStr(dt.Rows[i]["Name"]),
                            FilePath = Normal.ListStr(dt.Rows[i]["FilePath"]),
                            FileSize = Normal.ListStr(dt.Rows[i]["FileSize"]),
                            ImageFilePath = Normal.ListStr(dt.Rows[i]["ImageFilePath"]),
                            Detail = Normal.ListStr(dt.Rows[i]["Detail"])
                        });
                    list.Add(typeModel);
                }
                else
                {
                    if (gameid != 0)
                        typeModel.Games.Add(new GameModel()
                        {
                            ID = Normal.ParseInt(dt.Rows[i]["GameID"]),
                            Name = Normal.ListStr(dt.Rows[i]["Name"]),
                            FilePath = Normal.ListStr(dt.Rows[i]["FilePath"]),
                            FileSize = Normal.ListStr(dt.Rows[i]["FileSize"]),
                            ImageFilePath = Normal.ListStr(dt.Rows[i]["ImageFilePath"]),
                            Detail = Normal.ListStr(dt.Rows[i]["Detail"])
                        });

                }
            }
            return list;
        }

    }
}