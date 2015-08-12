using System;
using System.Configuration;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;
using ToolLib.data;
using ToolLib.util;
using ToolLib.exception;
namespace ToolLib.util
{
	/// <summary>
	/// ��ҳ��
	/// </summary>
	public class PageUtil
	{
		/// <summary>
		/// SQL��ORDER BYǰ����,��Ҫ�����DB2
		/// </summary>
		protected string fromStr="";  

		/// <summary>
		/// SQL��ORDER BY�󲿷�
		/// </summary>
		protected string orderStr=""; 

		/// <summary>
		/// ��ҳ������ַ
		/// </summary>
        protected string pageLink = ""; 

		/// <summary>
		/// ��ǰҳ��
		/// </summary>
		protected int currentPage = 1;

		/// <summary>
		/// �ܼ�¼��
		/// </summary>
		protected int sumRecords = 0;  

		/// <summary>
		/// ��ҳ��
		/// </summary>
		protected int pageCount = 0;   

		/// <summary>
		/// ÿҳ��¼��
		/// </summary>
		protected int pageSize = 10;   

		/// <summary>
		/// ����¼��
		/// </summary>
		protected int maxRecords = 1000;

        /// <summary>
        /// ��ǰ������
        /// </summary>
        protected string sortField = "";

        /// <summary>
        /// ��ǰ����ʽ
        /// </summary>
        protected string sortType = "";

        /// <summary>
        /// ���SQL
        /// </summary>
        protected string sqlStr = "";

        /// <summary>
        /// ������ҳBEAN
        /// </summary>
        /// <param name="FromStr">SQL��ORDER BYǰ����</param>
        /// <param name="OrderStr">SQL��ORDER BY�󲿷�</param>
        /// <param name="Request">��ǰ������</param>
		public PageUtil(string fromStr,string orderStr)
		{
			this.fromStr=fromStr;
			this.orderStr=Normal.Trim(orderStr);
            

            this.pageLink = Normal.Trim(HttpContext.Current.Request.Path);
			string queryStr=Normal.Trim(HttpContext.Current.Request.QueryString);
            //HttpContext.Current.Response.Write("pageLink=" + queryStr + "<br>" + "form=" + HttpContext.Current.Request.Form + "<br>");
			if (!queryStr.Equals("")&& !queryStr.Equals("null"))
				this.pageLink += "?" + queryStr;
			else
				this.pageLink +="?pageFlag=1";

			//int pos=this.pageLink.IndexOf("&currentPage=");
			//if(pos>=0)this.pageLink=this.pageLink.Substring(0,pos);
            this.pageLink = Regex.Replace(this.pageLink, "&currentPage=[^&]*", "");

            this.currentPage = Normal.ParseInt(HttpContext.Current.Request.QueryString["currentPage"]);

            this.sortField = Normal.ParseString(HttpContext.Current.Request.QueryString["sort"]);

            this.sortType = Normal.ParseString(HttpContext.Current.Request.QueryString["order"]);



            if (!this.sortField.Equals(""))
            {
                if (!this.sortType.ToLower().Equals("desc")) this.sortType = "asc";
                this.orderStr = this.sortField + " " + this.sortType;
            }

            

            this.pageSize = Normal.ParseInt(ConfigurationManager.AppSettings["PageSize"]);
            this.maxRecords = Normal.ParseInt(ConfigurationManager.AppSettings["MaxRecords"]);
            //HttpContext.Current.Response.Write("pageLink=" + queryStr + "<br>currentPage=" + this.currentPage + "<br>");
		}

		
		/// <summary>
		/// ���÷�ҳ
		/// </summary>
        protected void SetPage(string type,string key) 
		{
				string sql=" SELECT COUNT(*) AS CNT FROM("+fromStr+")XXX ";

                DataTable dt = DbModel.Query(type,key,sql);
				if(dt.Rows.Count>0)
				{
					sumRecords=Normal.ParseInt(dt.Rows[0]["cnt"]);
				}

                if (sumRecords > maxRecords) sumRecords = maxRecords;
			
				if(pageSize<1)pageSize=10;
				this.pageCount = this.sumRecords / pageSize;

				if (this.sumRecords % pageSize > 0)
					this.pageCount = this.pageCount + 1;

				if (this.currentPage < 1)
					this.currentPage = 1;

				if (this.currentPage > this.pageCount)
					this.currentPage = this.pageCount;
		}
       
