using Suporteri.DataAccessLayer;
using Suporteri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Suporteri.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult TopProduct()
        {
            List<ProductionVModel> products = new ProductionDAL().GetProduction();
            products = products.OrderByDescending(p => p.Quantity).ToList();
            List<string> allProducts = new List<string>();
            List<int> allQuantities = new List<int>();
            foreach (var p in products)
            {
                allProducts.Add(p.ProductName);
                allQuantities.Add(p.Quantity);
            }
            ViewBag.AllQuantities = allQuantities;
            return View(allProducts);
        }

        public ActionResult ProductionEvolution()
        {
            return View();
        }
        //public List<string> AllProducts()
        // {
        //     List<ProductionVModel> products = new ProductionDAL().GetProduction();
        //     List<string> allProducts = new List<string>();
        //     foreach(var p in products)
        //     {
        //         allProducts.Add(p.ProductName);
        //     }
        //     return allProducts;
        // }

        // public List<int> AllProductQuantities()
        // {
        //     List<ProductionVModel> products = new ProductionDAL().GetProduction();
        //     List<int> allProducts = new List<int>();
        //     foreach (var p in products)
        //     {
        //         allProducts.Add(p.Quantity);
        //     }
        //     return allProducts;
        // }
    }
}
