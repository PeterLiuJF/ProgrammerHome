using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Text.RegularExpressions;
using ToolLib.data;
using ToolLib.util;

namespace ToolLib.util
{
    public class TreeUtil
    {
        /// <summary>
        /// 1.1 画树型菜单DataTable
        /// dt必须包含以下列：
        ///   rootid:根ID
        ///   parentid:父ID
        ///   operateid:ID
        ///   level:级别
        ///   serial:序列,格式 _rootid_parentid_id_
        ///   operatename:显示名称
        ///   pageurl:地址
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static string DrawTree(DataTable dt,string title)
        {
            XmlDocument doc = DataTableToXml(dt,title);
            return DrawTree(doc);
        }

        /// <summary>
        /// 1.2 画树型菜单DataTable,默认标题
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DrawTree(DataTable dt)
        {
            XmlDocument doc = DataTableToXml(dt,"目录");
            return DrawTree(doc);

        }

        /// <summary>
        /// 2.1 画树型菜单file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string DrawTree(string file)
        {
            return DrawTree(file, "");
        }
        public static string DrawTree(string file,string filter)
        {
            try
            {
                string xmlfile = HttpContext.Current.Request.PhysicalApplicationPath + "\\" + file;
                //return xmlfile;
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlfile);
                return DrawTree(doc, filter);
            }
            catch (Exception e)
            {
                return "当前目录不存在，请重试!"+e.ToString();
            }
        }


        /// <summary>
        /// 3.1 画树型菜单XmlDocument
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string DrawTree(XmlDocument doc)
        {
            return DrawTree(doc, "", true);
        }
        public static string DrawTree(XmlDocument doc,string filter)
        {
            return DrawTree(doc, filter, true);
        }

        /// <summary>
        /// 3.2 画树型菜单XmlDocument
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="b">是否写脚本</param>
        /// <returns></returns>
        public static string DrawTree(XmlDocument doc, bool b)
        {
            return DrawTree(doc, "", b);
        }
        public static string DrawTree(XmlDocument doc,string filter,bool b)
        {
            StringBuilder s = new StringBuilder();
            XmlNodeList rootNodeList = doc.SelectSingleNode("root").ChildNodes;// xml.GetNodeList("root");
            for (int i = 0; i < rootNodeList.Count; i++)
            {
                XmlNode node = rootNodeList.Item(i);
                s.Append(DrawChildTree(node, 1, "_" + node.Attributes["name"].Value + "_",filter));
            }
            if(b)s.Append(WriteScript());
            return s.ToString();
        }

        /// <summary>
        /// 4.1 DataTable转为XML
        /// dt必须包含以下列：
        ///   parentid:父ID
        ///   operateid:ID
        ///   operatename:显示名称
        ///   pageurl:地址
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static XmlDocument DataTableToXml(DataTable dt,String title)
        {
            XmlDocument doc = new XmlDocument();

            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            s.Append("<menu name=\""+title+"\">\n");
            s.Append(DrawChildTree(dt, "0"));
            s.Append("</menu>\n");
            s.Append("</root>");
            doc.LoadXml(s.ToString());

            return doc;
        }

        /// <summary>
        /// 4.2 DataTable转为XML,默认标题
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static XmlDocument DataTableToXml(DataTable dt)
        {
            return DataTableToXml(dt, "目录");
        }


        /// <summary>
        /// 4.3 DataTable转为string,测试时用
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string DataTableToString(DataTable dt, String title)
        {
            //XmlDocument doc = new XmlDocument();

            StringBuilder s = new StringBuilder();
            s.Append("<root>");
            s.Append("<menu name=\"" + title + "\">\n");
            s.Append(DrawChildTree(dt, "0"));
            s.Append("</menu>\n");
            s.Append("</root>");
            //doc.LoadXml(s.ToString());

            return s.ToString();
        }

        /// <summary>
        /// 5.1 画子树DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        private static string DrawChildTree(DataTable dt, string parentid)
        {
            StringBuilder s = new StringBuilder();
            DataView dv1 = new DataView(dt);//dt.DefaultView;
            dv1.RowFilter = "parentid='" + parentid + "'";

            for (int i = 0; i < dv1.Count; i++)
            {
                string operateid = Normal.ParseString(dv1[i]["operateid"]);
                string operatename = Normal.ParseString(dv1[i]["operatename"]);
                string pageurl = Normal.ParseString(dv1[i]["pageurl"]);

                s.Append("<submenu name=\"" + operatename + "\" ");

                if (!pageurl.Equals(""))
                {
                   
                    s.Append(" url=\"" + pageurl.Replace("&", "&amp;") + "\" target=\"right\"");
                }
                s.Append(">");
                s.Append(DrawChildTree(dt, operateid));
                s.Append("</submenu>");
            }
            return s.ToString();

        }

