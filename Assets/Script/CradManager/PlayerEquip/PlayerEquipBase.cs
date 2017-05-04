
using System;
using Assets.Script.Base;
using Assets.Script.Tools;

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

        public virtual EquipSpaceTypeEnum EquipSpaceType
        {
            get
            {
                return EquipSpaceTypeEnum.PlayerPos;
            }
        }
        public override void Dispose()
        {
            Actor = null;
        }

        public override void LogicCollision(BaseCreator creator, ColliderStateEnum colliderState)
        {
            if (Actor == null)
            {
                Actor = creator as BaseCard;
                Actor.MoveCard(CacheTrans.position, 0.2f);
            }
        }

        public override void PlayGameSound(SoundEnum soundType)
        {
        }
    }
}
