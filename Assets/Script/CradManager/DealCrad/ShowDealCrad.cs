using Assets.Script.Base;
using Assets.Script.EventMgr;
using System.Collections.Generic;

namespace Assets.Script.CradManager
{
    public class ShowDealCrad : BaseMonoBehaviour
    {
        public int CurrentShowCradCount;
        private bool m_IsStart;
        private BaseCard[] CurrentShowCradArr;
        private const int MAX_SHOW_CARD_COUNT = 4;
        private const int MIN_show_CARD_COUNT = 1;
        private BaseCard tempBaseCard;
        private Queue<CradInfo> AllCardQueue;
        public override void InitData()
        {
            base.InitData();
            CurrentShowCradCount = 0;
            m_IsStart = false;
            CurrentShowCradArr = new BaseCard[MAX_SHOW_CARD_COUNT];
        }

        public override void Update()
        {
            base.Update();
            if (CurrentShowCradCount <= MIN_show_CARD_COUNT)
            {
                AllCardQueue = DealCardMgr.instance.AllCardQueue;
                for (int i = 0; i < MAX_SHOW_CARD_COUNT; i++)
                {
                    if (CurrentShowCradArr[i] == null)
                    {
                        CurrentShowCradArr[i] = GetBaseCardInfo(AllCardQueue.Dequeue());
                        CurrentShowCradCount++;
                    }
                }
            }
        }

        private BaseCard GetBaseCardInfo(CradInfo mCradInfo)
        {
            if (mCradInfo == null)
            {
                return null;
            }

            //从缓存池里取已经构建好的component

            //tempBaseCard = new BaseCard();

            return tempBaseCard;
        }

    }
}