        /// <summary>
        /// 5.2 画XML子树
        /// </summary>
        /// <param name="node">当前节点</param>
        /// <param name="level">当前级别</param>
        /// <param name="serial">当前序列</param>
        /// <returns></returns>
        private static string DrawChildTree(XmlNode node, int level, string serial)
        {
            return DrawChildTree(node, level, serial, "");
        }
        //2010-09-04增加过滤条件，过滤没有权限的。
        private static string DrawChildTree(XmlNode node, int level, string serial,string filter)
        {
            StringBuilder s = new StringBuilder();
            string rootPath = Normal.ParseString(ConfigurationManager.AppSettings["ServerPath"]);//HttpContext.Current.Request.ApplicationPath;
            string nodeName = "";
            try
            {
                nodeName = Normal.ListStr(node.Attributes["name"].Value);
            }
            catch { }
            string nodeUrl = "";
            try
            {
                nodeUrl = Normal.ParseString(node.Attributes["url"].Value);
            }
            catch { }
            string nodeTarget = "";
            try
            {
                nodeTarget = Normal.ListStr(node.Attributes["target"].Value);
            }
            catch { }
            if (nodeTarget.Equals("")) nodeTarget = "_self";

            string nodeEvent = "";
            try
            {
                nodeEvent = Normal.ListStr(node.Attributes["event"].Value);
            }
            catch { }

            if (!string.IsNullOrEmpty(filter))
            {
                if (("," + filter + ",").IndexOf("," + nodeName + ",") < 0)
                {
                    return "";//过滤没有权限的。
                }
            }

            XmlNodeList nodeList = node.ChildNodes;

            s.Append("<div id=\"Tree" + serial + "\" style='height:20px;white-space:nowrap;letter-spacing:0.5pt;");
            if (level == 1) s.Append(" font-weight:bold;");
            s.Append("'>\n");

            s.Append(DrawLine(node));

            if (nodeList.Count > 0)
            {
                s.Append("<a href=\"javascript:expandIt('" + serial + "');\">" +
                         "<img  id=\"Tree" + serial + "Pic\" src='" + rootPath + "/images/tree/");
                if (level > 1)
                {
                    if (node.ParentNode.ChildNodes[node.ParentNode.ChildNodes.Count - 1] == node)
                        s.Append("line_add3.gif");
                    else
                        s.Append("line_add2.gif");
                }
                else
                {
                    //默认第一级打开
                    if (node.ParentNode.ChildNodes.Count == 1)
                    {
                        s.Append("line_jian5.gif");
                    }
                    else
                    {
                        if (node.ParentNode.ChildNodes[0] == node)
                            s.Append("line_jian4.gif");
                        else if (node.ParentNode.ChildNodes[node.ParentNode.ChildNodes.Count - 1] == node)
                            s.Append("line_jian3.gif");
                        else
                            s.Append("line_jian2.gif");
                    }
                }
                s.Append("' border=0></a>");
            }
            else
            {
                s.Append("<img src='" + rootPath + "/images/tree/");
                if (level==1&&node.ParentNode.ChildNodes[0] == node)
                    s.Append("line4.gif");
                else if (node.ParentNode.ChildNodes[node.ParentNode.ChildNodes.Count - 1] == node)
                    s.Append("line3.gif");
                else
                    s.Append("line2.gif");
                s.Append("' border=0>");
            }
            s.Append("<span style='vertical-align:bottom'>");

            s.Append("<a ");
            if (!nodeEvent.Equals(""))
                s.Append(" onclick=\"" + nodeEvent + "\" ");

            if (!nodeUrl.Equals(""))
            {
                //nodeUrl = Regex.Replace(nodeUrl, "([\\W]*)", "'+escape('$1')+'");//转换所有的中文
                nodeUrl = Regex.Replace(nodeUrl, "([^?=/&.a-zA-Z0-9_]+)", "'+escape('$1')+'");
                s.Append(" href=\"javascript:opencontent('" + rootPath + "/" + nodeUrl + "','" + nodeTarget + "');\"");             
                
            }
            else
                s.Append(" href=\"#\"");
            s.Append(" title=\"" + Normal.Trim(nodeName) + "\">" + Normal.Trim(nodeName) + "</a>");
            s.Append("</span>");
            s.Append("\n</div>\n");

            if (nodeList.Count > 0)
            {
                s.Append("<div id=\"Tree" + serial + "Child\" ");
                if (level > 1) s.Append("style='display:none'");
                s.Append(">");
                for (int k = 0; k < nodeList.Count; k++)
                {
                    XmlNode subNode = nodeList.Item(k);
                    s.Append(DrawChildTree(subNode, level + 1, serial + subNode.Attributes["name"].Value + "_", filter));
                }
                s.Append("</div>");
            }
            return s.ToString();
        }

