using System;
using System.Configuration;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using ToolLib.data;
using ToolLib.util;
using ToolLib.exception;
namespace ToolLib.util
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class PageXmlUtil
    {
        /// <summary>
        /// SQL的ORDER BY前部分,主要是针对DB2
        /// </summary>
        protected XmlNodeList fromNode=null;

        /// <summary>
        /// SQL的ORDER BY后部分
        /// </summary>
        protected string orderStr = "";

        /// <summary>
        /// 分页导航地址
        /// </summary>
        protected string pageLink = "";

        /// <summary>
        /// 当前页码
        /// </summary>
        protected int currentPage = 1;

        /// <summary>
        /// 总记录数
        /// </summary>
        protected int sumRecords = 0;

        /// <summary>
        /// 总页数
        /// </summary>
        protected int pageCount = 0;

        /// <summary>
        /// 每页记录数
        /// </summary>
        protected int pageSize = 10;

        /// <summary>
        /// 最大记录数
        /// </summary>
        protected int maxRecords = 1000;

        /// <summary>
        /// 当前排序列
        /// </summary>
        protected string sortField = "";

        /// <summary>
        /// 当前排序方式
        /// </summary>
        protected string sortType = "";

        /// <summary>
        /// 输出SQL
        /// </summary>
        protected string sqlStr = "";


        protected string keyword = "";

        /// <summary>
        /// 创建分页BEAN
        /// </summary>
        /// <param name="FromStr">SQL的ORDER BY前部分</param>
        /// <param name="OrderStr">SQL的ORDER BY后部分</param>
        /// <param name="Request">当前的请求</param>
        public PageXmlUtil(XmlNodeList fromNode, string orderStr)
        {
            this.fromNode = fromNode;
            this.orderStr = Normal.Trim(orderStr);


            this.pageLink = Normal.Trim(HttpContext.Current.Request.Path);
            string queryStr = Normal.Trim(HttpContext.Current.Request.QueryString);
            //HttpContext.Current.Response.Write("pageLink=" + queryStr + "<br>" + "form=" + HttpContext.Current.Request.Form + "<br>");
            if (!queryStr.Equals("") && !queryStr.Equals("null"))
                this.pageLink += "?" + queryStr;
            else
                this.pageLink += "?pageFlag=1";

            //int pos=this.pageLink.IndexOf("&currentPage=");
            //if(pos>=0)this.pageLink=this.pageLink.Substring(0,pos);
            this.pageLink = Regex.Replace(this.pageLink, "&currentPage=[^&]*", "");

            this.currentPage = Normal.ParseInt(HttpContext.Current.Request.QueryString["currentPage"]);

            this.sortField = Normal.ParseString(HttpContext.Current.Request.QueryString["sort"]);

            this.sortType = Normal.ParseString(HttpContext.Current.Request.QueryString["order"]);

            this.keyword = Normal.CheckPoint(HttpContext.Current.Request.QueryString["keyword"]);



            if (!this.sortField.Equals(""))
            {
                if (!this.sortType.ToLower().Equals("desc")) this.sortType = "asc";
                this.orderStr = this.sortField + " " + this.sortType;
            }



            this.pageSize = Normal.ParseInt(ConfigurationManager.AppSettings["PageSize"]);
            this.maxRecords = Normal.ParseInt(ConfigurationManager.AppSettings["MaxRecords"]);
            //HttpContext.Current.Response.Write("pageLink=" + queryStr + "<br>currentPage=" + this.currentPage + "<br>");
        }

        public DataTable XmlToDataTable(XmlNodeList node)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name", System.Type.GetType("System.String"));
            dt.Columns.Add("subject", System.Type.GetType("System.String"));
            dt.Columns.Add("dt", System.Type.GetType("System.String"));
            
            for (int i =0; i <fromNode.Count; i++)
            {
                
                    XmlNode subNode = fromNode[i]["公有元素"];
                    //log.Log.Error(keyword + "," + subNode["标题"].InnerText);

                    if (keyword.Equals("") || ((subNode["标题"].InnerText).IndexOf(keyword) >= 0))
                    {
                        DataRow dr = dt.NewRow();
                        dr["name"] = fromNode[i].Attributes["文献名称"].Value;
                        dr["subject"] = subNode["标题"].InnerText;
                        dr["dt"] = subNode["日期"].InnerText;
                        dt.Rows.InsertAt(dr, i);
                    }
            }

            return dt;

        }


        /// <summary>
        /// 设置分页
        /// </summary>
        protected void SetPage()
        {
            
            //sumRecords = fromNode.Count;

            if (sumRecords > maxRecords) sumRecords = maxRecords;

            if (pageSize < 1) pageSize = 10;
            this.pageCount = this.sumRecords / pageSize;

            if (this.sumRecords % pageSize > 0)
                this.pageCount = this.pageCount + 1;

            if (this.currentPage < 1)
                this.currentPage = 1;

            if (this.currentPage > this.pageCount)
                this.currentPage = this.pageCount;
        }

        /// <summary>
        /// 获取当前记录集
        /// </summary>
        /// <returns></returns>
        public DataTable GetDetail()
        {
            //SetPage();


            DataView dv = XmlToDataTable(fromNode).DefaultView;
            
            
            dv.Sort="dt desc";
            DataTable dt = new DataTable();
            
            dt.Columns.Add("name", System.Type.GetType("System.String"));
            dt.Columns.Add("subject", System.Type.GetType("System.String"));
            dt.Columns.Add("dt", System.Type.GetType("System.String"));

            sumRecords = dv.Count;
            if (sumRecords <= 0) return dt;
            SetPage();


            
            for (int i = ((currentPage - 1) * pageSize); (i < (currentPage * pageSize)) && (i < sumRecords); i++)
            {
                DataRow dr = dt.NewRow();
                dr["name"] = dv[i]["name"].ToString();
                dr["subject"] = dv[i]["subject"].ToString();
                dr["dt"] = dv[i]["dt"].ToString();
                dt.Rows.InsertAt(dr, i);
            }
            /*
            DataTable dt = new DataTable();
            dt.Columns.Add("name", System.Type.GetType("System.String"));
            dt.Columns.Add("subject", System.Type.GetType("System.String"));
            dt.Columns.Add("dt", System.Type.GetType("System.String"));

            if (sortType.Equals("asc"))
            {
                for (int i = ((currentPage - 1) * pageSize); (i < (currentPage * pageSize)) && (i < sumRecords); i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["name"] = fromNode[i].Attributes["文献名称"].Value;
                    XmlNode subNode = fromNode[i]["公有元素"];
                    dr["subject"] = subNode["标题"].InnerText;
                    dr["dt"] = subNode["日期"].InnerText;
                    dt.Rows.InsertAt(dr, i);
                }
            }
            else
            {
                for (int i = sumRecords - 1 - ((currentPage - 1) * pageSize); (i > sumRecords - 1 - (currentPage * pageSize) ) && (i >= 0); i--)
                {
                    DataRow dr = dt.NewRow();
                    dr["name"] = fromNode[i].Attributes["文献名称"].Value;
                    XmlNode subNode = fromNode[i]["公有元素"];
                    dr["subject"] = subNode["标题"].InnerText;
                    dr["dt"] = subNode["日期"].InnerText;
                    dt.Rows.InsertAt(dr, sumRecords-1-i);
                }
            }
            */
            return dt;
        }

        /// <summary>
        /// 获取分页导航
        /// </summary>
        /// <returns></returns>
        public String GetPage()
        {
            string rootPath = Normal.ParseString(ConfigurationManager.AppSettings["ServerPath"]);//HttpContext.Current.Request.ApplicationPath;
            string msg = "<table class=table_title><tr><td class=td_page>" +
                        "<img src='" + rootPath + "/images/page/first_pg.gif' onclick=\"javascript:location.href='{0}&currentPage=1';\" style='cursor:hand' valign='middle'>&nbsp;" +
                        "<img src='" + rootPath + "/images/page/pre_pg.gif' onclick=\"javascript:location.href='{0}&currentPage={2}';\" style='cursor:hand' valign='middle'>&nbsp;" +
                        "<img src='" + rootPath + "/images/page/next_pg.gif' onclick=\"javascript:location.href='{0}&currentPage={3}';\" style='cursor:hand' valign='middle'>&nbsp;" +
                        "<img src='" + rootPath + "/images/page/last_pg.gif' onclick=\"javascript:location.href='{0}&currentPage={4}';\" style='cursor:hand' valign='middle'>&nbsp;" +
                        "&nbsp;&nbsp;<span style='vertical-align:middle'>第{1}页/共{4}页&nbsp;" +
                        "<input type='text' name='currentPage' size='3' onkeydown=\"if(event.keyCode==13)xxkxx1location.href='{0}&currentPage='+document.all.currentPage.value;return false;xxkxx2\"></span>&nbsp;" +
                        "<img src='" + rootPath + "/images/page/go_pg.gif' onclick=\"javascript:location.href='{0}&currentPage='+document.all.currentPage.value;\" style='cursor:hand' valign='middle'>" +
                        "</td></tr></table>";
            return String.Format(msg, this.pageLink
                , Normal.ParseString(this.currentPage)
                , Normal.ParseString(this.currentPage - 1)
                , Normal.ParseString(this.currentPage + 1)
                , Normal.ParseString(this.pageCount)
                , Normal.ParseString(this.sumRecords)).Replace("xxkxx1", "{").Replace("xxkxx2", "}");

        }

        public string GetSql()
        {
            return sqlStr;
        }

        public string GetKeyword()
        {
            return keyword;
        }

        public void SetKeyword(string keyword)
        {
            this.keyword = keyword;
        }

        public int GetCurrentPage()
        {
            return currentPage;
        }

        public void SetCurrentPage(int currentPage)
        {
            this.currentPage = currentPage;
        }

        public int GetMaxRecords()
        {
            return maxRecords;
        }

        public void SetMaxRecords(int maxRecords)
        {
            this.maxRecords = maxRecords;
        }

        public int GetPageCount()
        {
            return pageCount;
        }

        public void SetPageCount(int pageCount)
        {
            this.pageCount = pageCount;
        }

        public String GetPageLink()
        {
            return pageLink;
        }

        public void SetPageLink(String pageLink)
        {
            this.pageLink = pageLink;
        }

        public int GetPageSize()
        {
            return pageSize;
        }

        public void SetPageSize(int pageSize)
        {
            this.pageSize = pageSize;
        }

        public int GetSumRecords()
        {
            return sumRecords;
        }

        public void SetSumRecords(int sumRecords)
        {
            this.sumRecords = sumRecords;
        }

        public string GetLockTable(string lockId, int rowHeadNum, int colHeadNum)
        {
            string tableStr = "<script type=\"text/javascript\" language=\"javascript\">" +
                              "addLockTable(\"" + lockId + "\"," + rowHeadNum + "," + colHeadNum + ");" +
                              "</script>";
            return tableStr;
        }

        public string GetSortTable(string tableName, int rowHeadNum)
        {
            string pageLink1 = Regex.Replace(this.pageLink, "&order=[^&]*|&sort=[^&]*", "");
            string tableStr = "<script type=\"text/javascript\" language=\"javascript\">" +
                              "addSortTable(\"" + tableName + "\"," + rowHeadNum + ",\"" + sortField + "\",\"" + sortType + "\",\"" + pageLink1 + "\");" +
                              "</script>";
            return tableStr;
        }

        public string AddSign(string orderstr)
        {
            string temporder = "";
            orderstr = orderstr.ToLower();
            orderstr = orderstr.Replace("[", "").Replace("]", "");
            int n1 = orderstr.IndexOf("asc");
            int n2 = orderstr.IndexOf("desc");
            int n = -1;
            n = (n1 >= 0 ? n1 : (n2 >= 0 ? n2 : -1));
            if (n >= 0)
                temporder = "[" + Normal.Trim(orderstr.Substring(0, n)) + "] " + orderstr.Substring(n - 1);
            else
                temporder = "[" + Normal.Trim(orderstr) + "]";
            return temporder;
        }
        /// <summary>
        /// 替换asc,desc
        /// </summary>
        /// <param name="orderstr"></param>
        /// <returns></returns>
        public string ReplaceOrder(string orderstr)
        {
            string temporder = "";
            orderstr = orderstr.ToLower();
            if (orderstr.IndexOf("desc") >= 0)
                temporder = orderstr.Replace("desc", "asc");
            else
                if (orderstr.IndexOf("asc") >= 0)
                    temporder = orderstr.Replace("asc", "desc");
                else
                    temporder = orderstr + " desc";
            return temporder;
        }

        /// <summary>
        /// 获取asc,desc替换结果
        /// </summary>
        /// <param name="orderstr"></param>
        /// <returns></returns>
        public string GetOrder(string orderstr, bool b)
        {
            string s = "";
            if (b)
            {//反序

                if (orderstr.IndexOf(",") < 0)
                    s = AddSign(ReplaceOrder(orderstr));
                else
                {
                    string[] arrorder = orderstr.Split(",".ToCharArray());
                    for (int i = 0; i < arrorder.Length; i++)
                    {
                        if (!Normal.Trim(arrorder[i]).Equals(""))
                        {
                            if (!s.Equals("")) s += ",";
                            s += AddSign(ReplaceOrder(arrorder[i]));
                        }

                    }
                }
            }
            else
            {
                if (orderstr.IndexOf(",") < 0)
                    s = AddSign(orderstr);
                else
                {
                    string[] arrorder = orderstr.Split(",".ToCharArray());
                    for (int i = 0; i < arrorder.Length; i++)
                    {
                        if (!Normal.Trim(arrorder[i]).Equals(""))
                        {
                            if (!s.Equals("")) s += ",";
                            s += AddSign(arrorder[i]);
                        }

                    }
                }
            }
            return s;
        }
    }
}
