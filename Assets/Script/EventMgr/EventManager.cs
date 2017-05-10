using UnityEngine;
using System.Collections;
using System;
using Assets.Script.Base;
using UnityEngine.Events;
using System.Collections.Generic;
using Assets.Script.EventMgr;

public enum EventDefine
{
   HpValueChange, //血量变化
    EventMax,
}


public class EventManager : TSingleton<EventManager>, IDisposable
{
    private Delegate[] mEventArr;

    public EventManager()
    {
        mEventArr = new Delegate[(int)EventDefine.EventMax];
    }

    public void AddListener(EventDefine eventID, TDelegate eventHadle)
    {
        int index = (int)eventID;
        if (CheckValid(eventID, eventHadle))
        {
            mEventArr[index] = Delegate.Combine((TDelegate)mEventArr[index], eventHadle);
        }
    }

    public void RemoveListener(EventDefine eventID, TDelegate eventHadle)
    {
        int index = (int)eventID;
        if (CheckValid(eventID, eventHadle))
        {
            mEventArr[index] = Delegate.Remove((TDelegate)mEventArr[index], eventHadle);
        }
    }

    public void RasieEvent(EventDefine eventID)
    {
        int index = (int)eventID;
        if (index > mEventArr.Length)
        {
            DebugHelper.DebugLogError("EventDefine error " + eventID);
            return;
        }

        TDelegate tDelegate = mEventArr[index] as TDelegate;
        if (tDelegate !=null)
        {
            tDelegate.Invoke();
        }
    }


    public void AddListener<TParam>(EventDefine eventID, TDelegate<TParam> eventHadle)
    {
        int index = (int)eventID;
        if (CheckValid(eventID, eventHadle))
        {
            mEventArr[index] = Delegate.Combine((TDelegate<TParam>)mEventArr[index], eventHadle);
        }
    }

    public void RemoveListener<TParam>(EventDefine eventID, TDelegate<TParam> eventHadle)
    {
        int index = (int)eventID;
        if (CheckValid(eventID, eventHadle))
        {
            mEventArr[index] = Delegate.Remove((TDelegate<TParam>)mEventArr[index], eventHadle);
        }
    }

    public void RasieEvent<TParam>(EventDefine eventID, ref TParam _param)
    {
        int index = (int)eventID;
        if (index > mEventArr.Length)
        {
            DebugHelper.DebugLogError("EventDefine error " + eventID);
            return;
        }

        TDelegate<TParam> tDelegate = mEventArr[index] as TDelegate<TParam>;
        if (tDelegate != null)
        {
            tDelegate.Invoke(ref _param);
        }
    }

    public void RemoveAllListener()
    {
        if (mEventArr != null)
        {
            for (int i = 0; i < mEventArr.Length; i++)
            {
                Delegate delegator = mEventArr[i];
                if (delegator != null)
                {
                    Delegate[] funcs = delegator.GetInvocationList();
                    for (int jj = 0; jj < funcs.Length; ++jj)
                    {
                        delegator = Delegate.Remove(delegator, funcs[jj]);
                    }
                }
            }
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        RemoveAllListener();
        mEventArr = null;
    }

    private bool CheckValid(EventDefine evt, Delegate handler)
    {
        Delegate mDelegate = mEventArr[(int)evt];
        return mDelegate == null || mDelegate.GetType() == handler.GetType();
    }
}
