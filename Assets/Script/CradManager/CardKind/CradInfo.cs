using System;
using Assets.Script.Tools;
using UnityEngine;

namespace Assets.Script.CradManager
{
    [Serializable]
    public class CradInfo
    {
        [HideInInspector]
        public BaseCard CardData;
        public Transform RootTrans;
    }
}
