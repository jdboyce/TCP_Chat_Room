
using System;
using System.IO;

namespace ChatServer
{
    public class TextLogger : ILoggable
    {
        public void WriteToFile(string textToLog)
        {
            File.AppendAllText("ChatRoomLog.txt", Environment.NewLine + DateTime.Now + " - " + textToLog + Environment.NewLine);
        }
    }
}
