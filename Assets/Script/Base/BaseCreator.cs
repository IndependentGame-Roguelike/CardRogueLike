
//功能：
//创建者: 胡海辉
//创建时间：
using Assets.Script.Tools;
using UnityEngine;

namespace Assets.Script.Base
{
    public abstract class BaseCreator : BaseMonoBehaviour
    {
        [HideInInspector]
        public BaseCreator mCreator;
        [HideInInspector]
        public int ObjId = 0;
        public virtual ActorTypeEnum mActorType
        {
            get
            {
                return 0;
            }
        }
        [HideInInspector]
        public EquipSpaceTypeEnum EquipSpaceType;

        public override void OnDestroy()
        {
            base.OnDestroy();
            Dispose();
        }

        public virtual void SetBaseCreator(BaseCreator creator)
        {
            mCreator = creator;
        }
        public abstract void LogicCollision(BaseCreator creator, ColliderStateEnum colliderState);

        public abstract void PlayGameSound(SoundEnum soundType);

        public abstract void Dispose();
    }
}
