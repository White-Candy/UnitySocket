using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEngine;

namespace SocketDemo
{
    public class MyServer : MonoBehaviour
    {
        private static byte[] result = new byte[1024];
        private const int port = 4530;
        private static string IP = "127.0.0.1";
        private static Socket socket;
        private static Socket client;

        //private Thread m_thread;
        //List<Thread> cliList = new List<Thread>();

        void Start()
        {
            IPEndPoint ip_end = new IPEndPoint(IPAddress.Any, port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ip_end);
            socket.Listen(5);   

            //m_serverSocket.Connect(IPAddress.Any, port);

            socket.BeginAccept(new AsyncCallback((ar) =>
            {
                client = socket.EndAccept(ar);

                client.Send(Encoding.Unicode.GetBytes("Hi there, I accept you request at " + DateTime.Now.ToString()));

                var timer = new System.Timers.Timer();
                timer.Interval = 2000D;
                timer.Enabled = true;
                timer.Elapsed += (o, a) =>
                {
                    if (client.Connected)
                    {
                        try
                        {
                            client.Send(Encoding.Unicode.GetBytes("Message from server at " + DateTime.Now.ToString()));
                        }
                        catch 
                        {
                            
                        }
                    }
                    else
                    {
                        timer.Stop();
                        timer.Enabled = false;
                        Debug.Log("Client is disconnected, the timer is stop.");
                    }     
                };

                timer.Start();

                //接收客户端的消息(这个和在客户端实现的方式是一样的）
                client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), client);

            }), null);

            Debug.Log("Server is ready!");
            //Console.Read();

        }

        static byte[] buffer = new byte[1024];

        public static void ReceiveMessage(IAsyncResult ar)
        {
            try
            {
                socket = ar.AsyncState as Socket;

                //方法参考：http://msdn.microsoft.com/zh-cn/library/system.net.sockets.socket.endreceive.aspx
                var length = socket.EndReceive(ar);

                //读取出来消息内容
                var message = Encoding.Unicode.GetString(buffer, 0, length);

                if (message.Length == 0) { return; }

                //显示消息
                Debug.Log(message);

                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveMessage), socket);
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }
}