
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
    }
}
