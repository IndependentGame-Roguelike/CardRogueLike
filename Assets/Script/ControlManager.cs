using UnityEngine;
using System;
using Assets.Script.Base;
using Assets.Script.Tools;
using Assets.Script.CradManager;

public class ControlManager : TSingleton<ControlManager>, IDisposable
{

    public bool CanControl;
    public bool IsTouch;
    #region private
    private Camera cam;
    private BaseCard mSelectCard;
    private int mTouchLayerIndex;
    private float mMaxMoveHeight;
    private RaycastHit2D mHit;
    private Ray mRay;
    #endregion

    public override void Init()
    {
        base.Init();
        InitComponent();
        InitData();
        InitListener();
    }
    public override void Dispose()
    {
        base.Dispose();
        mSelectCard = null;
        RemoveListener();
    }

    public void InitComponent()
    {
        if (Application.platform == RuntimePlatform.Android
            || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Input.multiTouchEnabled = false;
        }
    }

    public void InitData()
    {
        CanControl = true;
        IsTouch = false;
        mTouchLayerIndex = StaticMemberMgr.CAN_MOVE_LAYER;
        mMaxMoveHeight = 1;
    }

    /// <summary>
    /// 初始化监听
    /// </summary>
    public void InitListener()
    {
    }
    /// <summary>
    /// 移除监听
    /// </summary>
    public void RemoveListener()
    {
    }

    public void InitCamera(Camera mainCamera)
    {
        cam = mainCamera;
    }

    public override void Update(float time)
    {
        base.Update(time);
        if (CanControl == false)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            mSelectCard = HitTrash(PlatformTools.m_TouchPosition);
            if (mSelectCard)
            {
                IsTouch = true;
                mSelectCard.PressFinger();
                //mSelectTrash.PickUpTrash();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (mSelectCard)
            {
                IsTouch = false;
                mSelectCard.ReleaseFinger(mSelectCard.ObjId);
                //mSelectTrash.ReleaseTrash();
                mSelectCard = null;

            }
        }

        if (Input.GetMouseButton(0))
        {
            MouseMove(PlatformTools.m_TouchPosition);
        }
    }


    #region fuc

    /// <summary>
    /// 鼠标点击移动目标
    /// </summary>
    private void MouseMove(Vector3 pos)
    {
        Vector3 newPosition = Vector3.zero;
        if (mSelectCard != null)
        {
            newPosition = Camera.main.WorldToScreenPoint(mSelectCard.CacheTrans.position);
            pos.z = newPosition.z;
            newPosition = cam.ScreenToWorldPoint(pos);
            //newPosition.y = Mathf.Min(cam.ScreenToWorldPoint(pos).y, mMaxMoveHeight);
            //newPosition.z = mSelectCard.CacheTrans.position.z;
            //if (GameHelper.instance.InTheArea(newPosition) && GameHelper.instance.UnderGround(newPosition, 0) == false)
            {
                mSelectCard.CacheTrans.position = newPosition;
            }
        }
    }

    private BaseCard HitTrash(Vector3 pos)
    {
      
        mRay = cam.ScreenPointToRay(pos);
        mHit = Physics2D.Raycast(mRay.origin, mRay.direction, 100, 1 << mTouchLayerIndex);
        if (mHit)
        {
            return mHit.transform.GetComponent<BaseCard>();
        }
        else
        {
            return null;
        }
    }
    #endregion
}
