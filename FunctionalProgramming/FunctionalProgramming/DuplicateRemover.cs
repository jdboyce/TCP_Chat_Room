
using System;
using System.Collections.Generic;
using System.Linq;


namespace FunctionalProgramming
{
    class DuplicateRemover
    {

        public List<string> RemoveDuplicates(List<string> passedList)
        {
            var distinctList = new List<string>();
            distinctList.AddRange(passedList.Distinct());
            return distinctList;
        }


        public void RunDemo()
        {
            List<string> nameList = new List<string>();
            nameList.AddRange(new string[] { "Adam", "Billy", "Billy", "Billy", "Charles", "Deborah", "Emily", "Adam", "Emily", "Emily" });
            Console.WriteLine("\nHere is the list of names before duplicates are removed:\n");
            nameList.ForEach(Console.WriteLine);
            Console.WriteLine("\n\nHere is the list of names after duplicates are removed using the \"Distinct\" extension method:\n");
            RemoveDuplicates(nameList).ForEach(Console.WriteLine);
        }
    }
}
