
//功能： 读取的Xml数据管理
//创建者: 胡海辉
//创建时间：


using Assets.Script.Base;
using Assets.Script.Tools;
using System;
using UnityEngine;
using System.Xml;

namespace Assets.Script
{
    public class ReadXmlDataMgr : TSingleton<ReadXmlDataMgr>, IDisposable
    {

        public XmlData GetXmlData(XmlName name)
        {
            switch (name)
            {
              //  case XmlName.AudioData: return new AudioData();
                case XmlName.CardData: return new CardData();
                default: return new XmlData();
            }
        }

        public string GetXmlPath(XmlName name)
        {
            return PlatformTools.m_FileRealPath + "/Config/" + name + ".xml";
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public int IntParse(XmlNode node, string name, int index)
        {
            return (int)Parse(node, name, 0, index);
        }

        public bool BoolParse(XmlNode node, string name, int index)
        {
            return (bool)Parse(node, name, true, index);
        }

        public string StrParse(XmlNode node, string name, int index)
        {
            return (string)Parse(node, name, "", index);
        }

        public float FloatParse(XmlNode node, string name, int index)
        {
            return (float)Parse(node, name, 0.0f, index);
        }

        public object Parse(XmlNode node, string name, object defaultValue, int index)
        {
            string value = "";
            try
            {
                value = node.ChildNodes[index].InnerText;
            }
            catch (System.Exception ex)
            {
                DebugHelper.DebugLogError(ex.Message);
            }
            return Convert.ChangeType(value, defaultValue.GetType());
        }
    }

    /// <summary>
    /// 卡牌基本数据
    /// </summary>
    [Serializable]
    public class CardData : XmlData
    {
        public string CradName;
        public bool IsShow;
        public int ConfigId;
        public int HpValue;
        public ActorTypeEnum ActorType;
        public PokerTypeEnum PokerType;
        public string SpritePath;

        public CardData ()
        {
            CradName = "";
            SpritePath = "";
        }

        public override bool GetXmlDataAttribute(XmlNode node)
        {
            CradName = ReadXmlDataMgr.instance.StrParse(node, "CradName", 0);
            IsShow = ReadXmlDataMgr.instance.BoolParse(node, "IsShow", 1);
            ConfigId = ReadXmlDataMgr.instance.IntParse(node, "ConfigId", 2);
            HpValue = ReadXmlDataMgr.instance.IntParse(node, "HpValue", 3);
            ActorType = (ActorTypeEnum)Enum.Parse(typeof(ActorTypeEnum),ReadXmlDataMgr.instance.StrParse(node, "ActorType", 4));
            PokerType = (PokerTypeEnum)Enum.Parse(typeof(PokerTypeEnum), ReadXmlDataMgr.instance.StrParse(node, "PokerType", 5));
            SpritePath = ReadXmlDataMgr.instance.StrParse(node, "SpritePath", 6);
            return base.GetXmlDataAttribute(node);
        }
    }

    /// <summary>
    /// 音频数据
    /// </summary>
    [Serializable]
    public class AudioData : XmlData
    {
        public int ID;
        public string Name;
        public string AudioName;

        public override bool GetXmlDataAttribute(XmlNode node)
        {
            ID = ReadXmlDataMgr.instance.IntParse(node, "ID", 0);
            Name = ReadXmlDataMgr.instance.StrParse(node, "Name", 0);
            AudioName = ReadXmlDataMgr.instance.StrParse(node, "AudioName", 0);
            return base.GetXmlDataAttribute(node);
        }

    }

}
