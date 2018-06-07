using Microsoft.AspNet.Identity;
using Suporteri.DataAccessLayer;
using Suporteri.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Suporteri.Controllers
{
    public class OrderController : Controller
    {
        List<Order> orders = new List<Order>();
        // GET: Order
        [Authorize(Roles = "Admin")]
        public ActionResult GetNextOrders(string orderBy, string order)
        {
            List<Order> list = new OrderDAL().GetNextOrders();
            list = list.OrderBy(p => p.UserEmail).ToList();
            List<User> users = new List<User>();
            string email = string.Empty;
            if (list.Count > 0)
            {
                foreach (var o in list)
                {
                    if (email != o.UserEmail)
                    {
                        users.Add(new ClientDAL().GetClientInfo(o.userId));
                    }
                    email = o.UserEmail;
                }
            }
            ViewBag.Users = users;
            list = SortOrderList(list, orderBy, order);
            return View(list);
        }

        // GET All Order
        [Authorize(Roles = "Admin")]
        public ActionResult GetAllOrders(string orderBy, string order)
        {
            List<Order> list = new OrderDAL().GetAllOrders();
            list = SortOrderList(list, orderBy, order);
            return View("GetAllOrders", list);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetLastWeekOrders(string orderBy, string order)
        {
            List<Order> list = new OrderDAL().GetLastWeekOrders();
            list = SortOrderList(list, orderBy, order);
            return View(list);
        }

        // GET: Order/Create
        [Authorize(Roles = "Client")]
        public ActionResult Create()
        {
            List<Product> list = new ProductDAL().GetProductList();
            foreach (var prod in list)
            {
                if (prod.ImagePath != null)
                {
                    prod.ImagePath = "/Images/2" + prod.ImagePath.Split('/').Last();
                }
            }
            ViewBag.Products = list;
            Order order = new Order();
            return View(order);
        }

        // POST: Order/Create
        [HttpPost]
        [Authorize(Roles = "Client")]
        public ActionResult Create(int quantity, int productId)
        {
            try
            {
                if (quantity > 0)
                {
                    var userId = User.Identity.GetUserId();
                    //new OrderDAL().Add(userId, productId, quantity, DateTime.Now, "");
                    if (orders == null && Session["Orders"] == null)
                    {
                        orders = new List<Order>();
                    }
                    else if (Session["Orders"] != null)
                    {
                        orders = Session["Orders"] as List<Order>;
                    }
                    Order order = new Order();
                    order.ProductId = productId;
                    order.Quantity = quantity;
                    order.Id = orders.Count;
                    orders.Add(order);
                    Session["Orders"] = orders;
                    return Json(new { success = true, responseText = "Comanda adaugata cu success!" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Comanda nu a putut fi adaugata! Alegeti alta cantitate." });
                }

            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public ActionResult SendOrder(DateTime deliveryDate, string deliveryMethod)
        {
            try
            {
                if (orders == null && Session["Orders"] == null)
                {
                    orders = new List<Order>();
                }
                else if (Session["Orders"] != null)
                {
                    orders = Session["Orders"] as List<Order>;
                }
                var userId = User.Identity.GetUserId();
                OrderDAL OrderDAL = new OrderDAL();
                foreach (Order o in orders)
                {
                    OrderDAL.Add(userId, o.ProductId, o.Quantity, deliveryDate, deliveryMethod);
                }
                orders = new List<Order>();
                Session["Orders"] = orders;
                //send sms 
                WebClient webClient = new WebClient();
                webClient.QueryString.Add("user", "cosmutamonica");
                webClient.QueryString.Add("pass", "Freed0mlife@");
                webClient.QueryString.Add("catre", "40763565144");
                webClient.QueryString.Add("dela", "DelicePan");
                webClient.QueryString.Add("mesaj", "Comanda a fost trimisa.Multumim!");

                //http://www.whosms.ro/send.php?user=test&pass=parolamea&catre=40722XXXXXX&mesaj=mesaj+test&dela=Companie&json=1

                string result = webClient.DownloadString("http://www.whosms.ro/send.php");

                return Json(new { success = true, responseText = "Comanda trimisa cu success!" });
            }
            catch
            {
                return Json(new { success = false, responseText = "Comanda nu a fost trimisa!" });
            }
        }

        // GET: Order
        [Authorize(Roles = "Client")]
        public ActionResult GetPendingOrders(string orderBy, string order)
        {
            List<Order> list = new List<Order>();
            if (Session["Orders"] != null)
            {
                list = Session["Orders"] as List<Order>;
            }
            List<Product> products = new ProductDAL().GetProductList();
            if (list.Count > 0)
            {
                foreach (Order orderToAdd in list)
                {
                    orderToAdd.ProductName = products.Where(p => p.ProductID == orderToAdd.ProductId).FirstOrDefault().ProductName;
                    orderToAdd.UnitPrice = products.Where(p => p.ProductID == orderToAdd.ProductId).FirstOrDefault().UnitPrice;
                    orderToAdd.Category = products.Where(p => p.ProductID == orderToAdd.ProductId).FirstOrDefault().Category;
                    orderToAdd.TotalPrice = orderToAdd.UnitPrice * orderToAdd.Quantity;
                }
                Session["Orders"] = list;
            }
            list = SortOrderList(list, orderBy, order);
            return View(list);
        }

        // GET: Order
        [Authorize(Roles = "Client")]
        public ActionResult GetClientOrders(string orderBy, string order)
        {
            string id = User.Identity.GetUserId();
            List<Order> list = new OrderDAL().GetClientOrders(id);
            list = SortOrderList(list, orderBy, order);
            return View(list);
        }

        [Authorize(Roles = "Client")]
        public ActionResult GetPreviousOrders(string orderBy, string order)
        {
            string id = User.Identity.GetUserId();
            List<Order> list = new OrderDAL().GetPreviousOrders(id);
            list = SortOrderList(list, orderBy, order);
            return View(list);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            List<Order> list = new List<Order>();
            if (Session["Orders"] != null)
            {
                list = Session["Orders"] as List<Order>;
            }
            Order order = list.Where(o => o.Id == id).FirstOrDefault();
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Order order)
        {
            try
            {
                List<Order> list = new List<Order>();
                if (Session["Orders"] != null)
                {
                    list = Session["Orders"] as List<Order>;
                }
                list.Where(o => o.Id == id).FirstOrDefault().Quantity = order.Quantity;
                Session["Orders"] = list;
                return RedirectToAction("GetPendingOrders");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            List<Order> list = new List<Order>();
            if (Session["Orders"] != null)
            {
                list = Session["Orders"] as List<Order>;
            }
            Order order = list.Where(o => o.Id == id).FirstOrDefault();
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Order order)
        {
            try
            {
                List<Order> list = new List<Order>();
                if (Session["Orders"] != null)
                {
                    list = Session["Orders"] as List<Order>;
                }
                list.Remove(list.Where(o => o.Id == id).FirstOrDefault());
                Session["Orders"] = list;
                return RedirectToAction("GetPendingOrders");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ClientInfo(string userId, string email)
        {
            User user = new ClientDAL().GetClientInfo(userId);
            user.Email = email;
            return View(user);
        }

        public List<Order> SortOrderList(List<Order> list, string orderBy, string order)
        {
            if (orderBy != null || orderBy != "")
            {
                switch (orderBy)
                {
                    case "Produs":
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
                    case " Data livrarii ":
                        if (order != orderBy)
                        {
                            list = list.OrderBy(p => p.DeliveryDate).ToList();
                        }
                        else
                        {
                            list = list.OrderByDescending(p => p.DeliveryDate).ToList();
                        }
                        break;
                    case "Data comenzii":
                        if (order != orderBy)
                        {
                            list = list.OrderBy(p => p.OrderDate).ToList();
                        }
                        else
                        {
                            list = list.OrderByDescending(p => p.OrderDate).ToList();
                        }
                        break;
                    case "Livrare":
                        if (order != orderBy)
                        {
                            list = list.OrderBy(p => p.DeliveryMethod).ToList();
                        }
                        else
                        {
                            list = list.OrderByDescending(p => p.DeliveryMethod).ToList();
                        }
                        break;
                    case "Utilizator":
                        if (order != orderBy)
                        {
                            list = list.OrderBy(p => p.UserEmail).ToList();
                        }
                        else
                        {
                            list = list.OrderByDescending(p => p.UserEmail).ToList();
                        }
                        break;
                }
            }
            ViewBag.Order = orderBy;
            return list;
        }
    }
}
