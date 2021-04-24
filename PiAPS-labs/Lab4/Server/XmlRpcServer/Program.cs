using System;


namespace XmlRpcServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Nwc.XmlRpc.XmlRpcServer server = new Nwc.XmlRpc.XmlRpcServer(8888);
            server.Add("sample", new Matrix());
            Console.WriteLine("Поехали!");
            server.Start();
        }
    }
}
