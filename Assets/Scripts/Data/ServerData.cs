using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerData
{
    public int ServerID;
    public string ServerName;
    public int ServerState; //0-维护 1-爆满 2-推荐 3-繁忙 4-正常
    public bool IsNew;  
}