		/// <summary>
		/// ��ȡ��ǰ��¼��
		/// </summary>
		/// <returns></returns>
		public DataTable GetDetail(string type,string key)
		{
			SetPage(type,key);
			string sql="";
            switch (type)
            {
                case DbTypes.DB2:
                    sql = " SELECT XXX.* FROM(" +
                        "    SELECT XX.*,rownumber() over(";
                    if (!orderStr.Equals(""))
                        sql += " ORDER BY  " + orderStr;
                    sql += "   ) AS RN FROM (" + fromStr + ") XX " +
                         ")XXX WHERE XXX.RN BETWEEN " + ((currentPage - 1) * pageSize + 1) + " AND " + (currentPage * pageSize);
                    break;
                case DbTypes.SQL:
                    sql = " SELECT XXX.* FROM(" +
                        "    SELECT TOP " + ((currentPage<pageCount)?pageSize:(sumRecords-(currentPage-1)*pageSize)) + "  * FROM(" +
                        "       SELECT TOP " + (currentPage * pageSize) + " * FROM(" + fromStr + ")X ";
                    string orderStrTemp = "";
                    if (!orderStr.Equals(""))
                    {
                        orderStrTemp = " Order by " + GetOrder(orderStr,true);
                        orderStr = " Order by " + GetOrder(orderStr, false);
                    }
                    sql += " " + orderStr +
                         "  )XX " + orderStrTemp +
                         ")XXX " + orderStr;
                    break;
                case DbTypes.ORA:
                    if (!orderStr.Equals(""))
                        sql += " ORDER BY  " + orderStr;

                    sql = " SELECT XXX.* FROM(" +
                        "    SELECT XX.*,ROWNUM AS RN FROM(" +
                        "       SELECT * FROM (" + fromStr + " )X ";
                    if (!orderStr.Equals(""))
                        sql += " ORDER BY  " + orderStr;
                    sql+="   )XX "+
                        " )XXX WHERE XXX.RN BETWEEN '" + ((currentPage - 1) * pageSize + 1) + "' AND '" + (currentPage * pageSize)+"'";
                    break;
            }
            sqlStr = sql;
            //HttpContext.Current.Response.Write(sql);
			return DbModel.Query(type,key,sql);
		}

        public DataTable GetDetail(string key)
        {
            return GetDetail(DbTypes.SQL, key);
        }

        public DataTable GetDetail()
        {
            return GetDetail(DbTypes.SQL, DbKey.SQL);
        }

		

		/// <summary>
		/// ��ȡ��ҳ����
		/// </summary>
		/// <returns></returns>
		public String GetPage() 
		{
            string rootPath = Normal.ParseString(ConfigurationManager.AppSettings["ServerPath"]);//HttpContext.Current.Request.ApplicationPath;
            string msg= "<table class=table_title><tr><td class=td_page>"+
                        "<img src='"+rootPath+"/images/page/first_pg.gif' onclick=\"javascript:location.href='{0}&currentPage=1';\" style='cursor:hand' valign='middle'>&nbsp;"+
                        "<img src='" + rootPath + "/images/page/pre_pg.gif' onclick=\"javascript:location.href='{0}&currentPage={2}';\" style='cursor:hand' valign='middle'>&nbsp;" +
                        "<img src='" + rootPath + "/images/page/next_pg.gif' onclick=\"javascript:location.href='{0}&currentPage={3}';\" style='cursor:hand' valign='middle'>&nbsp;" +
                        "<img src='" + rootPath + "/images/page/last_pg.gif' onclick=\"javascript:location.href='{0}&currentPage={4}';\" style='cursor:hand' valign='middle'>&nbsp;" +
                        "&nbsp;&nbsp;<span style='vertical-align:middle'>��"+sumRecords+"����¼,��{1}ҳ/��{4}ҳ&nbsp;"+
                        "<input type='text' name='currentPage' size='3' onkeydown=\"if(event.keyCode==13)xxkxx1location.href='{0}&currentPage='+document.all.currentPage.value;return false;xxkxx2\"></span>&nbsp;" +
                        "<img src='" + rootPath + "/images/page/go_pg.gif' onclick=\"javascript:location.href='{0}&currentPage='+document.all.currentPage.value;\" style='cursor:hand' valign='middle'>" +
                        "</td></tr></table>";
			return String.Format(msg,this.pageLink
				,Normal.ParseString(this.currentPage)
				,Normal.ParseString(this.currentPage - 1)
				,Normal.ParseString(this.currentPage + 1)
				,Normal.ParseString(this.pageCount)
				,Normal.ParseString(this.sumRecords)
                ).Replace("xxkxx1","{").Replace("xxkxx2","}");
				
		}

        public string GetSql()
        {
            return sqlStr;
        }
		public int GetCurrentPage() 
		{
			return currentPage;
		}

		public void SetCurrentPage(int currentPage) 
		{
			this.currentPage =currentPage;
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
			this.pageLink =pageLink;
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
                              "addLockTable(\""+lockId+"\","+rowHeadNum+","+colHeadNum+");" +
                              "</script>";
            return tableStr;
        }

        public string GetSortTable(string tableName,int rowHeadNum)
        {
            string pageLink1 = Regex.Replace(this.pageLink, "&order=[^&]*|&sort=[^&]*", "");
            string tableStr = "<script type=\"text/javascript\" language=\"javascript\">" +
                              "addSortTable(\""+tableName+"\","+rowHeadNum+",\"" + sortField + "\",\"" + sortType + "\",\"" + pageLink1 + "\");" +
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
                temporder = "[" + Normal.Trim(orderstr)+"]";
            return temporder;
        }
        /// <summary>
        /// �滻asc,desc
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
        /// ��ȡasc,desc�滻���
        /// </summary>
        /// <param name="orderstr"></param>
        /// <returns></returns>
        public string GetOrder(string orderstr,bool b)
        {
            string s = "";
            if (b)
            {//����

                if (orderstr.IndexOf(",") < 0)
                    s= AddSign(ReplaceOrder(orderstr));
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
