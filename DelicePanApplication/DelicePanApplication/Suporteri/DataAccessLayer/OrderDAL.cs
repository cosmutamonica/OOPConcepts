using Microsoft.AspNet.Identity;
using MySql.Data.MySqlClient;
using Suporteri.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Suporteri.DataAccessLayer
{
    public class OrderDAL
    {
        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public void Add(string userId, int productId, int quantity, DateTime deliveryDate, String deliveryMethod)
        {
            DateTime orderDate = DateTime.Now;
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "AddOrder";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters["@quantity"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@orderDate", DateTime.Now);
                    cmd.Parameters["@orderDate"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@deliveryDate", deliveryDate);
                    cmd.Parameters["@deliveryDate"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@deliveryMethod", deliveryMethod);
                    cmd.Parameters["@deliveryMethod"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@productId", productId);
                    cmd.Parameters["@productId"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters["@userId"].Direction = ParameterDirection.Input;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
        }

        public List<ProductionVModel> GetOrders(int userId)
        {
            List<Production> prodList = new List<Production>();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetOrders";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters["@userId"].Direction = ParameterDirection.Input;
                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Production product = new Production();
                                product.Date = (DateTime)reader["Date"];
                                product.ProductName = reader["ProductName"].ToString();
                                product.Quantity = (int)reader["Quantity"];
                                product.ProductionId = (int)reader["ProductionId"];
                                prodList.Add(product);

                            }
                            reader.NextResult();

                        }
                    }
                }
            }
            List<ProductionVModel> viewList = new List<ProductionVModel>();
            if (prodList.Count > 0)
            {
                List<Product> list = new ProductDAL().GetProductList();

                foreach (var production in prodList)
                {
                    Product product = list.Where(p => p.ProductName == production.ProductName).FirstOrDefault();
                    ProductionVModel model = new ProductionVModel();
                    model.ProductName = product.ProductName;
                    model.Quantity = production.Quantity;
                    model.Date = production.Date;
                    model.ImagePath = product.ImagePath;
                    model.ProductId = product.ProductID;
                    model.Category = product.Category;
                    model.Id = production.ProductionId;

                    if (model.Quantity > 0)
                    {
                        viewList.Add(model);
                    }
                }
            }
            else
            {
                return viewList;
            }

            return viewList.OrderBy(x => x.ProductName).ThenBy(x => x.Date).ToList();
        }

        public List<Order> GetNextOrders()
        {
            List<Order> orderList = new List<Order>();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetNextOrders";
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Order order = new Order();
                                order.Id = (int)reader["id"];
                                order.userId = reader["userId"].ToString();
                                order.UserEmail = reader["Email"].ToString();
                                order.ProductName = reader["ProductName"].ToString();
                                order.Quantity = (int)reader["Quantity"];
                                order.OrderDate = (DateTime)reader["OrderDate"];
                                order.DeliveryDate = (DateTime)reader["DeliveryDate"];
                                order.DeliveryMethod = reader["DeliveryMethod"].ToString();
                                orderList.Add(order);
                            }
                            reader.NextResult();
                        }
                    }
                }
            }

            return orderList.OrderBy(x => x.userId).ThenBy(x => x.DeliveryDate).ToList();
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orderList = new List<Order>();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetAllOrders";
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Order order = new Order();
                                order.Id = (int)reader["id"];
                                order.userId = reader["userId"].ToString();
                                order.UserEmail = reader["Email"].ToString();
                                order.ProductName = reader["ProductName"].ToString();
                                order.Quantity = (int)reader["Quantity"];
                                order.OrderDate = (DateTime)reader["OrderDate"];
                                order.DeliveryDate = (DateTime)reader["DeliveryDate"];
                                order.DeliveryMethod = reader["DeliveryMethod"].ToString();
                                orderList.Add(order);
                            }
                            reader.NextResult();
                        }
                    }
                }
            }

            return orderList.OrderByDescending(x => x.DeliveryDate).ToList();
        }

        public List<Order> GetLastWeekOrders()
        {
            List<Order> orderList = new List<Order>();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetLastWeekOrders";
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Order order = new Order();
                                order.Id = (int)reader["id"];
                                order.userId = reader["userId"].ToString();
                                order.UserEmail = reader["Email"].ToString();
                                order.ProductName = reader["ProductName"].ToString();
                                order.Quantity = (int)reader["Quantity"];
                                order.OrderDate = (DateTime)reader["OrderDate"];
                                order.DeliveryDate = (DateTime)reader["DeliveryDate"];
                                order.DeliveryMethod = reader["DeliveryMethod"].ToString();
                                orderList.Add(order);
                            }
                            reader.NextResult();
                        }
                    }
                }
            }

            return orderList.OrderByDescending(x => x.DeliveryDate).ToList();
        }

        public List<Order> GetClientOrders(string userId)
        {
            List<Order> orderList = new List<Order>();            
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetClientOrders";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Input;
                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Order order = new Order();
                                order.Id = (int)reader["id"];
                                order.ProductName = reader["ProductName"].ToString();
                                order.Quantity = (int)reader["Quantity"];
                                order.OrderDate = (DateTime)reader["OrderDate"];
                                order.DeliveryDate = (DateTime)reader["DeliveryDate"];
                                order.DeliveryMethod = reader["DeliveryMethod"].ToString();
                                orderList.Add(order);
                            }
                            reader.NextResult();
                        }
                    }
                }
            }

            return orderList.OrderBy(x => x.ProductName).ThenBy(x => x.DeliveryDate).ToList();
        }

        public List<Order> GetPreviousOrders(string userId)
        {
            List<Order> orderList = new List<Order>();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetPreviousOrders";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Input;
                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Order order = new Order();
                                order.Id = (int)reader["id"];
                                order.ProductName = reader["ProductName"].ToString();
                                order.Quantity = (int)reader["Quantity"];
                                order.OrderDate = (DateTime)reader["OrderDate"];
                                order.DeliveryDate = (DateTime)reader["DeliveryDate"];
                                order.DeliveryMethod = reader["DeliveryMethod"].ToString();
                                orderList.Add(order);
                            }
                            reader.NextResult();
                        }
                    }
                }
            }

            return orderList.OrderBy(x => x.ProductName).ThenBy(x => x.DeliveryDate).ToList();
        }
    }
}