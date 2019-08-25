using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;//首先要记得引用命名空间
public class loginsocket : MonoBehaviour
{
    private static byte[] result = new byte[1024];
    private static Socket clientSocket;
  //  public static loginsocket  _instance;
    void Awake()
    {
  //      _instance = this; 
    }
    public static void login(string name)
    {
        //设定服务器IP地址  
      //  IPAddress ip = IPAddress.Parse(NetworkUtils.GetLocalIPv4());
        IPAddress ip = IPAddress.Parse("129.28.94.184");
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(new IPEndPoint(ip, 8885)); //配置服务器IP与端口  
            Debug.Log("连接服务器成功");
        }
        catch
        {
            Debug.Log("连接服务器失败，请按回车键退出！");
            return;
        }
        try
        {
            string sendMessage = name;
            clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
            Debug.Log("向服务器发送用户名：{0}" + sendMessage);
        }
        catch
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }

        Thread myThread = new Thread(ReceiveMessage);
        myThread.Start(clientSocket);
     //   int receiveLength = clientSocket.Receive(result);
     //   Debug.Log("接收服务器消息：" + Encoding.ASCII.GetString(result, 0, receiveLength));
    }
    public static void send(string str)
    {
        clientSocket.Send(Encoding.ASCII.GetBytes(str));
    }
    private static void ReceiveMessage(object clientSocket)
    {
        Socket myClientSocket = (Socket)clientSocket;
        while (true)
        {
            try
            {
                //通过clientSocket接收数据  
                int receiveNumber = myClientSocket.Receive(result);
                string str = Encoding.ASCII.GetString(result, 0, receiveNumber);
                string[] array = str.Split(',');
                if (array[0] == "1")
                {
                    manage._instance.SubHero1Hp(int.Parse(array[1]));
                }
                else if (array[0] == "0") {
                    loginmanage.yourname = array[1];
                    GameController.currenHeroName = "hero2";

                    SceneManager.LoadScene("playing");  

                }
                else if (array[0] == "2")
                {
                    GameController._instance.TransformPlayer();
                }



                Debug.Log("接收服务器消息" + Encoding.ASCII.GetString(result, 0, receiveNumber));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                myClientSocket.Shutdown(SocketShutdown.Both);
                myClientSocket.Close();
                break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
