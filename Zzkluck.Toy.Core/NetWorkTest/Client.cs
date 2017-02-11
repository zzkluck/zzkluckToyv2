using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Zzkluck.Toy.Core.NetWorkTest
{
	public class Client
	{
		public TcpClient tcpClient;
		public NetworkStream networkStream;
		public BinaryReader reader;
		public BinaryWriter writer;

		public IPAddress ServerIP;
		static int MyPort = 54470;
		public void ClientThreadStart()
		{
			Thread connectThread = new Thread(connectToServer);
			connectThread.Start();

		}
		public void connectToServer()
		{
			try
			{
				tcpClient = new TcpClient();
				tcpClient.Connect(ServerIP, MyPort);

				Thread.Sleep(1000);

				if (tcpClient != null)
				{
					networkStream = tcpClient.GetStream();
					reader = new BinaryReader(networkStream);
					writer = new BinaryWriter(networkStream);
				}
			}
			catch
			{
				Thread.Sleep(1000);
			}
		}


		private void SendMessageThreadStart(string message)
		{
			Thread sendThread = new Thread(SendMessage);
			sendThread.Start(message);
		}

		private void SendMessage(object state)
		{
			try
			{
				writer.Write(state.ToString());
				Thread.Sleep(5000);
				writer.Flush();
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
	}

	
}
