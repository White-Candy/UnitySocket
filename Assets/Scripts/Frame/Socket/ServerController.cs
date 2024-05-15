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

        public void StartListen(int port)
        {
            m_port = port;
            IPEndPoint ip_end = new IPEndPoint(IPAddress.Any, m_port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ip_end);
            socket.Listen(5);

            thread = new Thread(ClientConnectListen);
            thread.Start();
        }

        // 客户端连接请求监听
        public void ClientConnectListen()
        {
            while (true)
            {
                Socket client = SendToClient("Hi there, I accept you request at " + DateTime.Now.ToString());

                //开启线程接收客户端信息
                Thread clientThread = new Thread(ReciveMessage);
                clientThread.Start(client);
                ctList.Add(clientThread);
            }
        }

        // 发送信息
        public Socket SendToClient(string mess)
        {
            Socket client = socket.Accept();
            client.Send(Encoding.Unicode.GetBytes(mess));
            return client;
        }

        // 接收客户端的讯息
        public void ReciveMessage(object obj)
        {
            Socket client = obj as Socket;
            while (true)
            {
                try
                {
                    int length = client.Receive(result);
                    string message = Encoding.Unicode.GetString(result, 0, length);

                    if (!string.IsNullOrEmpty(message))
                        Debug.Log(message);

                    if (!string.IsNullOrEmpty(message) && message == "Close")
                    {
                        Thread.CurrentThread.Abort();
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.Message);
                    client.Shutdown(SocketShutdown.Both);
                    client.Close(0);
                }

            }
        }

        public void Clear()
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

    }
}