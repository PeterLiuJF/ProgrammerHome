using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ToolLib.data
{
    public class SqlMacroModel
    {
        public static DataTable Query(string sql)
        {
            return SqlModel.Query("SQL_HGHY", sql);
        }

        public static int Execute(string sql)
        {
            return SqlModel.Execute("SQL_HGHY", sql);
        }

    }
}
