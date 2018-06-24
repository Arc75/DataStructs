using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTree
{
    public class BinaryTree<T> : ICollection<T>
    {
        private BinTreeNode<T> root;
        private IComparer<T> comparer;
        private int _count;

        public int Count { get; protected set; }

        public bool IsReadOnly { get { return false; } }

        public BinaryTree() : this(Comparer<T>.Default)
        {
            root = null;
        }
        public BinaryTree(IComparer<T> defaultComparer)
        {
            comparer = defaultComparer ?? throw new ArgumentNullException("Default comparer is null");
        }

        public T MinValue
        {
            get
            {
                if (root == null)
                    throw new InvalidOperationException("Tree is empty");
                var current = root;
                while (current.Left != null)
                    current = current.Left;
                return current.Value;
            }
        }
        public T MaxValue
        {
            get
            {
                if (root == null)
                    throw new InvalidOperationException("Tree is empty");
                var current = root;
                while (current.Right != null)
                    current = current.Right;
                return current.Value;
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var value in collection)
                Add(value);
        }

        public void Add(T value)
        {
            var node = new BinTreeNode<T>(value);
            if (root == null)
            {
                root = node;
            }
            else
            {
                BinTreeNode<T> current = root;
                BinTreeNode<T> parent = null;

                while (current != null)
                {
                    parent = current;
                    if (comparer.Compare(value, current.Value) < 0)
                        current = current.Left;
                    else
                        current = current.Right;
                }

                if (comparer.Compare(value, parent.Value) < 0)
                    parent.Left = node;
                else
                    parent.Right = node;
            }
        }

        public void Clear()
        {
            root = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            var current = root;
            while (current != null)
            {
                var result = comparer.Compare(item, current.Value);
                if (result == 0)
                    return true;
                else if (result < 0)
                    current = current.Left;
                else
                    current = current.Right;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var value in this)
                array[arrayIndex++] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Inorder().GetEnumerator();
        }

        public bool Remove(T item)
        {
            if (root == null)
                return false;

            BinTreeNode<T> current = root;
            BinTreeNode<T> parent = null;

            int result = 1;

            //найдём элемент в дереве.
            do
            {
                result = comparer.Compare(item, current.Value);
                if (result < 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result > 0)
                {
                    parent = current;
                    current = current.Right;
                }
                if (current == null)
                    return false;
            }
            while (result != 0);

            //если у элемента нет правого чилда
            if (current.Right == null)
            {
                if (current == root)
                    root = current.Left;
                else
                {
                    result = comparer.Compare(current.Value, parent.Value);
                    if (result < 0)
                        parent.Left = current.Left;
                    else
                        parent.Right = current.Left;
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (current == root)
                    root = current.Right;
                else
                {
                    result = comparer.Compare(current.Value, parent.Value);
                    if (result < 0)
                        parent.Left = current.Right;
                    else
                        parent.Right = current.Right;
                }
            }
            else
            {
                BinTreeNode<T> min = current.Right.Left, prev = current.Right;
                while (min.Left != null)
                {
                    prev = min;
                    min = min.Left;
                }
                prev.Left = min.Right;
                min.Left = current.Left;
                min.Right = current.Right;

                if (current == root)
                    root = min;
                else
                {
                    result = comparer.Compare(current.Value, parent.Value);
                    if (result < 0)
                        parent.Left = min;
                    else
                        parent.Right = min;
                }
            }
            --Count;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> Inorder()
        {
            if (root == null)
                yield break;

            var stack = new Stack<BinTreeNode<T>>();
            var node = root;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    yield return node.Value;
                    node = node.Right;
                }
                else
                {
                    stack.Push(node);
                    node = node.Left;
                }
            }
        }
        public IEnumerable<T> Preorder()
        {
            if (root == null)
                yield break;

            var stack = new Stack<BinTreeNode<T>>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                yield return node.Value;
                if (node.Right != null)
                    stack.Push(node.Right);
                if (node.Left != null)
                    stack.Push(node.Left);
            }
        }
        public IEnumerable<T> Postorder()
        {
            if (root == null)
                yield break;

            var stack = new Stack<BinTreeNode<T>>();
            var node = root;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    if (stack.Count > 0 && node.Right == stack.Peek())
                    {
                        stack.Pop();
                        stack.Push(node);
                        node = node.Right;
                    }
                    else
                    {
                        yield return node.Value;
                        node = null;
                    }
                }
                else
                {
                    if (node.Right != null)
                        stack.Push(node.Right);
                    stack.Push(node);
                    node = node.Left;
                }
            }
        }
        public IEnumerable<T> Levelorder()
        {
            if (root == null)
                yield break;

            var queue = new Queue<BinTreeNode<T>>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node.Value;
                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
        }
    }
}
