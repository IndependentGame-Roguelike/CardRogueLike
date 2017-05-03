using Assets.Script.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.Tools;

namespace Assets.Script.CradManager
{
    public class BaseCard : BaseCreator
    {

        public override void InitData()
        {
            base.InitData();
        }

        public override void InitComponent()
        {
            base.InitComponent();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void SetBaseCreator(BaseCreator creator)
        {
            base.SetBaseCreator(creator);
        }

        public override void Dispose()
        {
        }

        public override void LogicCollision(BaseCreator creator, ColliderStateEnum colliderState)
        {
        }

        public override void PlayGameSound(SoundEnum soundType)
        {
        }
    }
}
