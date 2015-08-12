using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ToolLib.data
{
    public class SqlTysjModel
    {
        public static DataTable Query(string sql)
        {
            return SqlModel.Query("SQL_TYSJ", sql);
        }

        public static int Execute(string sql)
        {
            return SqlModel.Execute("SQL_TYSJ", sql);
        }

    }
}
