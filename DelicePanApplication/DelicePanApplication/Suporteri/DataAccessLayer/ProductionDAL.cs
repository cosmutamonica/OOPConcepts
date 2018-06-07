using MySql.Data.MySqlClient;
using Suporteri.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Suporteri.DataAccessLayer
{
    public class ProductionDAL
    {
        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<ProductionVModel> GetWeeklyProduction()
        {
            List<Production> prodList = new List<Production>();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetWeeklyProduction";
                    cmd.CommandType = CommandType.StoredProcedure;
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

            return viewList;
        }

        public List<ProductionVModel> GetProduction()
        {
            List<Production> prodList = new List<Production>();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetWeeklyProduction";
                    cmd.CommandType = CommandType.StoredProcedure;
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
                foreach (var product in list)
                {
                    if (product.Show == "True")
                    {
                        ProductionVModel model = new ProductionVModel();
                        model.ProductName = product.ProductName;
                        model.Quantity = 0;
                        foreach (var production in prodList)
                        {
                            if (model.ProductName == production.ProductName)
                                model.Quantity = model.Quantity + production.Quantity;
                        }
                        viewList.Add(model);
                    }
                }
            }
            else
            {
                return viewList;
            }

            return viewList;
        }

        public List<ProductionVModel> GetDailyProduction()
        {
            List<Production> prodList = new List<Production>();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetDailyProduction";
                    cmd.CommandType = CommandType.StoredProcedure;
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


        public void Add(int id, int quantity)
        {
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "InsertProduction";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@prodDate", DateTime.Now);
                    cmd.Parameters["@prodDate"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@prodId", id);
                    cmd.Parameters["@prodId"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters["@quantity"].Direction = ParameterDirection.Input;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
        }

        public Production GetProductionById(int id)
        {
            List<Production> prodList = new List<Production>();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetProductionByProductionId";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Input;

                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                Production product = new Production();
                                product.Date = (DateTime)reader["Date"];
                                product.ProductName = reader["ProductId"].ToString();
                                product.Quantity = (int)reader["Quantity"];
                                prodList.Add(product);
                            }
                            reader.NextResult();
                        }
                    }
                }
            }
            return prodList.FirstOrDefault();
        }

        /// <summary>
        /// Get Production with all details abaut product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductionVModel GetCustomProductionById(int id)
        {
            List<ProductionVModel> prodList = new List<ProductionVModel>();
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetCustomProductionById";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Input;

                    con.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ProductionVModel product = new ProductionVModel();
                                product.Date = (DateTime)reader["Date"];
                                product.ProductName = reader["ProductName"].ToString();
                                product.Quantity = (int)reader["Quantity"];
                                product.ImagePath = reader["ImagePath"].ToString();
                                prodList.Add(product);
                            }
                            reader.NextResult();
                        }
                    }
                }
            }

            return prodList.FirstOrDefault();
        }

        public void DeleteProductionById(int id)
        {
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "DeleteProductionById";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Input;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
        }

        public void EditProduction(ProductionVModel production)
        {
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "EditProductionById";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", production.Id);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@quantity", production.Quantity);
                    cmd.Parameters["@quantity"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("@prodDate", production.Date);
                    cmd.Parameters["@prodDate"].Direction = ParameterDirection.Input;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
        }
    }
}