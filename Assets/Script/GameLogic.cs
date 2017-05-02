
//功能： 游戏总控制
//创建者: 胡海辉
//创建时间：


using Assets.Script.AudioMgr;
using Assets.Script.Base;
using Assets.Script.Tools;
using UnityEngine;

namespace Assets.Script
{
    public class GameLogic : BaseMonoBehaviour
    {
        public float audioTime = 0.5f;
        public Camera MainCamera;
        private GameStateEnum mCurrentState;
        private Transform mRootContainerTrans;
        private Transform mRootTrashTrans;

        public override void Awake()
        {
            //DebugHelper.bEnableDebug = true;
            base.Awake();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            AudioControl.CreateInstance();
            GameHelper.CreateInstance();
            ControlManager.CreateInstance();
            EventManager.CreateInstance();
            base.Init();
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        public override void InitComponent()
        {
            base.InitComponent();
            ControlManager.instance.InitCamera(MainCamera);
            GameHelper.instance.InitCamera(MainCamera);
            string path = StaticMemberMgr.SCENE_CONTAINERS_PATH;
            GameHelper.instance.GetTransformByPath(ref mRootContainerTrans, path);
            path = StaticMemberMgr.SCENE_TRASH_PATH;
            GameHelper.instance.GetTransformByPath(ref mRootTrashTrans, path);
        }

        /// <summary>
        /// 初始化监听
        /// </summary>
        public override void InitListener()
        {
            base.InitListener();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public override void InitData()
        {
            base.InitData();
            mCurrentState = GameStateEnum.Intro;
            //mGamestate = SwitchState(mCurrentState);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        public override void RemoveListener()
        {
            base.RemoveListener();
        }

        public override void Update()
        {
            base.Update();
            if (AudioControl.HasInstance())
            {
                AudioControl.instance.Update(audioTime);
            }
            if (ControlManager.HasInstance())
            {
                ControlManager.instance.Update(Time.deltaTime);
            }
            //if (mGamestate != null)
            //{
            //    if (mCurrentState < mGamestate.NextState)
            //    {
            //        mCurrentState = mGamestate.NextState;
            //        mGamestate.Dispose();
            //        mGamestate = SwitchState(mCurrentState);
            //        mGamestate.Init(mContainersDic, mTrashList, mRootTrashTrans);
            //    }
            //    mGamestate.Update();
            //}

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            ControlManager.DestroyInstance();
            GameHelper.DestroyInstance();
            ControlManager.DestroyInstance();
            EventManager.DestroyInstance();
            Dispose();
        }

        private void Dispose()
        {
        }

        /// <summary>
        /// switch game step 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        //public BaseState SwitchState(GameStateEnum state)
        //{
        //    switch (state)
        //    {
        //        case GameStateEnum.Intro:
        //            return new IntroSystem();
        //        case GameStateEnum.PlayGame:
        //            return new PlayGameSystem();
        //        case GameStateEnum.End:
        //            return new EndSystem();
        //    }
        //    DebugHelper.DebugLogError(" state is error ==" + state);
        //    return null;
        //}
    }
}
