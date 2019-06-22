using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace MultiUserWinTcpServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<TcpClient> clients = new List<TcpClient>();
        List<Thread> threads = new List<Thread>();

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
                     listBox1.Items.Add(string.Format("message from {0}:  {1}", client.Client.RemoteEndPoint, msg));
                    if (msg.Trim().Length == 0)
                        break;
                    msg = "continue....\n";
                    buffer = ASCIIEncoding.ASCII.GetBytes(msg);
                    ns.Write(buffer, 0, buffer.Length);

                }
                 listBox1.Items.Add(string.Format("Client {0} is disconnected", client.Client.RemoteEndPoint));
            }
            catch (Exception e)
            {
                 listBox1.Items.Add(string.Format("Client {0} is disconnected", client.Client.RemoteEndPoint));
            }


        }
        
        TcpListener server;

        public void StartServer()
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                clients.Add(client);

               listBox1.Items.Add(string.Format("client {0} is connected", client.Client.RemoteEndPoint));


                ParameterizedThreadStart pts = new ParameterizedThreadStart(SendRecv);

                Thread th = new Thread(pts);

                threads.Add(th);

                th.Start(client);

           }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            server = new TcpListener(8000);
            server.Start();
            button1.Enabled = false;
            button2.Enabled = true;
            
            Thread th = new Thread(new ThreadStart(StartServer));
            th.Start();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (TcpClient client in clients)
            {
                if (client.Connected)
                {
                     NetworkStream ns = client.GetStream();
                    

                        string msg = textBox1.Text +"\n";
                        byte[] buffer = ASCIIEncoding.ASCII.GetBytes(msg);
                        ns.Write(buffer, 0, buffer.Length);
                   
                }
            }
        }
    }
}
