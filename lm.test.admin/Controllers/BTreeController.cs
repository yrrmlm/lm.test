using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lm.algorithm.BTree;
using Microsoft.AspNetCore.Mvc;

namespace lm.test.admin.Controllers
{
    public class BTreeController : Controller
    {
        private static BTree<int> bTree;

        public BTreeController()
        {
            bTree = new BTree<int>();
            bTree.BtreeInsert(5);
        }

        public PartialViewResult Index()
        {
            return PartialView(bTree);
        }

        [ActionName("getTree")]
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
            return PartialView();
        }

        public PartialViewResult DeleteKey(int key)
        {
            return PartialView();
        }

        public PartialViewResult SearchKey(int key)
        {
            return PartialView();
        }
    }
}