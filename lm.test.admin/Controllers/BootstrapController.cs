using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace lm.test.admin.Controllers
{
    public class BootstrapController : Controller
    {
        public PartialViewResult Dropdown()
        {
            return PartialView();
        }

        public PartialViewResult ButtonGroup()
        {
            return PartialView();
        }

        public PartialViewResult ButtonDropdown()
        {
            return PartialView();
        }
    }
}