
//功能： 存储游戏数据
//创建者: 胡海辉
//创建时间：


using Assets.Script.Base;
using Assets.Script.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Assets.Script.CradManager;

namespace Assets.Script
{
    public class SaveGameDataXmlMgr : TSingleton<SaveGameDataXmlMgr>, IDisposable
    {
        private string saveFilePath;
        private const string ROOT_XML_NAME = "ReNodes";
        private const string CRAD_INFO_ELEMENT = "CardData";
        private const string CRAD_INFO_ELEMENT_ATTRIBUTE = "ID";
        public override void Init()
        {
            base.Init();
            saveFilePath = ReadXmlDataMgr.instance.GetXmlPath(XmlName.CardData);
         
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public List<CardData> GetCradDataInfoByXml()
        {
            List<CardData> tempList = new List<CardData>();
            List<XmlData> tempXmlDataList = new List<XmlData>();
            if (ReadXmlNewMgr.instance.GameXmlDataDic.ContainsKey((int)XmlName.CardData) == false)
            {
                return tempList;
            }
            tempXmlDataList = ReadXmlNewMgr.instance.GameXmlDataDic[(int)XmlName.CardData];
            for (int i = 0; i < tempXmlDataList.Count; i++)
            {
                tempList.Add((CardData)tempXmlDataList[i]);
            }
            return tempList;
        }

        public void CreateCradDataXml(List<CardData> cardDataList)
        {
            File.Create(saveFilePath).Close();
            XmlDocument xml = new XmlDocument();
            XmlElement root = xml.CreateElement(ROOT_XML_NAME);
            for (int i = 0; i < cardDataList.Count; i++)
            {
                XmlElement element = xml.CreateElement(CRAD_INFO_ELEMENT);
                element.SetAttribute(CRAD_INFO_ELEMENT_ATTRIBUTE, i.ToString());
                FindClassAllField<CardData>(cardDataList[i], element, xml);
                root.AppendChild(element);
            }
            xml.AppendChild(root);
            xml.Save(saveFilePath);
        }

        private void FindClassAllField<T>(T target, XmlElement element, XmlDocument xml) where T : new()
        {
            Type t = target.GetType();
            FieldInfo[] fields = t.GetFields();
            string fieldName = string.Empty;
            for (int i = 0; i < fields.Length; i++)
            {
                fieldName = fields[i].Name;
                XmlElement elementChild1 = xml.CreateElement(fieldName);
                elementChild1.InnerText = fields[i].GetValue(target).ToString();
                element.AppendChild(elementChild1);
            }
        }
    }
}
