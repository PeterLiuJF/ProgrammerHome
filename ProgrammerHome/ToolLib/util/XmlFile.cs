using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace ToolLib.util
{
    public class XmlFile : XmlDocument
    {
        
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="xmlFile"></param>
        public XmlFile(string xmlFile)
        {

            this.Load(xmlFile);
        }

        /// <summary>
        /// 给定一个节点的xPath表达式并返回一个节点
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public XmlNode GetNode(string nodeName)
        {
            XmlNode xmlNode = this.SelectSingleNode(nodeName);
            return xmlNode;
        }

        /// <summary>
        /// 给定一个节点的xPath表达式返回其值
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public string GetNodeValue(string nodeName)
        {
            XmlNode xmlNode = this.SelectSingleNode(nodeName);
            return xmlNode.InnerText;
        }

        /// <summary>
        /// 给定一个节点的表达式返回此节点下的孩子节点列表
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public XmlNodeList GetNodeList(string nodeName)
        {
            XmlNodeList nodeList = this.SelectSingleNode(nodeName).ChildNodes;
            return nodeList;

        }

    }
}