        /// <summary>
        /// 6 画补充线
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string DrawLine(XmlNode node)
        {
            string rootPath = Normal.ParseString(ConfigurationManager.AppSettings["ServerPath"]);//HttpContext.Current.Request.ApplicationPath;
            if (node.ParentNode.Name.Equals("root")) return "";
            string s = "";

            if (node.ParentNode.ParentNode.ChildNodes[node.ParentNode.ParentNode.ChildNodes.Count - 1] == node.ParentNode)
            {
                s += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";  //上上级是最后一级
            }
            else
            {
                s += "<img src='" + rootPath + "/images/tree/line1.gif ' border=0>";
            }

            s = DrawLine(node.ParentNode) + s;

            return s;
        }

        /// <summary>
        /// 7 画脚本
        /// </summary>
        /// <returns></returns>
        public static string WriteScript()
        {
            StringBuilder s = new StringBuilder();
            s.Append(@"<script language='javascript'>                                    
                       function expandIt(obj){                                        
                          var ee=document.getElementById('Tree'+obj+'Pic');        
                          var ef=document.getElementById('Tree'+obj+'Child');     
                          if(ee!=null){                                                
                              if(ee.src.indexOf('jian')>=0)                         
                                         ee.src=ee.src.replace(/jian/g,'add');     
                              else                                                     
                                         ee.src=ee.src.replace(/add/g,'jian');     
                          }                                                            
                          if(ef!=null){                                                
                              if(ef.style.display=='none')                         
                                         ef.style.display='block';                   
                              else                                                    
                                         ef.style.display='none';                   
                          }                                                           
                       }                  
                       
                       //找对应地址                                           
                       function findIt(address){  
                           var eeid=null;                                  
                           for(var i=0;i<document.all.length;i++){                    
                              var ee1=document.all[i];                                   
                              if(ee1!=null&&ee1.id!=null&&ee1.id.indexOf('Tree_')>=0&&ee1.id.indexOf('Child')<0&&ee1.id.indexOf('Pic')<0){                                 
                                  if(ee1.innerHTML.indexOf(address)>=0){                       
                                       eeid=ee1.id;   
                                       break;                                       
                                  }                                                    
                              }                                                          
                          } 
                          var s=eeid;
                          if(eeid!=null){
                             for(var i=0;i<document.all.length;i++){                    
                                var ee1=document.all[i];          
                                if(ee1!=null&&ee1.id!=null&&ee1.id!=eeid&&ee1.id.indexOf('Tree_')>=0&&ee1.id.indexOf('Child')<0&&eeid.indexOf(ee1.id)>=0){
                                    s+=ee1.id+',';
                                    var ee=document.getElementById(ee1.id+'Pic');        
                                    var ef=document.getElementById(ee1.id+'Child');     
                                    if(ee!=null){                           
                                         ee.src=ee.src.replace(/add/g,'jian');     
                                    }                                                            
                                    if(ef!=null){                                 
                                         ef.style.display='block';                   
                                    }           
                               }        
                            }  
                          }                                               
                       }       
                       
                       //打开链接            
                        function opencontent(address,target){
                           if(target==null)target='right';
                           // window.open(address,target);    
                           
                           var plusaddress='';
                           if(parent.right!=null){
                              var rightaddress=parent.right.location; 
                              rightaddress=rightaddress.toString();                                                           
                              var symbol1=rightaddress.match(/[?|&]+symbol=[^&]*/ig);
                              var exchange1=rightaddress.match(/[?|&]+exchange=[^&]*/ig);
                              var stype1=rightaddress.match(/[?|&]+stype=[^&]*/ig);

                              if(symbol1!=null)plusaddress+=symbol1;
                              //if(plusaddress!='')plusaddress+='&';
                              if(exchange1!=null)plusaddress+=exchange1;
                              //if(plusaddress!='')plusaddress+='&';
                              if(stype1!=null)plusaddress+=stype1;                              
                              //address+=plusaddress;

                              if(plusaddress!=''){
                                 if(address.indexOf('?')<0)
                                    address+='?';
                                 else
                                    address+='&';
                                 address+=plusaddress;
                              }
                           }
                           //alert(address);
                           
                           //address+='symbol='+document.all.symbol.value+document.all.exchange.value+'&stype='+document.all.stype.value;
                           window.open(address,target);          
                       }
                       </script> ");
            return s.ToString();
        }
    }



}
