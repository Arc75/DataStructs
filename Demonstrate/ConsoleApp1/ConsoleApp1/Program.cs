using BinaryTree;
using LinkedListAlg;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CallTheLinkedList();
            CallTheTree();            
        }

        static void CallTheTree()
        {
            BinaryTree<int> integerTree = new BinaryTree<int>();

            Random rand = new Random();

            for (int i = 0; i < 20; i++)
            {
                int value = rand.Next(100);
                Console.WriteLine("Adding {0}", value);
                integerTree.Add(value);
            }

            Console.WriteLine("Number of nodes is {0}", integerTree.Count);
            Console.WriteLine("Max value is {0}", integerTree.MaxValue);
            Console.WriteLine("Min value is {0}", integerTree.MinValue);
            Console.WriteLine("Preorder traversal:");
            Console.WriteLine(string.Join(" ", integerTree.Preorder()));
            Console.WriteLine("Inorder traversal:");
            Console.WriteLine(string.Join(" ", integerTree.Inorder()));
            Console.WriteLine("Postorder traversal:");
            Console.WriteLine(string.Join(" ", integerTree.Postorder()));
            Console.WriteLine("Levelorder traversal:");
            Console.WriteLine(string.Join(" ", integerTree.Levelorder()));
            Console.WriteLine("Default traversal (inorder):");
            foreach (int n in integerTree)
                Console.Write("{0} ", n);
            Console.WriteLine();
            Console.ReadKey(true);
        }

        static void CallTheLinkedList()
        {
            LinkedList<int> list = new LinkedList<int>();

            list.Add(15);
            list.Add(18);
            list.Add(2);
            list.Add(8);

            foreach (var item in list)
                Console.WriteLine(item.ToString());

            list.Remove(18);

            foreach (var item in list)
                Console.WriteLine(item.ToString());

             Console.WriteLine(list.Contains(2) ? "Yes" : "No");

            list.AppendFirst(100500);
        }

    }
}
