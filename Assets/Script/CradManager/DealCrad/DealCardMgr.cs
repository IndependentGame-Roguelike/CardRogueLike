using System.Collections.Generic;
using Assets.Script.Base;
using Assets.Script.Tools;
using System;

namespace Assets.Script.CradManager
{
    public class DealCardMgr : TSingleton<DealCardMgr>, IDisposable
    {
        public Queue<CradInfo> AllCardQueue;
        public override void Init()
        {
            base.Init();
            AllCardQueue = new Queue<CradInfo>(StaticMemberMgr.MAX_CARD_COUNT);
        }

        public void InitData()
        {
            //读取卡片信息
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
