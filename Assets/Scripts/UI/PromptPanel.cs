using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PromptPanel : BasePanel
{
    public Button btnSure;
    public Text txtContent;
    private void Start()
    {
        btnSure.onClick.AddListener(OnBtnSureClick);
    }

    private void OnBtnSureClick()
    {
        UIMgr.Instance.HidePanel<PromptPanel>();
    }

    public void SetContent(string content)
    {
        txtContent.text = content;
    }
}
