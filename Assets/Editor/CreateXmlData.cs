using System.Collections.Generic;
using Assets.Script;
using Assets.Script.CradManager;
using Assets.Script.Tools;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class CreateXmlData : EditorWindow
    {
        [MenuItem("CreateXml/CradInfo")]
        private static void CreateCradXml()
        {
            //创建窗口
            Rect wr = new Rect(0, 0, 500, 500);
            CreateXmlData window = (CreateXmlData)EditorWindow.GetWindowWithRect(typeof(CreateXmlData), wr, true, "CradInfo");
            window.autoRepaintOnSceneChange = true;
            window.Show();
        }

        //输入文字的内容
        private string toggleGroupText;
        private List<CardData> CardDataList;
        private CardData tempCardData;
        private int count;
        private Vector2 scrollPosition = Vector2.zero;
        public void Awake()
        {
            //在资源中读取一张贴图
            CardDataList = new List<CardData>(60);
            ReadXmlNewMgr.CreateInstance();
            SaveGameDataXmlMgr.CreateInstance();
            CardDataList = SaveGameDataXmlMgr.instance.GetCradDataInfoByXml();
            count = CardDataList.Count;
        }

        //绘制窗口时调用
        void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            for (int i = 0; i < count; i++)
            {
                tempCardData = CardDataList.Find(info => (info.ConfigId == i));
                if (tempCardData==null)
                {
                    tempCardData =new CardData();
                    CardDataList.Add(tempCardData);
                    tempCardData.ConfigId = i;
                    tempCardData.IsShow = true;
                }
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("*****************************************");
                tempCardData.CradName = EditorGUILayout.TextField("卡片名字:", tempCardData.CradName);
                tempCardData.IsShow = EditorGUILayout.BeginToggleGroup(tempCardData.CradName, tempCardData.IsShow);
                if (tempCardData.IsShow)
                {
                    tempCardData.HpValue = EditorGUILayout.IntField("输入点数:", tempCardData.HpValue);
                    tempCardData.ActorType = (ActorTypeEnum)EditorGUILayout.EnumPopup("角色类型:", tempCardData.ActorType);
                    tempCardData.PokerType = (PokerTypeEnum)EditorGUILayout.EnumPopup("扑克类型:", tempCardData.PokerType);
                    tempCardData.SpritePath = EditorGUILayout.TextField("卡片sprite名字:", tempCardData.SpritePath);
                }
              
                EditorGUILayout.EndToggleGroup();
            }
            EditorGUILayout.EndScrollView();
            if (GUILayout.Button("新建Crad", GUILayout.Width(200)))
            {
                count++;
                tempCardData = new CardData();
            }

            if (GUILayout.Button("保存", GUILayout.Width(200)))
            {
                SaveGameDataXmlMgr.instance.CreateCradDataXml(CardDataList);
            }

            //if (GUILayout.Button("打开通知", GUILayout.Width(200)))
            //{
            //    //打开一个通知栏
            //    this.ShowNotification(new GUIContent("This is a Notification"));
            //}

            //if (GUILayout.Button("关闭通知", GUILayout.Width(200)))
            //{
            //    //关闭通知栏
            //    this.RemoveNotification();
            //}

            ////文本框显示鼠标在窗口的位置
            //EditorGUILayout.LabelField("鼠标在窗口的位置", Event.current.mousePosition.ToString());


            if (GUILayout.Button("关闭窗口", GUILayout.Width(200)))
            {
                //关闭窗口
                SaveGameDataXmlMgr.DestroyInstance();
                this.Close();
            }
        }

        //更新
        void Update()
        {

        }

        void OnFocus()
        {
            Debug.Log("当窗口获得焦点时调用一次");
        }

        void OnLostFocus()
        {
            Debug.Log("当窗口丢失焦点时调用一次");
        }

        void OnHierarchyChange()
        {
            Debug.Log("当Hierarchy视图中的任何对象发生改变时调用一次");
        }

        void OnProjectChange()
        {
            Debug.Log("当Project视图中的资源发生改变时调用一次");
        }

        void OnInspectorUpdate()
        {
            //Debug.Log("窗口面板的更新");
            //这里开启窗口的重绘，不然窗口信息不会刷新
            this.Repaint();
        }

        void OnSelectionChange()
        {
            //当窗口出去开启状态，并且在Hierarchy视图中选择某游戏对象时调用
            foreach (Transform t in Selection.transforms)
            {
                //有可能是多选，这里开启一个循环打印选中游戏对象的名称
                Debug.Log("OnSelectionChange" + t.name);
            }
        }

        void OnDestroy()
        {
            Debug.Log("当窗口关闭时调用");
            ReadXmlNewMgr.DestroyInstance();
            SaveGameDataXmlMgr.DestroyInstance();
        }
    }
}
