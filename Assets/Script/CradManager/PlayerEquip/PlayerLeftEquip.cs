
using Assets.Script.Tools;

namespace Assets.Script.CradManager
{
    public class PlayerLeftEquip : PlayerEquipBase
    {
        public override EquipSpaceTypeEnum EquipSpaceType
        {
            get
            {
                return EquipSpaceTypeEnum.LeftEquip;
            }
        }
    }
}
