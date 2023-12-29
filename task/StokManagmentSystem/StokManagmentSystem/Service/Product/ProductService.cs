using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using StokManagmentSystem.Models;

namespace StokManagmentSystem.Service
{
    public class ProductService
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private readonly ILogger logger;


        public bool SaveProductInfo(ProductModel productModel)
        {
            bool isSaved = false;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    
                    string checkIfExistsQuery = "SELECT COUNT(*) FROM product WHERE ProductCode = @ProductCode";
                    MySqlCommand checkIfExistsCommand = new MySqlCommand(checkIfExistsQuery, connection);
                    checkIfExistsCommand.Parameters.AddWithValue("@ProductCode", productModel.productcode);

                    int existingProductCount = Convert.ToInt32(checkIfExistsCommand.ExecuteScalar());

                    if (existingProductCount == 0)
                    {
                       
                        string insertQuery = "INSERT INTO product (Productcode, productName) VALUES (@ProductCode, @ProductName)";
                        MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);

                        insertCommand.Parameters.AddWithValue("@ProductCode", productModel.productcode);
                        insertCommand.Parameters.AddWithValue("@ProductName", productModel.ProductName);

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            isSaved = true;
                        }
                    }
                    else
                    {
                        isSaved = false;
                        Console.WriteLine("Product already exists.");
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return isSaved;
        }

        public ProductModel GetAllProductInfoById(int productcode)
        {
           
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM product WHERE Productcode = @ProductCode";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductCode", productcode);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductModel product = new ProductModel
                            {
                                productcode = Convert.ToInt32(reader["Productcode"]),
                                ProductName = reader["productName"].ToString(),
                                // Add other properties from the database to the ProductModel
                            };

                            return product;
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                // Consider logging the exception or handling it appropriately
            }

            return null;
        }



        public bool DeleteProductInfo(int id)
        {
            bool isDeleted = false;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM product WHERE Productcode = @ProductId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductId", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    isDeleted = rowsAffected > 0;

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                
            }

            return isDeleted;
        }

        public bool EditProductInfo(int id, ProductModel productinfo)
        {
            bool isUpdated = false;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE product SET productName = @ProductName WHERE Productcode = @ProductCode";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductName", productinfo.ProductName);
                    command.Parameters.AddWithValue("@ProductCode", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        isUpdated = true;
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return isUpdated;
        }

        public List<ProductModel> GetAllProductInfo()
        {
            List<ProductModel> productList = new List<ProductModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT ProductCode, ProductName FROM product"; 
                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductModel product = new ProductModel
                            {
                                productcode = reader.GetInt32(0),
                                ProductName = reader.GetString(1)

                            };
                            productList.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
               
            }

            return productList;
        }
    }
}