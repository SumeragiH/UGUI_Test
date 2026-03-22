using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ServerPanel : BasePanel
{
    public Button btnEnter;
    public Button btnChangeServer;
    public Text txtServerID;
    public Text txtServerName;
    public Button btnBack;

    private int serverID;

    private void Start()
    {
        btnEnter.onClick.AddListener(OnEnterClick);
        btnChangeServer.onClick.AddListener(OnChangeServerClick);
        btnBack.onClick.AddListener(OnBackClick);
        Init();
    }
    public void Init()
    {
        //获取服务器数据
        LogInData logInData = DataMgr.Instance.LogInData;
        serverID = logInData.LastLogInServerID;
        if (serverID == -1)//没有服务器数据
            return;
        ServerData serverData = DataMgr.Instance.ServerDataInfo.ServerDataDic[serverID.ToString()];

        //显示服务器数据
        txtServerID.text = serverID + "区";
        //显示服务器名称
        txtServerName.text = serverData.ServerName;

    }

    public void UpdateServerInfo(int serverID)
    {
        this.serverID = serverID;
        //显示服务器数据
        txtServerID.text = serverID + "区";
        //显示服务器名称
        txtServerName.text = DataMgr.Instance.ServerDataInfo.ServerDataDic[serverID.ToString()].ServerName;
    }
    private void OnEnterClick()
    {
        //保存当前选服数据到本地
        if (serverID == -1)
        {
            UIMgr.Instance.ShowPanel<PromptPanel>().SetContent("请选择服务器");
            return;
        }
        //保存当前选服数据到本地
        DataMgr.Instance.LogInData.LastLogInServerID = serverID;
        DataMgr.Instance.SaveData();

        Debug.Log("进入游戏");
    }

    private void OnChangeServerClick()
    {
        //打开选择服务器界面
        UIMgr.Instance.ShowPanel<SelectServerPanel>();
        //关闭当前界面
        UIMgr.Instance.HidePanel<ServerPanel>();
    }

    private void OnBackClick()
    {
        //返回登录界面
        UIMgr.Instance.ShowPanel<LogInPanel>();
        //关闭当前界面
        UIMgr.Instance.HidePanel<ServerPanel>();
    }
    public override void Show(UnityAction show = null)
    {
        base.Show(show);
    }
    
}
