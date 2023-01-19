using System;
namespace VillaAPI.Logger
{
    public class Logging : ILogging
    {
        public void Log(string msg, string type)
        {
            if(type == "error")
            {
                Console.WriteLine("Error: " +  msg);
            }
            else
            {
                Console.WriteLine(msg);
            }
        }
    }
}

