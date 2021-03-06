﻿
//功能：出发提示管理
//创建者: 胡海辉
//创建时间：
using UnityEngine;
using Assets.Script.EventMgr;
using System;
using System.Collections.Generic;

public enum TiggerType
{
    none,

}


public class TiggerTipsMgr : MonoBehaviour
{
    private SpriteRenderer commTiggerTips;
    private Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        InitCompenont();
        InitListener();
        InitData();

    }

    /// <summary>
    /// 初始化组件
    /// </summary>
    private void InitCompenont()
    {
        Transform trans = transform.FindChild("tips");
        if (trans != null)
        {
            commTiggerTips = trans.GetComponent<SpriteRenderer>();
        }

    }

    private void InitData()
    {
        spriteDic.Clear();
        string path = "UItexture/Tips";
        object[] loadSprite = Resources.LoadAll(path, typeof(Sprite));
        for (int i = 0; i < loadSprite.Length; i++)
        {
            Sprite mSprite = (Sprite)loadSprite[i];
            spriteDic.Add(mSprite.name, mSprite);
        }
    }

    /// <summary>
    /// 初始化监听
    /// </summary>
    private void InitListener()
    {
        //EventManager.GetInstance().AddListener(EventDefine.TiggerType, TiggerChangeType);
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    private void RemoveListener()
    {
        //EventManager.GetInstance().RemoveListener(EventDefine.TiggerType, TiggerChangeType);
    }

    private void TiggerChangeType(object obj, EventArgs e)
    {
        //TiggerTypeParam param = (TiggerTypeParam)obj;
    }

    /// <summary>
    /// 设置提示图标
    /// </summary>
    /// <param name="mTiggerType"></param>
    public void SetSprite(TiggerType mTiggerType)
    {
        if (mTiggerType == TiggerType.none || (int)mTiggerType > 100) return;

        string spriteName = "";
        switch (mTiggerType)
        {
            default:
                spriteName = "";
                break;
        }
        if (spriteDic.ContainsKey(spriteName))
        {
            commTiggerTips.sprite = spriteDic[spriteName];
        }
    }


    private void OnDestroy()
    {
        RemoveListener();
    }



}
