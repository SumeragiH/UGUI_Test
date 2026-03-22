using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SelectServerPanel : BasePanel
{
    public Button BtnLastServer;
    public Text TxtLastServerID;
    public Text TxtLastServerName;
    public Image SprLastServerState;
    public Image SprLastServerIsNew;

    public Text TxtLight;
    public ScrollRect SvLeft;
    public ScrollRect SvRight;
    public Transform RightList;
    public Transform LeftList;

    private LogInData logInData;

    private SpriteAtlas LogInRes;

    private void Start()
    {
        logInData = DataMgr.Instance.LogInData;
        LogInRes = Resources.Load<SpriteAtlas>("LogInRes");
        InitLastItem();
        BtnLastServer.onClick.AddListener(OnBtnLastServerClick);
        ShowLeftItem();
        ShowRightItem(1, 5);
        TxtLight.text = "1-5区";

    }
    public void InitLastItem()
    {
        if (logInData.LastLogInServerID==-1)//如果没有上次选服数据，就显示默认文本
        {
            return;
        }

        TxtLastServerID.text = logInData.LastLogInServerID.ToString() + "区";//显示上次选服数据
        TxtLastServerName.text = DataMgr.Instance.ServerDataInfo.ServerDataDic[logInData.LastLogInServerID.ToString()].ServerName;//显示上次选服名称
        switch (DataMgr.Instance.ServerDataInfo.ServerDataDic[logInData.LastLogInServerID.ToString()].ServerState)//根据服务器状态设置状态图标
        {
            case 0:
                SprLastServerState.sprite =LogInRes.GetSprite("ui_DL_weihu_01");
                break;
            case 1:
                SprLastServerState.sprite = LogInRes.GetSprite("ui_DL_huobao_01");
                break;
            case 2:
                SprLastServerState.sprite = LogInRes.GetSprite("ui_DL_liuchang_01");
                break;
            case 3:
                SprLastServerState.sprite = LogInRes.GetSprite("ui_DL_fanhua_01");
                break;
            case 4:
                SprLastServerState.sprite = LogInRes.GetSprite("ui_DL_liuchang_01");
                break;
        }
        if (DataMgr.Instance.ServerDataInfo.ServerDataDic[logInData.LastLogInServerID.ToString()].IsNew)//设置是否显示新服图标
        {
            SprLastServerIsNew.gameObject.SetActive(true);
        }
        else
        {
            SprLastServerIsNew.gameObject.SetActive(false);
        }
    }
    public void ShowRightItem(int BeginIndex,int EndIndex)
    {
        //更新服务器列表
        TxtLight.text = BeginIndex.ToString() + "-" + EndIndex.ToString() + "区";
        //清除之前的服务器列表
        for (int i = 0; i < RightList.childCount; i++)
        {
            Destroy(RightList.GetChild(i).gameObject);
        }
        //显示新的服务器列表
        for (int i = BeginIndex; i <= EndIndex; i++)
        {
            if (DataMgr.Instance.ServerDataInfo.ServerDataDic.ContainsKey(i.ToString()))
            {
                //读取预制体，创建服务器选项按钮
                GameObject rightItem = Instantiate(Resources.Load<GameObject>("UI/BtnServer"));
                //设置父对象
                rightItem.transform.SetParent(RightList, false);
                //设置按钮位置
                rightItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(-151, 174, 0) + new Vector3(305*((i-1)%5%2),-80*((i-1)%5/2),0);
                //SvRight.content.sizeDelta = new Vector2(SvRight.content.sizeDelta.x, 80 * (Mathf.CeilToInt((EndIndex-BeginIndex+1)/2f)));//根据服务器选项数量调整右侧滚动区域大小

                //初始化服务器选项按钮
                ServerData s = DataMgr.Instance.ServerDataInfo.ServerDataDic[i.ToString()];
                rightItem.GetComponent<ServerItem>().Init(s);

            }
        }
    }
    public void ShowLeftItem()
    {
        ServerDataInfo info = DataMgr.Instance.ServerDataInfo;
        int num  = Mathf.CeilToInt(info.ServerDataDic.Count / 5f);//计算有多少个服务器范围按钮，一个按钮显示5个服务器
        for (int i = 0; i < num; i++)
        {
            //读取预制体，创建服务器范围按钮
            GameObject leftItem = Instantiate(Resources.Load<GameObject>("UI/BtnServerRange"));
            //设置父对象
            leftItem.transform.SetParent(LeftList, false);
            //设置按钮位置
            leftItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,153, 0)+new Vector3(0,-65*i,0);
            //SvLeft.content.sizeDelta = new Vector2(SvRight.content.sizeDelta.x, 65 * (num));//根据服务器范围按钮数量调整左侧滚动区域大小
            //初始化服务器范围按钮
            int beginIndex = i * 5 + 1;
            int endIndex = (i + 1) * 5;
            if (endIndex > info.ServerDataDic.Count)
            {
                endIndex = info.ServerDataDic.Count;
            }
            leftItem.GetComponent<ServerRangeItem>().Init(beginIndex,endIndex);
        }
    }

    private void OnBtnLastServerClick()
    {
        //显示服务器面板
        ServerPanel s =  UIMgr.Instance.ShowPanel<ServerPanel>();

        //更新服务器面板
        string num = TxtLastServerID.text.Substring(0,TxtLastServerID.text.Length-1);//获取服务器ID,去掉最后一个字“区”
        s.UpdateServerInfo(int.Parse(num));

        //隐藏选服面板
        UIMgr.Instance.HidePanel<SelectServerPanel>();
    }
}
