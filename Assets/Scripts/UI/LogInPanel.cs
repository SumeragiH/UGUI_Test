using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogInPanel : BasePanel
{
    public Button BtnSure;
    public Button BtnRegister;
    public Toggle TogRememberPassWord;
    public Toggle TogAutoLogin;
    public InputField InputAccount;
    public InputField InputPassword;

    private void Start()
    {
        BtnSure.onClick.AddListener(OnBtnSureClick);
        BtnRegister.onClick.AddListener(OnBtnRegisterClick);
        TogAutoLogin.onValueChanged.AddListener(OnTogAutoLoginValueChanged);
        TogRememberPassWord.onValueChanged.AddListener(OnTogRememberPassWordValueChanged);
    }

    private void OnBtnSureClick()
    {
    }

    private void OnBtnRegisterClick()
    {
    }

    private void OnTogAutoLoginValueChanged(bool isOn)
    {
       
    }

    public void OnTogRememberPassWordValueChanged(bool isOn)
    {

    }
}
