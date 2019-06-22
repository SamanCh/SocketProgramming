using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using System.Net.Sockets;
using System.IO;

using System.Threading;

namespace MultiUserTcpServer
{
    class Program
    {
        public void SendRecv(object p)
        {
            
           
            TcpClient client = (TcpClient)p;
            try
            {
                NetworkStream ns = client.GetStream();

                string msg = "welcome\n";
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(msg);
                ns.Write(buffer, 0, buffer.Length);
                StreamReader sr = new StreamReader(ns);
                while (true)
                {
                    msg = sr.ReadLine();
                    Console.WriteLine("message from {0}:  {1}", client.Client.RemoteEndPoint, msg);
                    if (msg.Trim().Length == 0)
                        break;
                    msg = "continue....\n";
                    buffer = ASCIIEncoding.ASCII.GetBytes(msg);
                    ns.Write(buffer, 0, buffer.Length);

                } 
                Console.WriteLine("Client {0} is disconnected",client.Client.RemoteEndPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine("Client {0} is disconnected",client.Client.RemoteEndPoint);
            }


        }

        static void Main(string[] args)
        {
            Program p=new Program();

            TcpListener server = new TcpListener(8000);

            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                Console.WriteLine("client {0} is connected",client.Client.RemoteEndPoint);


                ParameterizedThreadStart pts = new ParameterizedThreadStart(p.SendRecv);

                Thread th = new Thread(pts);

                th.Start(client);






            }

        }
    }
}
