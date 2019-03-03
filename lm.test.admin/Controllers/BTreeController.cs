using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lm.algorithm.BTree;
using lm.test.admin.Models.Algorithm;
using Microsoft.AspNetCore.Mvc;

namespace lm.test.admin.Controllers
{
    public class BTreeController : Controller
    {
        private static BTree<int> bTree;

        public static TreeData treeData;

        public BTreeController()
        {
            if (bTree == null)
            {
                bTree = new BTree<int>(3);
                treeData = new TreeData
                {
                    data = new List<LeafData>()
                };
            }
        }

        public PartialViewResult Index()
        {
            SetTreeData(bTree);
            return PartialView();
        }

        public PartialViewResult GetTree()
        {
            return PartialView();
        }

        [ActionName("getOperate")]
        public PartialViewResult GetOperate()
        {
            return PartialView();
        }

        public PartialViewResult InsertKey(int key)
        {
            bTree.BTreeInsert(key);
            SetTreeData(bTree);
            return PartialView("Index");
        }

        public PartialViewResult DeleteKey(int key)
        {
            return PartialView();
        }

        public PartialViewResult SearchKey(int key)
        {
            return PartialView();
        }

        public PartialViewResult ClearTree()
        {
            bTree = new BTree<int>(3);
            treeData = new TreeData
            {
                data = new List<LeafData>()
            };

            return PartialView("Index");
        }

        private void SetTreeData(BTree<int> bTree)
        {
            treeData.data = new List<LeafData>();
            var treeNode = bTree._rootNode;
            if(treeNode.Elements != null && treeNode.Elements.Count> 0)
            {
                treeData.data.Add(new LeafData
                {
                    name = string.Join("|",treeNode.Elements),
                    value= string.Empty,
                    symbolSize = new List<int> { 40 + (treeNode.elementNum - 1) * 15, 40 },
                    children = GetLeafDatas(treeNode.Pointer)
                });
            }
        }

        private List<LeafData> GetLeafDatas(List<TreeNode<int>> treeNodes)
        {
            var list = new List<LeafData>();
            if (treeNodes == null || treeNodes.Count == 0) return list;     
            foreach(var tn in treeNodes)
            {
                if (tn != null && tn.elementNum > 0)
                {
                    list.Add(new LeafData
                    {
                        name = string.Join("|", tn.Elements),
                        value = string.Empty,
                        symbolSize = new List<int> { (tn.elementNum - 1) * 15 + 40, 40 },
                        children = GetLeafDatas(tn.Pointer)
                    });
                }
            }

            return list;
        }
    }
}