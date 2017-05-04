
using Assets.Script.Tools;

namespace Assets.Script.CradManager
{
    public class PlayerPackage : PlayerEquipBase
    {
        public override EquipSpaceTypeEnum EquipSpaceType
        {
            get
            {
                return EquipSpaceTypeEnum.Package;
            }
        }
    }
}
