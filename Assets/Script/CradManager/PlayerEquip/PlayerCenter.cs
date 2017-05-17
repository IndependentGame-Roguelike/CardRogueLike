
using Assets.Script.Base;
using Assets.Script.Tools;

namespace Assets.Script.CradManager
{
    public class PlayerCenter : PlayerEquipBase
    {

        private ValueComponent playerValue;

        public override void InitComponent()
        {
            base.InitComponent();
            playerValue = new ValueComponent();
            playerValue.SetMonoCreator(this);
        }

        public override void InitData()
        {
            EquipSpaceType = EquipSpaceTypeEnum.PlayerPos;
            base.InitData();
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
            if (CanMoveCard == false)
            {
                return;
            }
            MoveCollisionCard();
            if (Actor.mActorType == ActorTypeEnum.MonsterCard)
            {
                playerValue.SetCurrentHpValue(Actor.CardValue);
            }

        }
    }
}
