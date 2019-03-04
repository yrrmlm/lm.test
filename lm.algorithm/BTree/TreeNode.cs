using System;
using System.Collections.Generic;
using System.Text;

namespace lm.algorithm.BTree
{
    public class TreeNode<T> where T : IComparable<T>
    {
        public int elementNum { get { return Elements == null ? 0 : Elements.Count; } }//元素个数
        public List<T> Elements = new List<T>();//元素集合,存在elementNum个
        public List<TreeNode<T>> Pointer = new List<TreeNode<T>>();//元素指针，存在elementNum+1
        public bool IsLeaf = true;//是否为叶子节点
        public TreeNode<T> Parent = null;//当前节点的父节点
        public bool BeFind = false;


        public void Clear()
        {
            this.Elements = new List<T>();
            this.Pointer = new List<TreeNode<T>>();
            Parent = null;
        }
    }
}
