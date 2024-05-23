using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace CandySocket
{
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
        public static UIMessage uimessage = GameObject.Find("UI").GetComponent<UIMessage>();
    }
}