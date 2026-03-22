using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public Button btnSure;
    public Button btnCancel;
    public InputField inputAccount;
    public InputField inputPassword;

    private LogInData logInData;

    private void Start()
    {
        btnSure.onClick.AddListener(OnBtnSureClick);
        btnCancel.onClick.AddListener(OnBtnCancelClick);
        logInData = DataMgr.Instance.LogInData;
    }

    private void OnBtnSureClick()
    {
        if (inputAccount.text == "" || inputPassword.text == "")
        {
            PromptPanel p = UIMgr.Instance.ShowPanel<PromptPanel>();
            p.SetContent("账号或密码不能为空！");
            return;
        }
        if (inputAccount.text == logInData.Account)
        {
            PromptPanel p = UIMgr.Instance.ShowPanel<PromptPanel>();
            p.SetContent("账号已存在！");
            return;
        }

        //如果都合法了

        //把注册的账号和密码保存到数据管理类中
        DataMgr.Instance.LogInData.Account = inputAccount.text;
        DataMgr.Instance.LogInData.Password = inputPassword.text;
        DataMgr.Instance.SaveData();
        //关闭注册面板 显示登录面板
        UIMgr.Instance.ShowPanel<LogInPanel>();
        UIMgr.Instance.HidePanel<RegisterPanel>();
    }
    private void OnBtnCancelClick()
    {
        //关闭注册面板 显示登录面板
        UIMgr.Instance.ShowPanel<LogInPanel>();
        UIMgr.Instance.HidePanel<RegisterPanel>();
    }

}
