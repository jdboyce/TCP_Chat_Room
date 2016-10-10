using System;


namespace BinarySearchTree
{
    class Node
    {
        private int nodeValue;
        public Node rightChild;
        public Node leftChild;

        public Node(int passedInt)
        {
            nodeValue = passedInt;
            rightChild = null;
            leftChild = null;
        }

        public bool CheckIfLeaf(ref Node node)
        {
            return (node.rightChild == null && node.leftChild == null);
        }


        public void InsertData(ref Node node, int passedData)
        {
            if (node == null)
            {
                node = new Node(passedData);

            }
            else if (node.nodeValue < passedData)
            {
                InsertData(ref node.rightChild, passedData);
            }

            else if (node.nodeValue > passedData)
            {
                InsertData(ref node.leftChild, passedData);
            }
        }


        public bool Search(Node node, int passedNumber)
        {
            if (node == null)
                return false;

            if (node.nodeValue == passedNumber)
            {
                return true;
            }
            else if (node.nodeValue < passedNumber)
            {
                return Search(node.rightChild, passedNumber);
            }
            else if (node.nodeValue > passedNumber)
            {
                return Search(node.leftChild, passedNumber);
            }

            return false;
        }


        public void DisplayOnConsole(Node node)
        {
            if (node == null)
                return;

            DisplayOnConsole(node.leftChild);
            Console.Write(" " + node.nodeValue);
            DisplayOnConsole(node.rightChild);
        }
    }
}
