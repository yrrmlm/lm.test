/***
 * Date：2019-02-24
 * Desc：BTree的C#实现
 * Remark:引用自：https://www.cnblogs.com/CareySon/archive/2012/04/06/Imple-BTree-With-CSharp.html
 https://blog.csdn.net/endlu/article/details/51720299
  * ***/
using System;
using System.Collections.Generic;
using System.Text;

namespace lm.algorithm.BTree
{
    public class BTree<T> where T : IComparable<T>
    {
        public TreeNode<T> RootNode;

        const int NumPerNode = 4; //4阶b树

        /// <summary>
        /// 创建一个b树,也是类的构造函数
        /// </summary>
        public BTree()
        {

            RootNode = new TreeNode<T>
            {
                elementNum = 0,
                IsLeaf = true,
                Elements = new List<T> { }
            };
        }

        /// <summary>
        /// 将一个元素插入B树
        /// </summary>
        /// <param name="KeyWord"></param>
        public void BtreeInsert(T KeyWord)
        {
            if (RootNode.elementNum == NumPerNode)
            {
                //如果根节点满了，则对跟节点进行分裂
                TreeNode<T> newRoot = new TreeNode<T>();
                newRoot.elementNum = 0;
                newRoot.IsLeaf = false;
                newRoot.Pointer.Add(RootNode);
                //将newRoot节点变为根节点
                BTreeSplitNode(newRoot, 0, RootNode);
                //分裂后插入新根的树
                BTreeInsertNotFull(newRoot, KeyWord);
                //将树的根进行变换
                RootNode = newRoot;
            }
            else
            {
                //如果根节点没有满，直接插入
                BTreeInsertNotFull(RootNode, KeyWord);
            }
        }

        /// <summary>
        /// 在节点非满时寻找插入节点
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="KeyWord"></param>
        public void BTreeInsertNotFull(TreeNode<T> Node, T KeyWord)
        {
            if (Node.IsLeaf)//如果是叶子节点
            {
                int pos = Node.elementNum;
                for (int j = 0; j < Node.elementNum; j++)//寻找合适的位置
                {
                    if (KeyWord.CompareTo(Node.Elements[j]) < 0)
                    {
                        pos = j;
                        break;
                    }
                }

                Node.Elements.Insert(pos, KeyWord); //将关键字插入节点
                Node.elementNum += 1;
            }
            else //如果是非叶子节点
            {
                int pos = Node.elementNum;
                for (int i = 0; i < Node.elementNum; i++)
                {
                    if (KeyWord.CompareTo(Node.Elements[i]) < 0)
                    {
                        pos = i;
                        break;
                    }
                }

                //这步将指针所指向的节点读入内存,IO读+1
                if (Node.Pointer[pos].elementNum == NumPerNode)
                {
                    //如果子节点已满，进行节点分裂
                    BTreeSplitNode(Node, pos, Node.Pointer[pos]);
                    if (KeyWord.CompareTo(Node.Elements[pos]) > 0)
                    {
                        //根据关键字的值决定插入分裂后的左孩子还是右孩子
                        pos += 1;
                    }
                }

                //迭代找叶子，找到叶子节点后插入
                BTreeInsertNotFull(Node.Pointer[pos], KeyWord);
            }
        }

        /// <summary>
        /// 从B树中搜索节点，存在则返回节点和元素在节点的值，否则返回NULL
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ReturnValue<T> BTreeSearch(TreeNode<T> rootNode, T keyword)
        {
            int i = 1;

            while (i <= rootNode.elementNum && keyword.CompareTo(rootNode.Elements[i - 1]) > 0)
            {
                i = i + 1;
            }
            if (i <= rootNode.elementNum && keyword.CompareTo(rootNode.Elements[i - 1]) == 0)
            {
                ReturnValue<T> r = new ReturnValue<T>();
                r.node = rootNode.Pointer[i];
                r.position = i;
                return r;
            }
            if (rootNode.IsLeaf)
            {
                return null;
            }
            else
            {
                //从磁盘将内容读出来,做一次IO读
                return BTreeSearch(rootNode.Pointer[i], keyword);
            }
        }

        /// <summary>
        /// B树中的节点分裂
        /// </summary>
        /// <param name="FatherNode">父节点</param>
        /// <param name="position">分裂点</param>
        /// <param name="NodeToBeSplit">被分裂节点</param>
        public void BTreeSplitNode(TreeNode<T> FatherNode, int position, TreeNode<T> NodeToBeSplit)
        {
            TreeNode<T> newNode = new TreeNode<T>();//创建新节点，容纳分裂后被移动的元素
            newNode.IsLeaf = NodeToBeSplit.IsLeaf;
            newNode.elementNum = NumPerNode - (NumPerNode / 2 + 1);
            for (int i = 1; i <= newNode.elementNum; i++)//将原页中后半部分复制到新页中
            {           
                newNode.Elements.Insert(i-1,NodeToBeSplit.Elements[i + NumPerNode / 2]);
                NodeToBeSplit.Elements.RemoveAt(i + NumPerNode / 2); //被分裂节点删除对应元素
            }

            if (!NodeToBeSplit.IsLeaf)//如果不是叶子节点，将指针也复制过去
            {
                for (int j = 1; j < NumPerNode / 2 + 1; j++)
                {
                    newNode.Pointer.Insert(j - 1,NodeToBeSplit.Pointer[NumPerNode / 2]);
                }
            }

            //将新节点插入父节点指针中
            FatherNode.Pointer.Insert(position+1, newNode);

            //将被分裂的页的中间节点插入父节点
            FatherNode.Elements.Insert(position,NodeToBeSplit.Elements[NumPerNode / 2]);

            //原节点删除插入父节点元素
            NodeToBeSplit.Elements.RemoveAt(NumPerNode / 2);

            //原节点剩余元素个数
            NodeToBeSplit.elementNum = NumPerNode / 2;

            //父节点元素大小+1
            FatherNode.elementNum += 1;
        }
    }
}
