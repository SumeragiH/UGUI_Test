using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerRangeItem : MonoBehaviour
{
    public Text TxtServerRange;
    public Button BtnServerRange;

    private int beginIndex;
    private int endIndex;

    private void Start()
    {
        BtnServerRange.onClick.AddListener(OnBtnSelectClick);
    }
    private void OnBtnSelectClick()
    {
        //点击该范围按钮，通知ServerSelectPanel显示该范围内的服务器列表
        UIMgr.Instance.ShowPanel<SelectServerPanel>().ShowRightItem(beginIndex,endIndex);

    }

    public void Init(int beginIndex,int endIndex)
    {
        this.beginIndex = beginIndex;
        this.endIndex = endIndex;
        TxtServerRange.text = beginIndex.ToString() + "—" + endIndex.ToString() + "区";
    }
}
