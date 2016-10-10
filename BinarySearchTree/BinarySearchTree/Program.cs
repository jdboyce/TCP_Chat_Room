using System;

namespace BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree binarySearchTree = new BinarySearchTree();

            binarySearchTree.Insert(7);
            binarySearchTree.Insert(6);
            binarySearchTree.Insert(1);
            binarySearchTree.Insert(4);
            binarySearchTree.Insert(21);
            binarySearchTree.Insert(37);
            binarySearchTree.Insert(185);
            binarySearchTree.Insert(16);
            binarySearchTree.Insert(44);
            binarySearchTree.Insert(3);
            binarySearchTree.Insert(2);
            binarySearchTree.DisplayOnConsole();
            Console.ReadLine();
        }
    }
}
