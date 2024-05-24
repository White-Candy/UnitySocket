using System;
using System.IO;
using System.Xml;
using UnityEngine;

/// <summary>
/// 服务器配置类
/// </summary>
public class Config
{

    /// <summary>
    /// 配置类所使用的目录为“程序基目录/Local/”
    /// </summary>
    static private string folderPath = "./Local";
    /// <summary>
    /// 配置文件的路径为“程序基目录/Local/PathConfig.xml”
    /// </summary>
    static private string fullPath = folderPath + "/config.xml";

    /// <summary>
    /// 服务器地址
    /// </summary>
    static public string url { get; private set; }

    static public void init()
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            createFile();
        }
        else if (!File.Exists(fullPath))
        {
            createFile();
        }
        else
        {
            XmlDocument xml = new XmlDocument();
            XmlReaderSettings xrs = new XmlReaderSettings();
            xrs.IgnoreComments = true;
            XmlNode root = null;
            try
            {
                xml.Load(XmlReader.Create(fullPath, xrs));
                root = xml.SelectSingleNode("ROOT");
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
            }
            try
            {
                XmlNode node = root.SelectSingleNode("Server");
                url = node.InnerText;
                //if (!url.StartsWith("http://")) url = "http://"+url;
                //if (!url.EndsWith("/")) url += "/";
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                Debug.Log(e.StackTrace);
            }
            //Debug.Log("url:" + url);
        }
    }

    static private void createFile()
    {
        File.WriteAllText(fullPath
            , "<ROOT>" + Environment.NewLine
            + "  <Server>127.0.0.1</Server>" + Environment.NewLine
            + "</ROOT>"
            , System.Text.Encoding.UTF8);
    }
}
