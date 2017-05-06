
using Assets.Script.Base;
using Assets.Script.Tools;

namespace Assets.Script.CradManager
{
    public class PlayerRightEquip : PlayerEquipBase
    {
        public override EquipSpaceTypeEnum EquipSpaceType
        {
            get
            {
                return EquipSpaceTypeEnum.RightEquip;
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

        public override void LogicCollision(BaseCreator creator, ColliderStateEnum colliderState)
        {
            base.LogicCollision(creator, colliderState);
            if (Actor == null)
            {
                return;
            }
           
            if (HaveCard == false && Actor.mActorType == ActorTypeEnum.MonsterCard)
            {
                CanMoveCard = false;
            }
            MoveCollisionCard();
        }
    }
}
