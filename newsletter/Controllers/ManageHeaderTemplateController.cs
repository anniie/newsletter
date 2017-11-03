using System;
using System.Web.Mvc;
using newsletter.Models;

namespace newsletter.Controllers
{
    public class ManageHeaderTemplateController : Controller
    {
        // GET: ManageHeaderTemplate
        [HttpGet]
        public ActionResult Index()
        {
            Header obj = new Header();
            return View(obj.GetAll());
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection form)
        {
            Header obj = new Header();
            obj.TemplateTitle = form["TemplateTitle"];
            obj.TemplateContent = form["TemplateContent"];
            obj.Status = form["Status"] == "Enable" ? "True" : "False";
            obj.CreatedOn = Convert.ToDateTime(form["CreatedOn"]);
            obj.ModifiedOn = Convert.ToDateTime(form["CreatedOn"]);

            obj.AddHeader(obj);

            return View(obj.GetAll());
        }
    }
}