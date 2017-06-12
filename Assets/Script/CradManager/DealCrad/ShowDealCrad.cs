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
        private List<BaseCard> BaseCardPool;
        private const int MAX_SHOW_CARD_COUNT = 4;
        private const int MIN_show_CARD_COUNT = 1;
        private BaseCard tempBaseCard;
        private Queue<CardData> AllCardQueue;
        public override void InitData()
        {
            base.InitData();
            CurrentShowCradCount = 0;
            m_IsStart = false;
            CurrentShowCradArr = new BaseCard[MAX_SHOW_CARD_COUNT];
            BaseCardPool= new List<BaseCard>(10);
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
                        CurrentShowCradArr[i] = GetBaseCardInfo(AllCardQueue.Dequeue(), i);
                        CurrentShowCradArr[i].gameObject.CustomSetActive(true);
                        CurrentShowCradCount++;
                    }
                }
            }
        }

        public void RemoveCard(int slotIndex)
        {
            if (slotIndex >= CurrentShowCradArr.Length)
            {
                DebugHelper.DebugLogError("ShowDealCrad --- RemoveCard is error slotIndex ==  " + slotIndex);
                return;
            }
            CurrentShowCradCount--;
            PushBaseCardByPool(CurrentShowCradArr[slotIndex]);
            CurrentShowCradArr[slotIndex].gameObject.CustomSetActive(false);
        }

        private BaseCard GetBaseCardInfo(CardData mCardData, int index)
        {
            if (mCardData == null)
            {
                return null;
            }

            //从缓存池里取已经构建好的component

            tempBaseCard = popBaseCardByPool();

            return tempBaseCard;
        }

        private BaseCard popBaseCardByPool()
        {
            BaseCard tempCard;
            if (BaseCardPool.Count < 0)
            {
                tempCard = new BaseCard();
            }
            else
            {
                tempCard = BaseCardPool[0];
                BaseCardPool.RemoveAt(0);
            }
            return tempCard;
        }

        private void PushBaseCardByPool(BaseCard tempCard)
        {
            tempCard.Dispose();
            BaseCardPool.Add(tempCard);
        }
    }
}
