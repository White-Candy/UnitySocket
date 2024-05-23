using LitJson;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace CandySocket
{
    public class ServerContorller
    {
        private static byte[] result = new byte[1024];
        private static int m_port;
        private static Socket socket;
        private Thread thread;
        private List<Thread> ctList = new List<Thread>();

        private static ServerContorller m_instance;
        public static ServerContorller Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new ServerContorller();
                }
                return m_instance;
            }
        }

        public bool Exit = false;
        public void StartListen(int port)
        {
            m_port = port;
            IPEndPoint ip_end = new IPEndPoint(IPAddress.Any, m_port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ip_end);
            socket.Listen(10);

            thread = new Thread(ClientConnectListen);
            thread.Start();
        }

        // 客户端连接请求监听
        public void ClientConnectListen()
        {
            while (true)
            {
                Socket client = socket.Accept();

                string mess = "Hi there, I accept you request at " + DateTime.Now.ToString();
                SendToClient(client, mess);

                //开启线程接收客户端信息
                Thread clientThread = new Thread(ReciveMessage);
                clientThread.Start(client);
                ctList.Add(clientThread);
            }
        }

        // 发送信息
        public void SendToClient(Socket cli, string mess)
        {
            cli.Send(Encoding.Unicode.GetBytes(mess));
        }

        // 接收客户端的讯息
        public void ReciveMessage(object obj)
        {
            while (!Exit)
            {
                Socket client = obj as Socket;
                try
                {
                    int length = client.Receive(result);
                    string message = Encoding.Unicode.GetString(result, 0, length);

                    if (!string.IsNullOrEmpty(message))
                    {
                        CliRetInfoProcess(client, message);
                    }
                }
                catch (Exception ex)
                {
                    //GlobalParam.uimessage.AdditionalContent(ex.Message);
                    Debug.Log(ex.Message);
                    if (client != null)
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close(0);
                    }
                }
            }
        }

        public void CliRetInfoProcess(Socket cli, string ret)
        {
            Debug.Log(ret);
            GlobalParam.MessageQueue.Enqueue(new ClientPkg(cli, ret)); 
        }

        public void Clear()
        {
            try
            {
                socket.Close();
                thread.Abort();
                thread = null;

                Debug.Log(ctList.Count);
                for (int i = 0; i < ctList.Count; i++)
                {
                    ctList[i].Abort();
                    ctList[i] = null;
                }
            }
            catch(Exception ex)
            {
                //GlobalParam.uimessage.AdditionalContent("Clear: " + ex.Message);
                Debug.Log("Clear: " + ex.Message);
            }
        }

    }
}