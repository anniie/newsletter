using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace newsletter.Controllers
{
    public class ManageNewsletterTemplateController : Controller
    {
        // GET: ManageMainTemplate
        public ActionResult Index()
        {
            return View();
        }
    }
}