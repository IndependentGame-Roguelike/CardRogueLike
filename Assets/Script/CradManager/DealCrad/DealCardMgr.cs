using System.Collections.Generic;
using Assets.Script.Base;
using Assets.Script.Tools;
using System;
using System.Diagnostics;

namespace Assets.Script.CradManager
{
    public class DealCardMgr : TSingleton<DealCardMgr>, IDisposable
    {
        public Queue<CardData> AllCardQueue;
        public override void Init()
        {
            base.Init();
            AllCardQueue = new Queue<CardData>(StaticMemberMgr.MAX_CARD_COUNT);
        }

        public void InitData()
        {
            //读取卡片信息
            List<XmlData> tempAllCardList = ReadXmlNewMgr.instance.GameXmlDataDic[(int)XmlName.CardData];
            List<CardData> allCardList = new List<CardData>(tempAllCardList.Count);
            for (int i = 0; i < tempAllCardList.Count; i++)
            {
                allCardList.Add((CardData)tempAllCardList[i]);
            }
            while (allCardList.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, allCardList.Count);
                if (allCardList[index].IsShow)
                {
                    AllCardQueue.Enqueue(allCardList[index]);
                }
                allCardList.RemoveAt(index);
            }
            DebugHelper.DebugLog("AllCardQueue.count==="+ AllCardQueue.Count);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Dispose()
        {
            base.Dispose();
            AllCardQueue.Clear();
            AllCardQueue = null;
        }
    }
}
