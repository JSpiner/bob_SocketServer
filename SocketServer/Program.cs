using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketServer
{
	class MainClass
	{
		Socket sender;

		public static void Main (string[] args)
		{
			MainClass m = new MainClass ();
		}

		public MainClass(){
		
			initSocket ();

			Thread receiveThread = new Thread (receiveData);

			while (true) {
				sendMessage ("{\"type\":1,\n\"raw\":\"VGhpcyBpcyBzbm9ydCB0ZXN0IGRhdGEgpHi4IEykuCBwdDA=\"}");
				Thread.Sleep (10000);
			}

		}

		void receiveData(){
			while (true) {


				byte[] bytes = new byte[1024];


				int bytesRec = sender.Receive(bytes);

				if (bytesRec != 0) {
					Console.WriteLine ("Receive : {0} {1}",
						Encoding.ASCII.GetString (bytes, 0,bytesRec),
						bytesRec);
				}
			}
		}

		void initSocket(){


			IPHostEntry ipHost = Dns.Resolve("192.168.32.11");
			IPAddress ipAddr = ipHost.AddressList[0];
			IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);


			sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			sender.Connect(ipEndPoint);

			Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());
		}

		void sendMessage(String str){


			byte[] msg = Encoding.ASCII.GetBytes(str);


			int bytesSent = sender.Send(msg);
			Console.WriteLine ("send data {0}",bytesSent);

		}
	}
}
