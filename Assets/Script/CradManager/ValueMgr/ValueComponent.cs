using Assets.Script.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script.CradManager
{
    public class ValueComponent : BaseCommpoent
    {
        public int HpValue;
        public int MinHp = 0;
        public bool SetCurrentHpValue(int changeValue)
        {
            HpValue -= changeValue;
            return CheckDeath(HpValue, MinHp);
        }

        public bool CheckDeath(int currentValue,int minHp)
        {
            return currentValue <= minHp;
        }
    }
}
