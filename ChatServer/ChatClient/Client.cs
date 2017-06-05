
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ChatClient
{
    public class ChatClient
    {

        public TcpClient chatClient;
        public string userName;

        public ChatClient(TcpClient chatClient)
        {
            this.chatClient = chatClient;
        }


        public void Run()
        {
            Task.Run(() => ReceiveMessages());
            SendMessages();
        }


        public void ReceiveMessages()
        {
            while (true)
            {
                try
                {
                    NetworkStream reader = chatClient.GetStream();
                    byte[] buffer = new byte[chatClient.ReceiveBufferSize];
                    int bytesRead = reader.Read(buffer, 0, chatClient.ReceiveBufferSize);
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    string formattedMessage = FormatMessage(message);
                    Console.WriteLine(formattedMessage + "\n");
                }
                catch
                {
                    Console.WriteLine("Connection has been lost. Please exit the chat room application.");
                    break;
                }
            }
        }


        public string FormatMessage(string passedMessage)
        {
            string formMessage;

            if(passedMessage.Contains("*"))
            {
                string[] textArray = passedMessage.Split('*');
                if (textArray[0] == userName)
                {
                    formMessage = "\n\nYou have joined the chat room!\n\nTo send a message, simply type what you want to say, then press enter.\n";
                }
                else
                {
                    formMessage = textArray[0] + textArray[1];
                }

            }
            else if (passedMessage.Contains("@"))
            {
                string[] textArray = passedMessage.Split('@');
                if(textArray[0] == userName)
                {
                    formMessage = "\nYou" + textArray[1];
                }
                else
                {
                    formMessage = textArray[0] + textArray[1];
                }
            }
            else
            {
                formMessage = passedMessage;
            }
            return formMessage;

        }

        public void SendMessages()
        {
            string text;
            while (true)
            {
                try
                {
                    NetworkStream writer = chatClient.GetStream();
                    if(userName == null)
                    {
                        userName = Console.ReadLine();
                        text = userName;
                    }
                    else
                    {
                        text = Console.ReadLine();
                    }
                    byte[] message = ASCIIEncoding.ASCII.GetBytes(text);
                    writer.Write(message, 0, message.Length);
                    writer.Flush();
                    writer = null;
                }
                catch
                {
                    Thread.Sleep(3000);
                }
            }
        }
    }
}


















  //NetworkStream networkStream = client.GetStream();


//Console.WriteLine("Sending back: " + message);
                //streamWriter.WriteLine(message);


                //string textToSend = Console.ReadLine();
                //byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

                //Console.WriteLine("Sending: " + textToSend);
                //networkStream.Write(bytesToSend, 0, bytesToSend.Length);

                //byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                //int bytesRead = networkStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                //Console.WriteLine("Received back: " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));