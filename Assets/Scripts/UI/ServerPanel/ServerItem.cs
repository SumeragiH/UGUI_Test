using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ServerItem : MonoBehaviour
{
    public Image SprNew;
    public Image SprState;
    public Text TxtServerID;
    public Text TxtServerName;
    public Button BtnServer;

    private int serverID;
    private SpriteAtlas LogInRes;

    public void Start()
    {
        LogInRes = Resources.Load<SpriteAtlas>("LogInRes");
        BtnServer.onClick.AddListener(OnBtnServerClick);
    }
    public void Init(ServerData serverData)
    {

        serverID = serverData.ServerID;

        // 在使用前初始化 LogInRes
        if (LogInRes == null)
        {
            LogInRes = Resources.Load<SpriteAtlas>("LogInRes");
        }

        TxtServerID.text = serverData.ServerID + "区";//显示服务器ID

        TxtServerName.text = serverData.ServerName;//显示服务器名称

        if (serverData.IsNew)//如果是新服,显示新服图标
        {
            SprNew.gameObject.SetActive(true);
        }
        else
        {
            SprNew.gameObject.SetActive(false);
        }

        switch (serverData.ServerState)//根据服务器状态设置状态图标
        {
            case 0:
                SprState.sprite = LogInRes.GetSprite("ui_DL_weihu_01");
                break;
            case 1:
                SprState.sprite = LogInRes.GetSprite("ui_DL_huobao_01");
                break;
            case 2:
                SprState.sprite = LogInRes.GetSprite("ui_DL_liuchang_01");
                break;
            case 3:
                SprState.sprite = LogInRes.GetSprite("ui_DL_fanhua_01");
                break;
            case 4:
                SprState.sprite = LogInRes.GetSprite("ui_DL_liuchang_01");
                break;
        }
    }

    private void OnBtnServerClick()
    {
        //点击服务器项,返回服务器界面
        ServerPanel s = UIMgr.Instance.ShowPanel<ServerPanel>();
        //更新服务器界面显示的服务器信息
        s.UpdateServerInfo(serverID);
        //关闭当前服务器项界面
        UIMgr.Instance.HidePanel<SelectServerPanel>();
    }
}
