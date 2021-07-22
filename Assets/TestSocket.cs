

using UnityEngine;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

public class TestSocket : MonoBehaviour
{

    void Start()
    {
        ConncetServer();
    }

    void ConncetServer()
    {
        IPAddress ipAdr = IPAddress.Parse("172.26.213.158");

        IPEndPoint ipEp = new IPEndPoint(ipAdr, 6666);

        Socket clientScoket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        clientScoket.Connect(ipEp);

        string output = "zhangsan";//  中文暂时不知道怎么解析  “中文”

        byte[] concent = Encoding.UTF8.GetBytes(output);

        //byte[] concent = Encoding.Unicode.GetBytes(output);
        int count = clientScoket.Send(concent);

        Debug.LogError(count);

        //byte[] response = new byte[1024];

        //int bytesRead = clientScoket.Receive(response);

        //string input = Encoding.UTF8.GetString(response, 0, bytesRead);

        //print(“Client request:” + input);
        clientScoket.Shutdown(SocketShutdown.Both);
        clientScoket.Close();
    }

    private void ConnectCallBack(System.IAsyncResult ar)
    {
        Debug.LogError("连接成功");
    }
}