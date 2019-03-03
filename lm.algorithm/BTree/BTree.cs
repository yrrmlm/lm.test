using System;
using System.Collections.Generic;
using System.Text;

namespace lm.algorithm.BTree
{
    public class BTree<T> where T : IComparable<T>
    {
        public TreeNode<T> _rootNode;

        private int _m = 6;

        private int _min = 3;

        public BTree(int m)
        {
            _rootNode = new TreeNode<T>();
            _m = m;
            _min = (int)Math.Ceiling((double)m / 2);
        }

        public void BTreeInsert(T key)
        {
            var treeNode = GetBeInsertLeaf(key, _rootNode);
            if(treeNode == null)
            {
                throw new Exception("找不到可以插入的叶子节点");
            }

            ToInsert(key, treeNode);
        }

        private void ToInsert(T key,TreeNode<T> curNode)
        {
            int pos = curNode.elementNum;
            for (int i = 0; i < curNode.elementNum; i++)
            {
                if (key.CompareTo(curNode.Elements[i]) < 0)
                {
                    pos = i;
                    break;
                }
            }

            curNode.Elements.Insert(pos, key);

            if(curNode.elementNum > _m - 1) //节点关键字超出数量，进行分裂
            {
                SplitNode(curNode);
            }
        }

        /// <summary>
        /// 分裂当前节点
        /// </summary>
        /// <param name="curNode"></param>
        private void SplitNode(TreeNode<T> curNode)
        {
            if (curNode == null) return;

            if (curNode.elementNum <= _m - 1) return;

            if(curNode.Parent == null)//没有父节点
            {
                var fKey = curNode.Elements[_min - 1];
                TreeNode<T> leftTree = new TreeNode<T>
                {
                    Parent = curNode
                };
                TreeNode<T> rightTree = new TreeNode<T>
                {
                    Parent = curNode
                };
                for (int i = 0; i < curNode.elementNum; i++)
                {
                    if (i < _min - 1)
                    {
                        leftTree.Elements.Add(curNode.Elements[i]);
                    }
                    if (i >= _min)
                    {
                        rightTree.Elements.Add(curNode.Elements[i]);
                    }
                }

                if(curNode.Pointer.Count > 0) //如果有孩子节点，将孩子节点复制
                {
                    for (int i = 0; i < curNode.Pointer.Count; i++)
                    {
                        if (i < _min)
                        {
                            if (curNode.Pointer.Count > i && curNode.Pointer[i] != null)
                            {
                                curNode.Pointer[i].Parent = leftTree;
                                leftTree.Pointer.Add(curNode.Pointer[i]);
                            }
                        }
                        if (i >= _min)
                        {
                            if (curNode.Pointer.Count >= i && curNode.Pointer[i] != null)
                            {
                                curNode.Pointer[i].Parent = rightTree;
                                rightTree.Pointer.Add(curNode.Pointer[i]);
                            }
                        }
                    }
                }

                curNode.Clear();
                curNode.IsLeaf = false;
                curNode.Elements.Add(fKey);
                curNode.Pointer.Add(leftTree);
                curNode.Pointer.Add(rightTree);
            }
            else
            {
                TreeNode<T> newTree = new TreeNode<T>
                {
                    Parent = curNode.Parent
                };
                var fKey = curNode.Elements[_min - 1];
                for(int i=0;i < curNode.elementNum; i++) //把当前节点后半页key赋值到新节点
                {
                    if (i >= _min)
                    {
                        newTree.Elements.Add(curNode.Elements[i]);
                    }
                }

                for(int j = curNode.elementNum - 1; j >= _min - 1; j--) //移除原节点后半页key
                {
                    curNode.Elements.RemoveAt(j);
                }

                var childSize = curNode.Pointer.Count;
                for(int i = childSize - 1; i >= _min; i--) //将后半页的孩子节点赋值到新的节点
                {
                    var temp = curNode.Pointer[i];
                    temp.Parent = newTree;
                    if(newTree.Pointer.Count <= 0)
                    {
                        newTree.Pointer.Add(temp);
                    }
                    else
                    {
                        newTree.Pointer.Insert(0, temp);
                    }
                }

                for(int j = childSize - 1; j >= _min; j--) //将原节点的后半页的孩子节点移除
                {
                    curNode.Pointer.RemoveAt(j);
                }

                int pos = curNode.Parent.elementNum;
                for (int i = 0; i < curNode.Parent.elementNum; i++) //找出分裂几点插入其父节点的位置
                {
                    if (fKey.CompareTo(curNode.Parent.Elements[i]) < 0)
                    {
                        pos = i;
                        break;
                    }
                }

                curNode.Parent.Elements.Insert(pos, fKey);
                curNode.Parent.Pointer.Insert(pos + 1, newTree);

                if(curNode.Parent.elementNum > _m - 1) //如果父节点满了，继续分裂
                {
                    SplitNode(curNode.Parent);
                }
            }
        }

        /// <summary>
        /// 寻找带插入的叶子节点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="curNode"></param>
        /// <returns></returns>
        public TreeNode<T> GetBeInsertLeaf(T key,TreeNode<T> curNode)
        {
            if (curNode.Pointer.Count == 0) //如果当前节点是叶子节点
            {
                return curNode;
            }
            else //如果是根节点，则找到可以插入的孩子节点
            {
                int pos = curNode.elementNum;
                for (int i = 0; i < curNode.elementNum; i++)
                {
                    if (key.CompareTo(curNode.Elements[i]) < 0)
                    {
                        pos = i;
                        break;
                    }
                }
                TreeNode<T> childNode = curNode.Pointer[pos];
                return GetBeInsertLeaf(key, childNode);
            }
        }
    }
}
