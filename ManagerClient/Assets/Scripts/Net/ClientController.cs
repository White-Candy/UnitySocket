using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace CandySocket
{
    public class ClientController
    {
        private static ClientController m_instance;
        public static ClientController Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new ClientController();
                }
                return m_instance;
            }
        }

        private static Socket m_socket;

        static byte[] buffer = new byte[1024];

        private bool isConnect = false;
        private string m_ip;
        private int m_port;
        private Thread m_thread;


        public void Init(string ip, Int32 port)
        {
            m_ip = ip;
            m_port = port;
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                isConnect = true;
                m_socket.Connect(m_ip, m_port);

                m_thread = new Thread(ReciveMessage);
                m_thread.Start();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                isConnect = false;
                Application.Quit();
            }
        }

        public void ReciveMessage()
        {
            while (isConnect)
            {
                try
                {
                    if (buffer == null)
                    {
                        continue;
                    }
                    int length = m_socket.Receive(buffer);
                    string mess = Encoding.Unicode.GetString(buffer, 0, length);
                    Array.Clear(buffer, 0, buffer.Length);
                    
                    
                    string endfield = mess.Substring(mess.Count() - 4, 4);
                    if (endfield == "-end")
                    {
                        mess = mess.Substring(0, mess.Count() - 4);
                        GlobalParameterManager.Message += mess;
                        if (JsonController.Instance.checkStringIsJson(GlobalParameterManager.Message))
                        {
                            //Debug.Log("ReciveMessage: " + mess); 
                            GlobalParameterManager.MessQueue.Enqueue(GlobalParameterManager.Message);
                            GlobalParameterManager.Message = "";
                        }
                    }
                    else
                    {
                        GlobalParameterManager.Message += mess;
                        //Debug.Log(GlobalParameterManager.Message);
                    }
                }
                catch (Exception ex)
                {
                    if (isConnect)
                    {
                        m_socket.Shutdown(SocketShutdown.Both);
                        m_socket.Close();
                    }
                }
            }
        }

        public void Send(string mess)
        {
            if (isConnect)
            {
                var message = mess;
                var outputBuffer = Encoding.Unicode.GetBytes(message);
                m_socket.BeginSend(outputBuffer, 0, outputBuffer.Length, SocketFlags.None, null, null);
            }
        }

        public void SocketClose()
        {
            try
            {
                isConnect = false;
                m_socket.Close();
                m_thread.Abort();
            }
            catch
            {

            }
        }
    }
}
