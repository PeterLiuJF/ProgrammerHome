using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;

namespace ToolLib.util
{
    public class ExportUtil
    {
        public static void OutFile(string[] title, DataTable[] dt, string[] str, string fileName, string extName, string footStr)
        {
            OutFile(title, dt, str, fileName, extName, footStr, "utf-8");
        }
        /// <summary>
        /// 1 导出文件
        /// </summary>
        /// <param name="title">表头</param>
        /// <param name="dt">输出DATATABLE</param>
        /// <param name="str">输出字符</param>
        /// <param name="fileName">输出的文件名</param>
        /// <param name="extName">输出的文件扩展名</param>
        /// <param name="footStr">输出的数据来源</param>
        public static void OutFile(string[] title, DataTable []dt,string[]str,string fileName,string extName,string footStr,string encode)
        {
            //1.编码
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding(encode);

            //2.导出文件名
            string fileName1 = "";
            if (fileName.Equals(""))
            {
                fileName1 = title[0];
                if (fileName1 == null || fileName1.Equals("")) fileName1 = "招行导出";
                if (fileName1.Length > 10) fileName1 = fileName1.Substring(0, 10);
            }
            else
                fileName1 = fileName;

           // 

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName1) + "." + extName);

            string contentType = "";
            if (extName.Equals("doc"))
                contentType = "application/msword";
            else if (extName.Equals("xls"))
                contentType = "application/vnd.ms-excel";
            else if (extName.Equals("txt"))
                contentType = "text/plain";
            else
                contentType = "application/octet-stream";

            HttpContext.Current.Response.ContentType = contentType;   

            //3.输出文件头,txt文件除外
            if(!extName.Equals("txt"))
               HttpContext.Current.Response.Write(GetHeader().ToString());

            //4.输出datatable
            int k = 0;
            if (dt != null)
            {
                for (int i = 0; i < dt.Length; i++)
                {
                    if ((dt != null) && (dt[i] != null))
                    {
                        string title1 = "";
                        if (title.Length >= k + 1) title1 = title[k];
                        if (extName.Equals("xls")) 
                            HttpContext.Current.Response.Write(GetBody(title1, dt[i]).ToString());
                        else if (extName.Equals("txt")) 
                            HttpContext.Current.Response.Write(GetBodyTxt(title1, dt[i]).ToString());
                        else if (extName.Equals("doc"))
                            HttpContext.Current.Response.Write(GetBodyDoc(title1, dt[i]).ToString());
                        k = k + 1;
                    }
                }
            }

