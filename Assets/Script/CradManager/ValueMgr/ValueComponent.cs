using Assets.Script.Base;

namespace Assets.Script.CradManager
{
    public class ValueComponent : BaseCommpoent
    {
        private int m_HpValue;
        public int HpValue
        {
            get
            {
                return m_HpValue;
            }
            set
            {
                if (m_HpValue != value)
                {
                    EventManager.instance.RasieEvent<BaseCreator>(EventDefine.HpValueChange,ref mMonoCreator);
                    m_HpValue = value;
                }
            }
        }
        public int MinHp = 0;
        private int lastHpValue = 0;
        public bool SetCurrentHpValue(ValueComponent targetValueComponent)
        {
            lastHpValue = m_HpValue;
            HpValue -= targetValueComponent.HpValue;
            targetValueComponent.HpValue -= lastHpValue;
            return CheckDeath(HpValue, MinHp);
        }

        public bool AddTargetValue(ValueComponent targetValueComponent)
        {
            lastHpValue = m_HpValue;
            HpValue += targetValueComponent.HpValue;
            targetValueComponent.HpValue -= lastHpValue;
            return CheckDeath(HpValue, MinHp);
        }

        public bool ChangeTargetValue(ref int targetValue)
        {
            lastHpValue = m_HpValue;
            HpValue -= targetValue;
            targetValue += lastHpValue;
            return CheckDeath(HpValue, MinHp);
        }

        public bool CheckDeath(int currentValue,int minHp)
        {
            return currentValue <= minHp;
        }
    }
}
