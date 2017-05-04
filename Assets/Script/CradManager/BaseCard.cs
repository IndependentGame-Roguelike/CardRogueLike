using Assets.Script.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.Tools;
using UnityEngine;

namespace Assets.Script.CradManager
{
    public class BaseCard : BaseCreator
    {
        public override ActorTypeEnum mActorType
        {
            get
            {
                return ActorTypeEnum.MonsterCard;
            }
        }

        public Transform m_ParentTrans;

        private bool m_CanMove;
        private Vector2 m_MoveTargetPos;
        private float m_MoveNeedTime;
        private bool b_ReleaseFinger;

        public override void InitData()
        {
            base.InitData();
            m_CanMove = false;
            m_MoveNeedTime = 0;
            //m_ParentTrans = CacheTrans.parent;
            b_ReleaseFinger = true;
        }

        public override void InitComponent()
        {
            base.InitComponent();
        }

        public override void Update()
        {
            base.Update();
            if (m_CanMove)
            {
                if ((m_MoveNeedTime -= Time.smoothDeltaTime) <= 0)
                {
                    m_CanMove = false;
                }
                Vector2.Lerp(CacheTrans.position, m_MoveTargetPos, m_MoveNeedTime);
            }
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

        public void OnTriggerEnter2D(Collider other)
        {
            OnTriggerState(other.transform, ColliderStateEnum.Enter);
        }

        public void OnTriggerStay2D(Collider other)
        {
            OnTriggerState(other.transform, ColliderStateEnum.Stay);
        }

        public void OnTriggerExit2D(Collider other)
        {
            OnTriggerState(other.transform, ColliderStateEnum.Exit);
        }

        public void ReleaseFinger()
        {
            b_ReleaseFinger = true;
        }

        public void PressFinger()
        {
            b_ReleaseFinger = false;
        }

        public void MoveCard(Vector2 targetPos, float moveTime)
        {
            m_CanMove = true;
            m_MoveNeedTime = moveTime;
            m_MoveTargetPos = targetPos;
        }

        private void OnTriggerState(Transform trans, ColliderStateEnum colliderstate)
        {
            BaseCreator creator = trans.GetComponent<BaseCreator>();
            if (creator == null)
            {
                return;
            }

            if (b_ReleaseFinger == false)
            {
                return;
            }

            if (creator.mActorType != mActorType)
            {
                creator.SetBaseCreator(this);
                creator.LogicCollision(this, colliderstate);
            }
        }
    }
}
