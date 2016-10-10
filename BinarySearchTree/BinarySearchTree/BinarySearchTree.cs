

namespace BinarySearchTree
{
    class BinarySearchTree
    {
        private Node root;
        private int count;

        public BinarySearchTree()
        {
            root = null;
            count = 0;
        }


        public bool Search(int passedNumber)
        {
            return root.Search(root, passedNumber);
        }


        public bool CheckIfEmpty()
        {
            return root == null;
        }
         

        public bool CheckIfLeaf()
        {
            if (!CheckIfEmpty())
            {
                return root.CheckIfLeaf(ref root);
            }

            return true;
        }


        public void Insert(int d)
        {
            if (CheckIfEmpty())
            {
                root = new Node(d);
            }
            else
            {
                root.InsertData(ref root, d);
            }

            count++;
        }

       

        public void DisplayOnConsole()
        {
            if (!CheckIfEmpty())
            {
                root.DisplayOnConsole(root);
            }
        }

        public int Count()
        {
            return count;
        }
    }
}
