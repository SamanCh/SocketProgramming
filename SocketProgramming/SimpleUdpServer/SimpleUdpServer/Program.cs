using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.IO;


namespace SimpleUdpServer
{
    class Program
    {
        static void Main(string[] args)
        {

            UdpClient client = new UdpClient(8000);

            IPEndPoint sender=null;
            
            while (true)
            {
                byte[] buffer = client.Receive(ref sender);
                string msg = ASCIIEncoding.ASCII.GetString(buffer);
                Console.WriteLine("Message {0} is received from {1}",msg,sender);
            }



            
        }
    }
}
