using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToolLib.DbAccess;
using ToolLib.util;
using System.Data;

namespace ProgrammerHome.Service
{
    public class RecreationService
    {
        #region 影视音乐
        public List<PlayDetailModel> GetMusicItems(int type, int userID = 0)
        {
            string sql = string.Format("select * from PlayList where type={0}", type);
            if (userID != 0)
                sql += string.Format(" and userID={0}", userID);

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

        public PlayListModel GetPlayListItems(int userID = 0)
        {
            string sql = "select * from PlayList";
            if (userID != 0)
                sql += string.Format(" where userID={0}", userID);

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

        public bool AddMusic(List<PlayDetailModel> models, int userID)
        {
            string temp = "insert into PlayList(title,artist,fileinfo,type,userid) values('{0}','{1}','{2}',{3},{4});";
            string sql = "";
            foreach (var item in models)
            {
                sql += string.Format(temp, item.title, item.artist, item.mp3, 1, userID);
            }
            return SqlAccess.ExecuteTran(sql) > 0;
        }
        #endregion

        public List<GameModel> GetGameItems(int userID = 0)
        {
            string sql = "select * from Game";
            if (userID != 0)
                sql += string.Format(" where userID={0}", userID);

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

        public List<GameTypeModel> GetGameTypeItems(int userID = 0)
        {

            string sql = @"select r.*,GameID=g.id,g.name,g.filepath,g.filesize,g.ImageFilePath,g.Detail 
from RecreationType r left join Game g on g.typeid=r.id where maintype='单机游戏'";

            if (userID != 0)
                sql += string.Format(" and g.userID={0}", userID);
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