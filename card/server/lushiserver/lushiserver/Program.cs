using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
namespace lushiserver
{

    class Program
    {
        private static byte[] result = new byte[1024];
        private static int myProt = 8885;   //端口  
        private static List<Socket> list = new List<Socket>();
        private static Hashtable socketmap = new Hashtable(),fightname=new Hashtable(),threadtable=new Hashtable(), socketname = new Hashtable();
        static Socket serverSocket;
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse(NetworkUtils.GetLocalIPv4());
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口  
            serverSocket.Listen(10);    //设定最多10个排队连接请求  
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());
            //通过Clientsoket发送数据  
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();



            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();

            Console.ReadLine();
        }
        /// <summary>  
        /// 监听客户端连接  
        /// </summary>  
        private static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                //clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));


                int receiveNumber = clientSocket.Receive(result);
                string NickName = Encoding.ASCII.GetString(result, 0, receiveNumber);
                Console.WriteLine(NickName);
                if (socketmap.Contains(NickName))
                {
                    Console.WriteLine("存在名字");
                }
                else
                {
                    Console.WriteLine("登录成功");
                    socketmap.Add(NickName,clientSocket);
                    socketname.Add(clientSocket, NickName);
                    Thread receiveThread = new Thread(ReceiveMessage);
                    receiveThread.Start(clientSocket);

                    threadtable.Add(clientSocket, receiveThread);
                    //  ((Socket)socketmap[NickName]).Send(Encoding.ASCII.GetBytes("testhashtable"));

                    list.Add(clientSocket);
                }
            }
        }

        /// <summary>  
        /// 接收消息  
        /// </summary>  
        /// <param name="clientSocket"></param>  
        private static void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                //Console.WriteLine("bug");
                try
                {
                    //通过clientSocket接收数据  
                    int receiveNumber = myClientSocket.Receive(result);
                    string str = Encoding.ASCII.GetString(result, 0, receiveNumber);
                    string[] array = str.Split(',');
                    Console.WriteLine(str);
                    if (array[0] == "0")
                    {
                        //fightname.Add(array[1], array[2]);
                        //fightname.Add(array[2], array[1]);
                        Socket s = (Socket)socketmap[array[2]];
                        s.Send(Encoding.ASCII.GetBytes("0," + array[1]));
                    }
                    else if(array[0]=="1")
                    {
                        Socket s = (Socket)socketmap[array[1]];
                        s.Send(Encoding.ASCII.GetBytes("1," + array[2]));
                    }
                    else if (array[0] == "2")
                    {
                        Socket s = (Socket)socketmap[array[1]];
                        s.Send(Encoding.ASCII.GetBytes("2"));
                    }

                   // Console.WriteLine("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //myClientSocket.Shutdown(SocketShutdown.Both);
                    //myClientSocket.Close();
                    break;
                }
            }
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (list.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {

                    string sendStr = "Server Information";
                    byte[] bs = Encoding.ASCII.GetBytes(sendStr);
                    if (list[i].Poll(1000, SelectMode.SelectRead))
                    //SelectMode.SelectRead表示，如果已调用 并且有挂起的连接，true。
                    //- 或 - 如果有数据可供读取，则为 true。- 或 - 如果连接已关闭、重置或终止，则返回 true（此种情况就表示若客户端断开连接了，则此方法就返回true）； 否则，返回 false。

                    {
                        ((Thread)threadtable[list[i]]).Abort();
                        string name =(string) socketname[list[i]];
                        socketmap.Remove(name);
                        fightname.Remove(name);
                        socketname.Remove(list[i]);
                        list[i].Close();//关闭socket
                        list.RemoveAt(i);//从列表中删除断开的socke
                        continue;

                    }

                    list[i].Send(bs, bs.Length, 0);

                }
            }
        }
    }
}
