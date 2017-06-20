
using System;


namespace ChatServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            TextLogger textLogger = new TextLogger();
            ChatServer chatServer = new ChatServer(textLogger);
            chatServer.Run();
            Console.ReadLine();
        }
    }
}