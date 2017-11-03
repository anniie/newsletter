using newsletter.Models;
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
            Newsletter obj = new Newsletter();
            return View(obj.GetAll());
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection form)
        {
            Newsletter obj = new Newsletter();
            obj.HeaderId = Convert.ToInt32(form["HeaderId"]);
            obj.FooterId = Convert.ToInt32(form["FooterId"]);
            obj.TemplateTitle = form["TemplateTitle"];
            obj.TemplateContent = form["TemplateContent"];
            obj.Status = form["Status"] == "Enable" ? "True" : "False";
            obj.CreatedOn = Convert.ToDateTime(form["CreatedOn"]);
            obj.ModifiedOn = Convert.ToDateTime(form["CreatedOn"]);

            obj.AddNewsletter(obj);

            return View(obj.GetAll());
        }
    }
}