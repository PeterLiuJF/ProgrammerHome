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
        /// ������
        /// </summary>
        /// <param name="xmlFile"></param>
        public XmlFile(string xmlFile)
        {

            this.Load(xmlFile);
        }

        /// <summary>
        /// ����һ���ڵ��xPath���ʽ������һ���ڵ�
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public XmlNode GetNode(string nodeName)
        {
            XmlNode xmlNode = this.SelectSingleNode(nodeName);
            return xmlNode;
        }

        /// <summary>
        /// ����һ���ڵ��xPath���ʽ������ֵ
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public string GetNodeValue(string nodeName)
        {
            XmlNode xmlNode = this.SelectSingleNode(nodeName);
            return xmlNode.InnerText;
        }

        /// <summary>
        /// ����һ���ڵ�ı��ʽ���ش˽ڵ��µĺ��ӽڵ��б�
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
