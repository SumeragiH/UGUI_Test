using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LogInPanel : BasePanel
{
    public Button BtnSure;
    public Button BtnRegister;
    public Toggle TogRememberPassWord;
    public Toggle TogAutoLogin;
    public InputField InputAccount;
    public InputField InputPassword;

    private LogInData logInData;

    private void Start()
    {
        BtnSure.onClick.AddListener(OnBtnSureClick);
        BtnRegister.onClick.AddListener(OnBtnRegisterClick);
        TogAutoLogin.onValueChanged.AddListener(OnTogAutoLoginValueChanged);
        TogRememberPassWord.onValueChanged.AddListener(OnTogRememberPassWordValueChanged);
        Init();
    }

    private void OnBtnSureClick()
    {
        if (InputAccount.text == "" || InputPassword.text == "")
        {
            PromptPanel  p = UIMgr.Instance.ShowPanel<PromptPanel>();
            p.SetContent("账号或密码不能为空！");
            return;
        }
        if (InputAccount.text != logInData.Account || InputPassword.text != logInData.Password)
        {
            PromptPanel p = UIMgr.Instance.ShowPanel<PromptPanel>();
            p.SetContent("账号或密码错误！");
            return;
        }
        //如果账号和密码都正确了，就进入选服面板
        if(logInData.LastLogInServerID==-1)//如果上次登录的服务器ID没有了 就说明是第一次登录 就直接进入选服面板
        {
            UIMgr.Instance.ShowPanel<SelectServerPanel>();
        }
        else
        {
            //如果上次登录的服务器ID有了 就说明不是第一次登录 就直接进入服务器界面
            UIMgr.Instance.ShowPanel<ServerPanel>();
        }


        //保存数据
        logInData.IsAutoLogin = TogAutoLogin.isOn;
        logInData.IsRememberPassWord = TogRememberPassWord.isOn;
        DataMgr.Instance.SaveData();
        //关闭登录面板
        UIMgr.Instance.HidePanel<LogInPanel>(false);
    }

    private void OnBtnRegisterClick()
    {
        //点击注册按钮 就显示注册面板
        UIMgr.Instance.ShowPanel<RegisterPanel>();
        //隐藏登录面板
        UIMgr.Instance.HidePanel<LogInPanel>();

    }

    private void OnTogAutoLoginValueChanged(bool isOn)
    {
       if (isOn)
        {
            //如果自动登录被打开了 就把记住密码也打开了
            TogRememberPassWord.isOn = true;
        }
    }

    private void OnTogRememberPassWordValueChanged(bool isOn)
    {
        if (!isOn)
        {
            //如果记住密码被关闭了 就把自动登录也关闭了
            TogAutoLogin.isOn = false;
        }
    }

    public void Init()
    { 
        logInData = DataMgr.Instance.LogInData;
        if(logInData==null)
            return;

        if (logInData.Account==null&&logInData.LastLogInServerID==-1)//如果账号和上次登录的服务器ID都没有 那就说明是第一次登录 就不需要进行任何操作了 直接返回就好了
            return;

        //如果不是第一次登录，显示数据

        InputAccount.text = logInData.Account;
        TogAutoLogin.isOn = logInData.IsAutoLogin;
        TogRememberPassWord.isOn = logInData.IsRememberPassWord;
        if (logInData.IsRememberPassWord)
        {
            InputPassword.text = logInData.Password;
        }
        //如果是自动登录 就直接点击登录按钮
        if (logInData.IsAutoLogin)
        {
            OnBtnSureClick();
        }
    }

    //当面板显示的时候 就调用Init方法 来显示数据
    public override void Show(UnityAction show = null)
    {
        base.Show(show);
    }
}
