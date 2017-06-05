using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyPresentation
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] OpponentsEcountered = new string[5];

            OpponentsEcountered[0] = "Alpha";
            OpponentsEcountered[1] = "Bravo";
            OpponentsEcountered[2] = "Charlie";
            OpponentsEcountered[3] = "Delta";
            OpponentsEcountered[4] = "Echo";

            //string checkOpponent = "Delta";
            string checkOpponent = "Foxtrot";


            if (OpponentsEcountered.Any(argument => argument == checkOpponent))
            {
                Console.WriteLine("Opponent already encountered. Continue search.");
            }
            else
            {
                Console.WriteLine("Opponent not yet encounted. Engage opponent.");
            }

            Console.ReadLine();



        }
    }
}
