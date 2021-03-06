﻿
//功能：动态加载预制件
//创建者: 胡海辉
//创建时间：


using Assets.Script.Base;
using System;
using UnityEngine;

namespace Assets.Script
{
    public class DynamicPrefabMgr : TSingleton<DynamicPrefabMgr>, IDisposable
    {

        public T GetPrefab<T>(string pathStr, Transform trans) where T : class
        {
            GameObject go = GetPrefab(pathStr, trans) as GameObject;
            T fuc = go.transform.GetComponent<T>();

            if (fuc == null)
            {
                Debug.LogError("don't find prefab at " + pathStr);
            }
            return fuc;
        }

        public GameObject GetPrefab(string pathStr, Transform trans)
        {
            GameObject goPrefab = Resources.Load(pathStr) as GameObject;
            if (goPrefab == null)
            {
                DebugHelper.DebugLogError("goPrefab path is vaid ===  " + pathStr);
                return null;
            }
            Transform transChild = trans.FindChild(goPrefab.name);
            if (transChild == null)
            {
                GameObject go = MonoBehaviour.Instantiate<GameObject>(goPrefab);
                transChild = go.transform;
                transChild.parent = trans;
                //transChild.localPosition = Vector3.zero;
                transChild.name = goPrefab.name;
            }
            return transChild.gameObject;
        }

        public T GetPrefab<T>(string path)
        {
            GameObject goPrefab = Resources.Load(path) as GameObject;
            T fuc = goPrefab.GetComponent<T>();

            if (fuc == null)
            {
                DebugHelper.DebugLogErrorFormat("{0} is not find", path);
            }
            return fuc;
        }

        public T InstantiatePrefab<T>(GameObject obj, Transform parentTrans, Vector3 initPos)
        {
            GameObject go = MonoBehaviour.Instantiate<GameObject>(obj);
            if (go == null) return default(T);
            go.transform.parent = parentTrans;
            go.transform.localPosition = initPos;
            return go.GetComponent<T>();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
