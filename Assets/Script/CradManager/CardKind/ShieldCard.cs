using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.Base;
using Assets.Script.Tools;
using UnityEngine;

namespace Assets.Script.CradManager
{
    public class ShieldCard : BaseCard
    {
        public override ActorTypeEnum mActorType
        {
            get
            {
                return ActorTypeEnum.ShieldCard;
            }
        }

        public override void InitComponent()
        {
            base.InitComponent();
        }

        public override void InitData()
        {
            base.InitData();
        }

        /// <summary>
        /// 其他卡来碰我
        /// </summary>
        /// <param name="creator">其他的card</param>
        /// <param name="colliderState"></param>
        public override void LogicCollision(BaseCreator creator, ColliderStateEnum colliderState)
        {
            base.LogicCollision(creator, colliderState);
            if (TargetCard == null)
            {
                return;
            }
            if (TargetCard.mActorType == ActorTypeEnum.MonsterCard)
            {
                IsDeath = CardValue.SetCurrentHpValue(TargetCard.CardValue);
            }
        }

        /// <summary>
        /// 我碰到的东西
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="colliderstate"></param>
        /// <returns></returns>
        public override bool OnTriggerState(Transform trans, ColliderStateEnum colliderstate)
        {
            if (base.OnTriggerState(trans, colliderstate) == false)
            {
                return false;
            }

            if (TargetCreator.EquipSpaceType != EquipSpaceTypeEnum.None || TargetCreator.mActorType == ActorTypeEnum.MonsterCard)
            {
                TargetCreator.SetBaseCreator(this);
                TargetCreator.LogicCollision(this, colliderstate);
                return true;
            }
            return false;
        }
    }
}
