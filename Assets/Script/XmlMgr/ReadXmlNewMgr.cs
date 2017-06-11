
//功能：
//创建者: 胡海辉
//创建时间：


using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using Assets.Script.Base;
using Assets.Script.Tools;

namespace Assets.Script
{
    public class XmlData
    {
        public virtual bool GetXmlDataAttribute(XmlNode node)
        {
            return true;
        }
    }

    public class ReadXmlNewMgr : TSingleton<ReadXmlNewMgr>, IDisposable
    {
        public Dictionary<int, List<XmlData>> GameXmlDataDic;
        public override void Init()
        {
            base.Init();
            GameXmlDataDic = new Dictionary<int, List<XmlData>>();
            int maxEnum = Enum.GetNames(typeof(XmlName)).Length; //(int)ReadXmlDataMgr.XmlName.Max;
            for (int i = 0; i < maxEnum; i++)
            {
                if (GameXmlDataDic.ContainsKey(i))
                {
                    GameXmlDataDic[i] = LoadCofig((XmlName) i);
                }
                else
                {
                    GameXmlDataDic.Add(i, LoadCofig((XmlName) i));
                }
            }
        }

        private List<XmlData> LoadCofig(XmlName name)
        {
            List<XmlData> tempList=new List<XmlData>();
            XmlNode node = LoadXmlFile(name);
            if (node == null)
            {
                DebugHelper.DebugLog("node====null");
                return null;
            }

            //XmlData data = ReadXmlDataMgr.GetInstance().GetXmlData(name);
            XmlNodeList childrenNodeList = node.ChildNodes;
            for (int ii = 0; ii < childrenNodeList.Count; ii++)
            {
                XmlData data = ReadXmlDataMgr.GetInstance().GetXmlData(name);
                data.GetXmlDataAttribute(childrenNodeList[ii]);
                tempList.Add(data);
            }
            return tempList;
        }

        public override void Dispose()
        {
            base.Dispose();
            GameXmlDataDic.Clear();
        }


        public MemoryStream LoadFile(XmlName name)
        {
            string mPath = ReadXmlDataMgr.instance.GetXmlPath(name);
            StreamReader reader = new StreamReader(mPath);
            UTF8Encoding encode = new System.Text.UTF8Encoding();
            byte[] binary = encode.GetBytes(reader.ReadToEnd());
            reader.Close();
            return new MemoryStream(binary);
        }

        public XmlDocument GetXmlDocByMemory(MemoryStream mStream)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlReader reader = XmlReader.Create(mStream);
            xmlDoc.Load(reader);
            reader.Close();
            return xmlDoc;
        }

        public XmlNodeList GetXmlNodeList(XmlDocument xmlDoc)
        {
            return xmlDoc.GetElementsByTagName("ReNodes");
        }

        public XmlNode LoadXmlFile(XmlName name)
        {
            MemoryStream stream = LoadFile(name);
            XmlNodeList nodeList = GetXmlNodeList(GetXmlDocByMemory(stream));
            if (nodeList == null || nodeList.Count <= 0) return null;
            return nodeList[0];
        }
    }
}
