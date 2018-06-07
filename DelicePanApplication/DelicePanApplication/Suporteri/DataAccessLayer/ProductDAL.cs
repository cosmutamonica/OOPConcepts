using MySql.Data.MySqlClient;
using Suporteri.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Suporteri.DataAccessLayer
{
    public class ProductDAL
    {
        String connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<Product> GetProductList()
        {
            List<Product> list = new List<Product>();
            using (var context = new ProductContext())
            {
                // Perform data access using the context 
                list = context.Product.ToList();
            }
            return list;
        }

        public void AddProduct(Product product)
        {
            if (product != null)
            {
                using (var context = new ProductContext())
                {
                    // Perform data access using the context 
                    context.Product.Add(product);
                    context.SaveChanges();
                }
            }
        }

        public void Delete(int id)
        {
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "DeleteProductById";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters["@id"].Direction = ParameterDirection.Input;

                    con.Open();
                    cmd.ExecuteReader();
                }
            }
        }

        public void Edit(Product product)
        {
            if (product != null)
            {
                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "EditProduct";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id", product.ProductID);
                        cmd.Parameters["@id"].Direction = ParameterDirection.Input;

                        cmd.Parameters.AddWithValue("@prodName", product.ProductName);
                        cmd.Parameters["@prodName"].Direction = ParameterDirection.Input;

                        cmd.Parameters.AddWithValue("@price", product.UnitPrice);
                        cmd.Parameters["@price"].Direction = ParameterDirection.Input;
                                                
                        cmd.Parameters.AddWithValue("@ImagePath", product.ImagePath);
                        cmd.Parameters["@ImagePath"].Direction = ParameterDirection.Input;
                                                
                        cmd.Parameters.AddWithValue("@category", product.Category);
                        cmd.Parameters["@category"].Direction = ParameterDirection.Input;

                        cmd.Parameters.AddWithValue("@description", product.Description);
                        cmd.Parameters["@description"].Direction = ParameterDirection.Input;
                        
                        cmd.Parameters.AddWithValue("@showProd", product.Show);
                        cmd.Parameters["@showProd"].Direction = ParameterDirection.Input;
                        
                        con.Open();
                        cmd.ExecuteReader();
                    }
                }
            }
        }
    }
}