using System;
using System.Web.Mvc;
using newsletter.Models;

namespace newsletter.Controllers
{
    public class ManageFooterTemplateController : Controller
    {
        // GET: ManageFooterTemplate
        [HttpGet]
        public ActionResult Index()
        {
            Footer obj = new Footer();
            return View(obj.GetAll());
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection form)
        {
            Footer obj = new Footer();
            obj.TemplateTitle = form["TemplateTitle"];
            obj.TemplateContent = form["TemplateContent"];
            obj.Status = form["Status"] == "Enable" ? "True" : "False";
            obj.CreatedOn = Convert.ToDateTime(form["CreatedOn"]);
            obj.ModifiedOn = Convert.ToDateTime(form["CreatedOn"]);

            obj.AddFooter(obj);

            return View(obj.GetAll());
        }
    }
}