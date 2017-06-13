using Assets.Script.Base;
using Assets.Script.EventMgr;
using System.Collections.Generic;
using Assets.Script.Tools;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Assets.Script.CradManager
{
    public class ShowDealCrad : BaseMonoBehaviour
    {
        public int CurrentShowCradCount;
        private bool m_IsStart;
        private CradInfo[] CurrentShowCradArr;
        private Dictionary<ActorTypeEnum, List<BaseCard>> BaseCardPool;
        private const int MAX_SHOW_CARD_COUNT = 4;
        private const int MIN_show_CARD_COUNT = 1;
        private BaseCard tempBaseCard;
        private Queue<CardData> AllCardQueue;
        public override void InitData()
        {
            base.InitData();
            CurrentShowCradCount = 0;
            m_IsStart = false;
            CurrentShowCradArr = new CradInfo[MAX_SHOW_CARD_COUNT];
            BaseCardPool = new Dictionary<ActorTypeEnum, List<BaseCard>>(10);
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
                        CurrentShowCradArr[i].CardData = GetBaseCardInfo(AllCardQueue.Dequeue(), i);
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
            PushBaseCardByPool(CurrentShowCradArr[slotIndex].CardData.mActorType, CurrentShowCradArr[slotIndex].CardData);
        }

        private BaseCard GetBaseCardInfo(CardData mCardData, int index)
        {
            if (mCardData == null)
            {
                return null;
            }

            //从缓存池里取已经构建好的component
            tempBaseCard = PopBaseCardByPool(mCardData.ActorType);
            tempBaseCard.CardValue.HpValue = mCardData.HpValue;
            tempBaseCard.ConfigId = mCardData.ConfigId;
            tempBaseCard.PokerType = mCardData.PokerType;
            tempBaseCard.SpritePathName = mCardData.SpritePath;
            tempBaseCard.CacheTrans.parent = CurrentShowCradArr[index].RootTrans;
            tempBaseCard.Init();
            return tempBaseCard;
        }

        private BaseCard PopBaseCardByPool(ActorTypeEnum actorType)
        {
            BaseCard tempCard;
            List<BaseCard> tempBaseCardList;
            if (BaseCardPool.TryGetValue(actorType, out tempBaseCardList))
            {
                tempCard = tempBaseCardList[0];
            }
            else
            {
                tempBaseCardList = new List<BaseCard>();
                tempCard = CreateBaseCardByActorType(actorType);
                tempBaseCardList.Add(tempCard);
                BaseCardPool.Add(actorType, tempBaseCardList);
            }

            BaseCardPool[actorType].RemoveAt(0);
            return tempCard;
        }

        private void PushBaseCardByPool(ActorTypeEnum actorType, BaseCard tempCard)
        {
            tempCard.Dispose();
            List<BaseCard> tempBaseCardList;
            if (BaseCardPool.TryGetValue(actorType, out tempBaseCardList))
            {
                tempBaseCardList.Add(tempCard);
            }
            else
            {
                tempBaseCardList = new List<BaseCard> { tempCard };
                BaseCardPool.Add(actorType, tempBaseCardList);
            }
        }

        private BaseCard CreateBaseCardByActorType(ActorTypeEnum actorType)
        {
            GameObject obj = InstanceCardObj(actorType);
            switch (actorType)
            {
                case ActorTypeEnum.CoinCard:
                    return obj.AddComponent<CoinCard>();
                case ActorTypeEnum.HealCard:
                    return obj.AddComponent<HealBloodCard>();
                case ActorTypeEnum.MonsterCard:
                    return obj.AddComponent<MonsterCard>();
                case ActorTypeEnum.ShieldCard:
                    return obj.AddComponent<ShieldCard>();
                case ActorTypeEnum.WeaponCard:
                    return obj.AddComponent<WeaponCard>();
                default:
                    return null;
            }
        }

        private GameObject InstanceCardObj(ActorTypeEnum actorType)
        {
            return DynamicPrefabMgr.instance.GetPrefab<GameObject>(string.Format(StaticMemberMgr.CARD_PREFAB_PATH, actorType), StaticMemberMgr.HideRootTrans);
        }
    }
}
