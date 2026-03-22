using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr
{
    //单例模式
    private static DataMgr instance=new DataMgr();
    public static DataMgr Instance => instance;

    //数据成员
    private LogInData logInData;
    private ServerDataInfo serverDataInfo;
    //数据属性，外部通过这个属性来访问数据成员
    public LogInData LogInData => logInData;
    public ServerDataInfo ServerDataInfo => serverDataInfo;

    //在构造函数中 从Json文件中读取数据到内存中
    private DataMgr()
    {
        logInData = JsonMgr.Instance.LoadData<LogInData>("LogInData");
        serverDataInfo = JsonMgr.Instance.LoadData<ServerDataInfo>("ServerDataInfo");
    }




    //读取和保存数据的方法 供外部调用
    public void SaveData()
    {
        JsonMgr.Instance.SaveData(logInData, "LogInData");
        //这里的ServerDataInfo是从服务器获取的 不是玩家自己修改的 所以不需要保存到本地
    }
    public void LoadData()
    {
        logInData = JsonMgr.Instance.LoadData<LogInData>("LogInData");
        serverDataInfo = JsonMgr.Instance.LoadData<ServerDataInfo>("ServerDataInfo");
    }



}
