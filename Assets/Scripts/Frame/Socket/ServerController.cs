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
        //private static Socket client;

        private Thread thread;
        private List<Thread> ctList = new List<Thread>();
        DatabaseController data;
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
            data = new DatabaseController();
            thread = new Thread(ClientConnectListen);
            thread.Start();
        }

        // 客户端连接请求监听
        public void ClientConnectListen()
        {
            Debug.Log("ClientConnectListen");
            data.Kinder.users.Add(new UserTable());

            while (true)
            {
                Socket client = socket.Accept();
                Debug.Log(string.Format("客户端{0}成功链接", client.RemoteEndPoint));

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
            Socket client = obj as Socket;
            while (!Exit)
            {
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
                    Debug.Log(ex.Message);
                    if (client != null)
                    {
                        client.Shutdown(SocketShutdown.Receive);
                        client.Close(0);
                    }
                }
            }
        }

        public void CliRetInfoProcess(Socket cli, string ret)
        {
            Debug.Log(ret);
            ReciveClientBody body = JsonController.Instance.StringToJson(ret);

            if (body != null)
            {
                IEvent spawn = EventSpawn.CreateEvent(body.type);
                spawn.CliRetInfoProcess(cli, body.body);
            }
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
                Debug.Log("Clear: " + ex.Message);
            }
        }

    }
}