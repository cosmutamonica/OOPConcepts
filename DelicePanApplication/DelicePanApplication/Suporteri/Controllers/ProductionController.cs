using Suporteri.DataAccessLayer;
using Suporteri.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Suporteri.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductionController : Controller
    {
        // GET: Category
        public ActionResult DailyProduction(string orderBy, string order)
        {
            List<ProductionVModel> list = new ProductionDAL().GetDailyProduction();
            if (orderBy != null || orderBy != "")
            {
                switch (orderBy)
                {
                    case "Denumire":
                        if (order != orderBy)
                        {
                            list = list.OrderBy(p => p.ProductName).ToList();
                        }
                        else
                        {
                            list = list.OrderByDescending(p => p.ProductName).ToList();
                        }

                        break;
                    case "Cantitate":
                        if (order != orderBy)
                        {
                            list = list.OrderBy(p => p.Quantity).ToList();
                        }
                        else
                        {
                            list = list.OrderByDescending(p => p.Quantity).ToList();
                        }
                        break;
                    case "Data":
                        if (order != orderBy)
                        {
                            list = list.OrderBy(p => p.Date).ToList();
                        }
                        else
                        {
                            list = list.OrderByDescending(p => p.Date).ToList();
                        }
                        break;
                }
            }
            ViewBag.Order = orderBy;
            return View(list);
        }

        // GET: Weekly production
        public ActionResult WeeklyProduction(string orderBy, string order)
        {
            List<ProductionVModel> list = new ProductionDAL().GetWeeklyProduction();
            if (orderBy != null || orderBy != "")
            {
                switch (orderBy)
                {
                    case "Denumire":
                        if (order != orderBy)
                        {
                            list = list.OrderBy(p => p.ProductName).ToList();
                        }
                        else
                        {
                            list = list.OrderByDescending(p => p.ProductName).ToList();
                        }

                        break;
                    case "Cantitate":
                        if (order != orderBy)
                        {
                            list = list.OrderBy(p => p.Quantity).ToList();
                        }
                        else
                        {
                            list = list.OrderByDescending(p => p.Quantity).ToList();
                        }
                        break;
                    case "Data":
                        if (order != orderBy)
                        {
                            list = list.OrderBy(p => p.Date).ToList();
                        }
                        else
                        {
                            list = list.OrderByDescending(p => p.Date).ToList();
                        }
                        break;
                }
            }
            ViewBag.Order = orderBy;
            return View(list);
        }

        // GET: Category/Create
        public ActionResult AddProduction()
        {
            List<Product> list = new ProductDAL().GetProductList();
            foreach (var prod in list)
            {
                if (prod.ImagePath != null)
                {
                    //WebImage img = new WebImage(Server.MapPath(prod.ImagePath));
                    //if (img.Width > 300 || img.Height > 200)
                    //    img.Resize(300, 200);
                    //img.Save(HttpContext.Server.MapPath("~/Images/")+"2" +prod.ImagePath.Split('/').Last());
                    prod.ImagePath = "/Images/2" + prod.ImagePath.Split('/').Last();
                }
            }
            ViewBag.Products = list;
            ProductionVModel production = new ProductionVModel();
            return View(production);
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult AddProduction(int quantity, int productId)
        {
            try
            {
                if (quantity > 0)
                {
                    new ProductionDAL().Add(productId, quantity);
                }
                else
                {
                    ViewBag.Message = "Comanda nu a putut fi procesata!</br> Alegeti alta cantitate!";
                    return Json(new { success = false, responseText = "Comanda nu a putut fi procesata! Alegeti alta cantitate!" });
                }
                ViewBag.Message = "Adaugat cu success!";
                return Json(new { success = true, responseText = "Adaugat cu success!" });
            }
            catch
            {
                ViewBag.Message = "Comanda nu a fost procesata!";
                return Json(new { success = false, responseText = "Comanda nu a fost procesata!" });
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            ProductionVModel prod = new ProductionDAL().GetCustomProductionById(id);
            prod.ImagePath = "/Images/2" + prod.ImagePath.Split('/').Last();
            return View(prod);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ProductionVModel production)
        {
            try
            {
                new ProductionDAL().EditProduction(production);
                return RedirectToAction("DailyProduction");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            ProductionVModel prod = new ProductionDAL().GetCustomProductionById(id);
            prod.ImagePath = "/Images/2" + prod.ImagePath.Split('/').Last();
            return View(prod);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ProductionVModel production)
        {
            try
            {
                new ProductionDAL().DeleteProductionById(id);
                return RedirectToAction("DailyProduction");
            }
            catch
            {
                return View();
            }
        }
    }
}
