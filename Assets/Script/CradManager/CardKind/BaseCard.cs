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
                return 0;
            }
        }
        //装备在玩家身上后的属性枚举
        public EquipSpaceTypeEnum EquipSpaceType
        {
            get;
            protected set;
        }

        public Transform m_ParentTrans
        {
            get;
            protected set;
        }

        private bool m_IsDeath;
        public bool IsDeath
        {
            get
            {
                return m_IsDeath;
            }
            set
            {
                //抛死亡消息
                if (value)
                {
                    m_IsDeath = value;
                }
            }
        }

        //卡片数值
        [HideInInspector]
        public ValueComponent CardValue;
        [HideInInspector]
        public bool UseCardAfterDestroy;
        [HideInInspector]
        public BaseCard TargetCard;
        [HideInInspector]
        public BaseCreator TargetCreator;

        private bool m_IsSelectThisCard;
        private bool m_CanMove;
        private Vector2 m_MoveTargetPos;
        private float m_MoveNeedTime;
        private float m_CanMoveTime;
        private const float c_ExtraMoveTime = 0.2f;
        private bool b_ReleaseFinger;

        public override void InitData()
        {
            base.InitData();
            m_CanMove = false;
            m_IsSelectThisCard = false;
            m_MoveNeedTime = 0;
            m_CanMoveTime = 0;
            //m_ParentTrans = CacheTrans.parent;
            b_ReleaseFinger = true;
            EquipSpaceType = EquipSpaceTypeEnum.None;
            CardValue = new ValueComponent();
            CardValue.SetMonoCreator(this);
            ObjId = StaticMemberMgr.CurrentObjId++;
        }

        public override void InitComponent()
        {
            base.InitComponent();
        }

        public override void InitListener()
        {
            base.InitListener();
            EventManager.instance.AddListener<BaseCard>(EventDefine.HpValueChange, HpValueChange);
        }

        public override void RemoveListener()
        {
            base.RemoveListener();
            EventManager.instance.RemoveListener<BaseCard>(EventDefine.HpValueChange, HpValueChange);
        }
        public override void Update()
        {
            base.Update();
            if (CardValue != null)
            {
                CardValue.Update(Time.smoothDeltaTime);
            }
            if (m_CanMove)
            {
                if ((m_CanMoveTime -= Time.smoothDeltaTime) <= 0)
                {
                    m_CanMove = false;
                }
                CacheTrans.position = Vector2.Lerp(CacheTrans.position, m_MoveTargetPos, m_MoveNeedTime);
            }
        }

        public override void SetBaseCreator(BaseCreator creator)
        {
            base.SetBaseCreator(creator);
        }

        public override void Dispose()
        {
            TargetCreator = null;
            TargetCard = null;
            if (CardValue != null)
            {
                CardValue.Dispose();
            }
            CardValue = null;
        }

        public override void LogicCollision(BaseCreator creator, ColliderStateEnum colliderState)
        {
            TargetCard = creator as BaseCard;
        }

        public override void PlayGameSound(SoundEnum soundType)
        {
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerState(other.transform, ColliderStateEnum.Enter);
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerState(other.transform, ColliderStateEnum.Stay);
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerState(other.transform, ColliderStateEnum.Exit);
        }

        public void ReleaseFinger(int objId)
        {
            b_ReleaseFinger = true;
            m_IsSelectThisCard = (ObjId == objId);
        }

        public void PressFinger()
        {
            b_ReleaseFinger = false;
        }

        public void MoveCard(Vector2 targetPos, float moveTime, EquipSpaceTypeEnum equipSpaceType)
        {
            if (m_CanMove)
            {
                return;
            }
            m_MoveNeedTime = moveTime;
            m_CanMoveTime = moveTime + c_ExtraMoveTime;
            m_MoveTargetPos = targetPos;
            m_CanMove = true;
            EquipSpaceType = equipSpaceType;
        }

        public virtual bool OnTriggerState(Transform trans, ColliderStateEnum colliderstate)
        {
            TargetCreator = trans.GetComponent<BaseCreator>();
            if (TargetCreator == null)
            {
                return false;
            }

            if (b_ReleaseFinger == false)
            {
                return false;
            }

            if (m_IsSelectThisCard == false)
            {
                return false;
            }

            if (TargetCreator.mActorType == mActorType)
            {
                return false;

            }
            return true;
        }

        /// <summary>
        /// 血量变化
        /// </summary>
        /// <param name="changeCard"></param>
        /// <param name="e"></param>
        private void HpValueChange(ref BaseCard changeCard)
        {
            if (changeCard.ObjId == ObjId)
            {
                //销毁
                Destroy(this);
            }

        }

    }
}
