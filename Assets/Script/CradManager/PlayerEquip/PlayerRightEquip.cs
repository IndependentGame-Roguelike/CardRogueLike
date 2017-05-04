
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
    }
}
