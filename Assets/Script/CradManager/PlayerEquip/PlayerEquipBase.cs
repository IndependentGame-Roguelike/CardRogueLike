
using System;
using Assets.Script.Base;
using Assets.Script.Tools;
using UnityEngine;

namespace Assets.Script.CradManager
{
    public class PlayerEquipBase : BaseCreator
    {

        public override ActorTypeEnum mActorType
        {
            get
            {
                return ActorTypeEnum.PlayerEquip;
            }
        }

        public BaseCard Actor
        {
            get;
            private set;
        }

        public override EquipSpaceTypeEnum EquipSpaceType
        {
            get
            {
                return 0;
            }
        }

        public BoxCollider2D mCollider;
        public bool CanMoveCard;
        public bool HaveCard;

        public const float c_CardMoveTime = 0.2f;

        public override void InitComponent()
        {
            base.InitComponent();
            mCollider = GetComponent<BoxCollider2D>();
        }

        public override void InitData()
        {
            base.InitData();
        }

        public override void Dispose()
        {
            Actor = null;
        }

        public override void LogicCollision(BaseCreator creator, ColliderStateEnum colliderState)
        {
            Actor = creator as BaseCard;
            CanMoveCard = true;
        }

        public override void PlayGameSound(SoundEnum soundType)
        {
        }

        public void MoveCollisionCard()
        {
            if (CanMoveCard)
            {
                Actor.MoveCard(CacheTrans.position, c_CardMoveTime, EquipSpaceType);
                HaveCard = true;
                CanMoveCard = false;
                if (mCollider) mCollider.enabled = false;
            }
        }

        public void RestData()
        {
            if(mCollider) mCollider.enabled = false;
            HaveCard = false;
        }
    }
}
