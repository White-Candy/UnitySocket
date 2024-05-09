using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace MyClient
{
    public class MyClient : MonoBehaviour
    {
        public Text serverRet;

        private static Socket socket;
        private static string message;

        static byte[] buffer = new byte[1024];
        private float timer = 2f;

        // Start is called before the first frame update
        void Start()
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("localhost", 4530);


            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None,
                new AsyncCallback(ClientReceive), socket);

            //Console.Read();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SendMessageTOServer("Hello");
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                SendMessageTOServer("World");
            }

            if (message != null)
            {
                serverRet.text = message;
            }
        }

        void OnDestroy()
        {
            socket.Close();
        }

        public static void ClientReceive(IAsyncResult ar) 
        {
            try
            {
                socket = ar.AsyncState as Socket;
                var length = socket.EndReceive(ar);
                message = Encoding.Unicode.GetString(buffer, 0, length);

                Debug.Log(message);

                //接收下一个消息(因为这是一个递归的调用，所以这样就可以一直接收消息了）
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ClientReceive), socket);
            }
            catch (Exception ex)
            {
                //Debug.Log("Catch: " + ex.Message);
            }
        }

        public void SendMessageTOServer(string mess)
        {
            var message = "Message from client : " + mess;
            var outputBuffer = Encoding.Unicode.GetBytes(message);
            socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
        }
    }
}
