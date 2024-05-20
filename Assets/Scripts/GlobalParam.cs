using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ClientPkg
{
    public Socket cli;
    public string ret;

    public ClientPkg(Socket cli, string ret)
    {
        this.cli = cli;
        this.ret = ret;
    }
}

public static class GlobalParam
{
    public static Queue<ClientPkg> MessageQueue = new Queue<ClientPkg>();
}
