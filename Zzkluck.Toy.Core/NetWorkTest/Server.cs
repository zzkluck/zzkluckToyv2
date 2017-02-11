using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;


namespace Zzkluck.Toy.Core.NetWorkTest
{
	public class Server
	{
		public TcpListener tcpListener;
		public TcpClient tcpClient;
		public NetworkStream networkStream;
		public BinaryReader reader;
		public BinaryWriter writer;

		public IPAddress ServerIP;
		static int MyPort = 54470;
		private void ServerThreadStart(object sender, EventArgs e)
		{
			tcpListener = new TcpListener(ServerIP, MyPort);
			tcpListener.Start();
			Thread acceptThread = new Thread(acceptClientConnect);
			acceptThread.Start();
		}

		private void acceptClientConnect()
		{
			ShowInformation(0);
			Thread.Sleep(1000);
			try
			{
				ShowInformation(1);
				tcpClient = tcpListener.AcceptTcpClient();
				if (tcpClient != null)
				{
					ShowInformation(2);
					networkStream = tcpClient.GetStream();
					reader = new BinaryReader(networkStream);
					writer = new BinaryWriter(networkStream);

				}
			}
			catch
			{
				ShowInformation(3);
				Thread.Sleep(1000);
				ShowInformation(4);
			}
		}

		private void SendMessageThreadStart(string message)
		{
			Thread acceptThread = new Thread(acceptMessage);
			acceptThread.Start();
		}

		private void acceptMessage()
		{
			try
			{
				byte[] m1 = new byte[2048];
				reader.Read(m1, 0, 2048);
				string m2 = Encoding.UTF8.GetString(m1);
				ShowInformation(m2);
				Thread.Sleep(5000);
			}
			catch
			{
				if (reader != null)
				{
					reader.Close();
				}
				if (writer != null)
				{
					writer.Close();
				}
				if (tcpClient != null)
				{
					tcpClient.Close();
				}
			}
		}


		public virtual void ShowInformation(int a) { }
		public virtual void ShowInformation(string message) { }
	}
}
