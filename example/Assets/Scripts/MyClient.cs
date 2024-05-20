using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using UnityEngine.UI;
using UnityEngine;
using System;
using LitJson;
using System.Net;
using System.Threading;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEditor.PackageManager;

namespace MyClient
{

    [Serializable]
    public class RetureClientBody
    {
        public string type;
        public string body;
    }

    public class MyClient : MonoBehaviour
    {
        public Text serverRet;

        private static Socket socket;

        static byte[] buffer = new byte[1024];
        private static string message;

        private bool isConnect = false;
        private string IpStr = "127.0.0.1";
        private int port = 4530;
        private Thread thread;

        void Start()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                isConnect = true;
                socket.Connect(IpStr, 4530);
                Debug.Log("链接服务器成功");
                thread = new Thread(ReciveMessage);
                thread.Start();
            }
            catch
            {
                isConnect = false;
            }

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Send("Hello");
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                Send("World");
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                RetureClientBody body = new RetureClientBody();

                body.type = "Close";
                string mess = JsonMapper.ToJson(body);
                Debug.Log(mess);

                Send(mess);
            }

            if (message != null)
            {
                serverRet.text = message;
            }
        }

        public void ReciveMessage()
        {
            while (true)
            {
                try
                {
                    int num = socket.Receive(buffer);
                    var length = buffer.Length;
                    string mess = Encoding.Unicode.GetString(buffer, 0, length);

                    Debug.Log(mess);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex);
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
        }

        public void Send(string mess)
        {
            if (isConnect)
            {
                var message = mess;
                var outputBuffer = Encoding.Unicode.GetBytes(message);
                socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
            }
        }

        void OnDestroy()
        {
            Send("Close");
            socket.Close();
            thread.Abort();
        }
    }
}
