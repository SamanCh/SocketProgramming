using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace SimpleUdpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient client = new UdpClient();
            while (true)
            {
                string str1 = Console.ReadLine() + "\n";
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(str1);
                client.Send(buffer, buffer.Length, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000));
            }
        }
    }
}
