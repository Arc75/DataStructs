using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTree
{
    class BinTreeNode<T>
    {
        public BinTreeNode(T value)
        {
            Value = value;
            Left = null;
            Right = null;
        }

        public BinTreeNode<T> Left { get; set; }
        public BinTreeNode<T> Right { get; set; }
        public T Value { get; private set; }
        
    }
}
