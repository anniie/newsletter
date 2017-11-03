using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace newsletter.Controllers
{
    public class SendNewsletterController : Controller
    {
        // GET: SendNewsletter
        public ActionResult Index()
        {
            return View();
        }
    }
}