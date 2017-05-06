
using Assets.Script.Base;
using Assets.Script.Tools;

namespace Assets.Script.CradManager
{
    public class PlayerCenter : PlayerEquipBase
    {
        public override EquipSpaceTypeEnum EquipSpaceType
        {
            get
            {
                return EquipSpaceTypeEnum.PlayerPos;
            }
        }

        public override void LogicCollision(BaseCreator creator, ColliderStateEnum colliderState)
        {
            base.LogicCollision(creator, colliderState);
            if (Actor == null)
            {
                return;
            }
            CanMoveCard = false;
            if (Actor.EquipSpaceType == EquipSpaceTypeEnum.LeftEquip ||
                Actor.EquipSpaceType == EquipSpaceTypeEnum.RightEquip ||
                Actor.mActorType == ActorTypeEnum.MonsterCard)
            {
                CanMoveCard = true;
            }
            MoveCollisionCard();
        }
    }
}