            //5.输出字符串
            if (str != null)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if ((str != null) && (str[i] != null))
                    {
                        string title1 = "";
                        if (title.Length >= k + 1) title1 = title[k];
                        if (extName.Equals("xls"))
                           HttpContext.Current.Response.Write(GetBody(title1, str[i]).ToString());
                        else if (extName.Equals("txt"))
                           HttpContext.Current.Response.Write(GetBodyTxt(title1, str[i]).ToString());
                        else if (extName.Equals("doc"))
                           HttpContext.Current.Response.Write(GetBodyDoc(title1, str[i]).ToString());
                        k = k + 1;
                    }
                }
            }

            //6.输出数据来源
            HttpContext.Current.Response.Write(GetFoot(footStr).ToString());


            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 2.1 DataTable导出EXCEL,默认文件名，来源
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        public static void OutExcel(string title, DataTable dt)
        {
            OutFile(new string[] { title }, new DataTable[] { dt }, new string[] { null }, "", "xls", "大智慧财汇数据科技有限公司");
        }

        /// <summary>
        ///2.2 DataTable导出EXCEL,默认文件名
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="footStr"></param>
        public static void OutExcel(string title, DataTable dt,string footStr)
        {
            OutFile(new string[] { title }, new DataTable[] { dt }, new string[] { null }, "","xls",footStr);
        }

        /// <summary>
        /// 2.3 DataTable导出EXCEL
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        /// <param name="footStr"></param>
        public static void OutExcel(string title, DataTable dt, string fileName,string footStr)
        {
            OutFile(new string[] { title }, new DataTable[] { dt }, new string[] { null }, fileName, "xls", footStr);
        }

        /// <summary>
        /// 2.4 string导出EXCEL，,默认文件名，来源
        /// </summary>
        /// <param name="title"></param>
        /// <param name="str"></param>
        public static void OutExcel(string title, string str)
        {
            OutFile(new string[] { title }, new DataTable[] { null }, new string[] { str }, "", "xls", "大智慧财汇数据科技有限公司");
        }

        /// <summary>
        /// 2.5 string导出EXCEL，,默认文件名
        /// </summary>
        /// <param name="title"></param>
        /// <param name="str"></param>
        /// <param name="footStr"></param>
        public static void OutExcel(string title, string str, string footStr)
        {
            OutFile(new string[] { title }, new DataTable[] { null }, new string[] { str }, "", "xls", footStr);
        }

        /// <summary>
        /// 2.6 string导出EXCEL
        /// </summary>
        /// <param name="title"></param>
        /// <param name="str"></param>
        /// <param name="fileName"></param>
        /// <param name="footStr"></param>
        public static void OutExcel(string title, string str, string fileName, string footStr)
        {
            OutFile(new string[] { title }, new DataTable[] { null }, new string[] { str }, fileName, "xls", footStr);
        }

        /// <summary>
        /// 3.1 DataTable导出WORD，,默认文件名，来源
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        public static void OutWord(string title, DataTable dt)
        {
            OutFile(new string[] { title }, new DataTable[] { dt }, new string[] { null }, "", "doc", "大智慧财汇数据科技有限公司");
        }

        /// <summary>
        ///  3.2 DataTable导出WORD，,默认文件名
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="footStr"></param>
        public static void OutWord(string title, DataTable dt, string footStr)
        {
            OutFile(new string[] { title }, new DataTable[] { dt }, new string[] { null }, "", "doc", footStr);
        }

        /// <summary>
        ///  3.3 DataTable导出WORD
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        /// <param name="footStr"></param>
        public static void OutWord(string title, DataTable dt, string fileName, string footStr)
        {
            OutFile(new string[] { title }, new DataTable[] { dt }, new string[] { null }, fileName, "doc", footStr);
        }

        /// <summary>
        ///  3.4 string导出WORD，,默认文件名，来源
        /// </summary>
        /// <param name="title"></param>
        /// <param name="str"></param>
        public static void OutWord(string title, string str)
        {
            OutFile(new string[] { title }, new DataTable[] { null }, new string[] { str }, "", "doc", "");
        }

        /// <summary>
        ///  3.5 string导出WORD，,默认文件名
        /// </summary>
        /// <param name="title"></param>
        /// <param name="str"></param>
        /// <param name="footStr"></param>
        public static void OutWord(string title, string str, string footStr)
        {
            OutFile(new string[] { title }, new DataTable[] { null }, new string[] { str }, "", "doc", footStr);
        }

        /// <summary>
        ///  3.6 string导出WORD
        /// </summary>
        /// <param name="title"></param>
        /// <param name="str"></param>
        /// <param name="fileName"></param>
        /// <param name="footStr"></param>
        public static void OutWord(string title, string str, string fileName, string footStr)
        {
            OutFile(new string[] { title }, new DataTable[] { null }, new string[] { str }, fileName, "doc", footStr);
        }


        /// <summary>
        /// 4.1 DataTable导出TXT，,默认文件名，来源
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        public static void OutText(string title, DataTable dt)
        {
            OutFile(new string[] { title }, new DataTable[] { dt }, new string[] { null }, "", "txt", "");
        }

        /// <summary>
        ///  4.2 DataTable导出TXT，,默认文件名
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="footStr"></param>
        public static void OutText(string title, DataTable dt, string footStr)
        {
            OutFile(new string[] { title }, new DataTable[] { dt }, new string[] { null }, "", "txt", footStr);
        }

        /// <summary>
        ///  4.3 DataTable导出TXT
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        /// <param name="footStr"></param>
        public static void OutText(string title, DataTable dt, string fileName, string footStr)
        {
            OutFile(new string[] { title }, new DataTable[] { dt }, new string[] { null }, fileName, "txt", footStr,"utf-8");
        }

        public static void OutText(string title, DataTable dt, string fileName, string footStr,string encode)
        {
            OutFile(new string[] { title }, new DataTable[] { dt }, new string[] { null }, fileName, "txt", footStr,encode);

        }
        /// <summary>
        ///  4.4 string导出TXT，,默认文件名，来源
        /// </summary>
        /// <param name="title"></param>
        /// <param name="str"></param>
        public static void OutText(string title, string str)
        {
            OutFile(new string[] { title }, new DataTable[] { null }, new string[] { str }, "", "txt", "");
        }

        /// <summary>
        ///  4.5 string导出TXT，,默认文件名
        /// </summary>
        /// <param name="title"></param>
        /// <param name="str"></param>
        /// <param name="footStr"></param>
        public static void OutText(string title, string str, string footStr)
        {
            OutFile(new string[] { title }, new DataTable[] { null }, new string[] { str }, "", "txt", footStr);
        }

        /// <summary>
        ///  4.6 string导出TXT
        /// </summary>
        /// <param name="title"></param>
        /// <param name="str"></param>
        /// <param name="fileName"></param>
        /// <param name="footStr"></param>
        public static void OutText(string title, string str, string fileName, string footStr)
        {
            OutFile(new string[] { title }, new DataTable[] { null }, new string[] { str }, fileName, "txt", footStr,"utf-8");
        }

        public static void OutText(string title, string str, string fileName, string footStr,string encode)
        {
            OutFile(new string[] { title }, new DataTable[] { null }, new string[] { str }, fileName, "txt", footStr,encode);
        }

        /// <summary>
        /// 5 输出头文件
        /// </summary>
        /// <returns></returns>
        public static StringBuilder GetHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns=\"http://www.w3.org/TR/REC-html40\">\n\r");
            sb.Append("<head>\n\r");
            
            sb.Append("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">\n\r");
            sb.Append("<meta name=ProgId content=Excel.Sheet>\n\r");
            sb.Append("<meta name=Generator content=\"Microsoft Excel 11\">\n\r");
            sb.Append("<style>\n\r");
            sb.Append("td {font-size:12px;border:.5pt solid black;}\n\r");
            sb.Append(".title {font-size:14px; font-weight:bold;height:30px;border:0px}\n\r");
            sb.Append(".title1 {font-size:14px; height:30px;border:0px}\n\r");
            sb.Append(".thead{font-weight:bold;}\n\r");
            sb.Append(".style0{mso-number-format:General;text-align:general;vertical-align:middle;white-space:normal;"+
                              "mso-rotate:0;mso-background-source:auto;mso-pattern:auto;color:windowtext;"+
                              "font-weight:400;font-style:normal;text-decoration:none;font-family:宋体;"+
                              "mso-generic-font-family:auto;mso-font-charset:134;border:none;"+
                              "mso-protection:locked visible;mso-style-name:常规;mso-style-id:0;"+
                              "font-size:9.0pt;border:.5pt solid black;}\n\r");
            sb.Append(".x1281{mso-style-parent:style0;mso-number-format:\"\\@\";border:.5pt solid black;font-weight:bold;}\n\r");
            sb.Append(".x1282{mso-style-parent:style0;mso-number-format:\"\\@\";border:.5pt solid black;}\n\r");
            sb.Append(".x0{mso-style-parent:style0;mso-number-format:\"0_ \";text-align:right;border:.5pt solid black;}\n\r");
            sb.Append(".x1{mso-style-parent:style0;mso-number-format:\"0\\.0_ \";text-align:right;border:.5pt solid black;}\n\r");
            sb.Append(".x2{mso-style-parent:style0;mso-number-format:\"0\\.00_ \";text-align:right;border:.5pt solid black;}\n\r");
            sb.Append(".x3{mso-style-parent:style0;mso-number-format:\"0\\.000_ \";text-align:right;border:.5pt solid black;}\n\r");
            sb.Append(".x4{mso-style-parent:style0;mso-number-format:\"0\\.0000_ \";text-align:right;border:.5pt solid black;}\n\r");
            sb.Append(".x5{mso-style-parent:style0;mso-number-format:\"0\\.00000_ \";text-align:right;border:.5pt solid black;}\n\r");
            sb.Append(".td_code{font-size:12px;line-height:20px;color:#333333;}\n\r");
            sb.Append(".td_search{font-size:12px;line-height:20px;color:#333333;text-align:right;vertical-align:top;}\n\r");
            sb.Append(".td_export{font-size:12px;line-height:20px;color:#333333;text-align:right;vertical-align:top;}\n\r");
            sb.Append(".td_title{font-size:12px;line-height:20px;background-color:#f8edc1;color:#333333;text-align:center;font-weight:bold;border:#999999 1px solid ;}\n\r");
            sb.Append(".td_head{font-size:12px;line-height:20px;background-color:#eeeeee;color:#333333;}\n\r");
            sb.Append(".td_head td{text-align:center; white-space:nowrap;border:.5pt solid black;}\n\r");
            sb.Append(".td_body1{font-size:12px;line-height:20px;background-color:#ffffff;color:#000000;}\n\r");
            sb.Append(".td_body1 td{border:.5pt solid black;}\n\r");
            sb.Append(".td_body2{font-size:12px;line-height:20px;background-color:#ececec;color:#000000;}\n\r");
            sb.Append(".td_body2 td{border:.5pt solid black;}\n\r");
            sb.Append(".td_page{background-color:white;white-space:nowrap;padding-right:15px;vertical-align:bottom;text-align:right;}\n\r");
            sb.Append(".td_sum{background-color:white;white-space:nowrap;vertical-align:bottom}\n\r");
            sb.Append(".td_left{font-size:12px;line-height:20px;background-color:#d6efff;color:#333333;text-align:left;border:.5pt solid black;}\n\r");
            sb.Append(".td_right{font-size:12px;line-height:20px;background-color:#ffffff;color:#000000;vertical-align:top;word-break:break-all;border:.5pt solid black;}\n\r");
            sb.Append(".td_foot{font-size:12px;line-height:20px;background-color:#eeeeee;color:#333333;border:0px}\n\r");
            sb.Append("</style>\n\r");
            sb.Append("</head>\n\r");
            sb.Append("<body>\n\r");
            

            return sb;           
        }
        

        /// <summary>
        ///6.1 DataTable导出EXCEL内容
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static StringBuilder GetBody(string title,DataTable dt)
        {            
            StringBuilder s = new StringBuilder();
            
            s.Append("<table ID=\"Table0\" BORDER=0 CELLSPACING=1 CELLPADDING=3 width=100% align=center>\n\r");
            s.Append("<tr>");
            int cols = dt.Columns.Count;
            if (cols > 12) cols = 12;
            s.Append("<td colspan=\""+cols+"\" align=center class=\"title\">" + title + "</td>");
            s.Append("</tr>\n\r");
            s.Append("</table>\n\r");
            s.Append("<table border=0 cellspacing=0 CELLPADDING=3 width=100% align=center>");
            s.Append("<tr>");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                s.Append("<td class='x1281'>" + dt.Columns[i].Caption.ToString().Replace("*" + i, "") + "</td>");
            }
            s.Append("</tr>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                s.Append("<tr>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    s.Append("<td class='" + GetCss(Normal.ParseString(dt.Rows[i][j]), dt.Columns[j].DataType.Name) + "'>" + Normal.ParseString(dt.Rows[i][j]) + "</td>");
                }
                s.Append("</tr>");
            }
            s.Append("</table>");
           
            
            return s;            
        }

        /// <summary>
        /// 6.2 DataTable导出TXT内容
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static StringBuilder GetBodyTxt(string title, DataTable dt)
        {
            StringBuilder s = new StringBuilder();
            s.Append("\t\t\t"+title + "\r\n");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                s.Append(dt.Columns[i].Caption.ToString().Replace("*" + i, "") + "&nbsp;");
            }
            s.Append("\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    s.Append(Normal.ParseString(dt.Rows[i][j]) + "&nbsp;");
                }
                s.Append("\n");
            }

            return s;
        }

        /// <summary>
        /// 6.3 DataTable导出WORD内容
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static StringBuilder GetBodyDoc(string title, DataTable dt)
        {
            StringBuilder s = new StringBuilder();
            s.Append("<div align=center>" + title + "</div><br>");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                s.Append(dt.Columns[i].Caption.ToString().Replace("*" + i, "") + "&nbsp;");
            }
            s.Append("\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    s.Append(Normal.ParseString(dt.Rows[i][j]) + "&nbsp;");
                }
                s.Append("\n");
            }

            return s;
        }


        /// <summary>
        /// 6.4 string导出EXCEL内容
        /// </summary>
        /// <param name="title"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static StringBuilder GetBody(string title, string str)
        {
            StringBuilder s = new StringBuilder();
            s.Append("<table ID=\"Table0\" BORDER=0 CELLSPACING=1 CELLPADDING=3 width=100% align=center>\n\r");
            s.Append("<tr>");
            s.Append("<td align=center class=\"title\"  colspan=\"10\">" + title + "</td>");
            s.Append("</tr>\n\r");
            s.Append("</table>\n\r");
            s.Append(str);            
            return s;
        }

        /// <summary>
        /// 6.5 string 导出TXT内容
        /// </summary>
        /// <param name="title"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static StringBuilder GetBodyTxt(string title, string str)
        {
            StringBuilder s = new StringBuilder();
            s.Append("\t\t\t" + title + "\r\n" + GetLineTxt(str));
            return s;
        }

        /// <summary>
        /// 6.6 string 导出WORD内容
        /// </summary>
        /// <param name="title"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static StringBuilder GetBodyDoc(string title, string str)
        {
            StringBuilder s = new StringBuilder();
           // s.Append("\t\t\t" + title + "\r\n" + str.Replace("<br>","\r\n").Replace("&nbsp;"," "));
            s.Append("<div align=center>" + title + "</div><br>" + str);
            return s;
        }

        /// <summary>
        /// 7.1 TEXT每行固定宽度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string GetLineTxt(string str)
        {
            StringBuilder s = new StringBuilder();
            if (str != null && !str.Equals(""))
            {
                string[] alltxt = str.Split("\r\n".ToCharArray());
                for (int i = 0; i < alltxt.Length; i++)
                    s.Append(GetLineStr(alltxt[i],60)+"\r\n");
            }
            return s.ToString();
        }

        /// <summary>
        /// 7.2 TEXT每行增加回车
        /// </summary>
        /// <param name="str"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static string GetLineStr(string str,int n)
        {
            StringBuilder s = new StringBuilder();
            int m = str.Length / n;
            for (int i = 0; i < m; i++)
                s.Append(str.Substring(i * n, n) + "\r\n");
            if(str.Length%n>0)
                s.Append(str.Substring(n*m)+"\r\n");
            return s.ToString();
        }

       

        /// <summary>
        /// 8 输出数据来源
        /// </summary>
        /// <returns></returns>
        public static StringBuilder GetFoot(string strSource)
        {
            StringBuilder s = new StringBuilder();
            if (strSource!=null&&!strSource.Equals(""))
            {
                s.Append("<table  BORDER=0 CELLSPACING=1 CELLPADDING=3 width=100% align=center>" +
                        "<tr><td height=\"30\" colspan=\"10\" style=\"border:0px\"></td></tr>" +
                        "<tr><td  colspan=\"10\"  class=\"td_foot\" >数据来源：" + strSource + "</td></tr>" +
                        "</table>");
                s.Append("</body></html>");
            }
            return s;
        }

        /// <summary>
        /// 9 获取单元格样式，主要是数字小数位数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="typename"></param>
        /// <returns></returns>
         public static string GetCss(string str,string typename)
        {
            string tempStr="x1282";
            if(!str.Equals("")&&("Int32,Decimal,Double".IndexOf(typename)>=0))
            {

                int m = 0;
                if(str.LastIndexOf(".")>=0)
                    m=str.Length - str.LastIndexOf('.') - 1;                
                if (m >= 0) tempStr = "x" + m;
            }
            return tempStr;
        }

    }
}
