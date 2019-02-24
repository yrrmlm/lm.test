using System;
using System.Collections.Generic;
using System.Text;

namespace lm.algorithm.BTree
{
    public class ReturnValue<T> where T : IComparable<T>
    {
        public TreeNode<T> node;
        public int position;
    }
}
