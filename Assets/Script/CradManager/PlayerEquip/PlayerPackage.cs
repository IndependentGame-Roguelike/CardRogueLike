
using Assets.Script.Base;
using Assets.Script.Tools;

namespace Assets.Script.CradManager
{
    public class PlayerPackage : PlayerEquipBase
    {

        public override void InitComponent()
        {
            base.InitComponent();
        }

        public override void InitData()
        {
            EquipSpaceType = EquipSpaceTypeEnum.Package;
            base.InitData();
        }

        public override void LogicCollision(BaseCreator creator, ColliderStateEnum colliderState)
        {
            base.LogicCollision(creator, colliderState);
            if (Actor == null)
            {
                return;
            }

            if (HaveCard ||
                Actor.mActorType == ActorTypeEnum.MonsterCard ||
                Actor.EquipSpaceType == EquipSpaceTypeEnum.LeftEquip || 
                Actor.EquipSpaceType == EquipSpaceTypeEnum.RightEquip)
            {
                CanMoveCard = false;
            }
        }
    }
}
