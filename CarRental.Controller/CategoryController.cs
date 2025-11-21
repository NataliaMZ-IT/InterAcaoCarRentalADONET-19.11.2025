using CarRental.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Utils.Databases;

namespace CarRental.Controller
{
    public class CategoryController
    {
        private readonly SqlConnection Connection = new(ConnectionDB.GetConnectionString());

        public void AddCategory(Category category)
        {
            Connection.Open();
            using (SqlTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(Category.INSERTCATEGORY, Connection, transaction);

                    command.Parameters.AddWithValue("@Name", category.Name);
                    command.Parameters.AddWithValue("@Description", category.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DailyRate", category.DailyRate);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error adding category: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Unexpected error adding category: " + ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        public List<Category> ListCategories()
        {
            Connection.Open();
            try
            {
                var command = new SqlCommand(Category.SELECTALLCATEGORIES, Connection);

                SqlDataReader reader = command.ExecuteReader();

                List<Category> categories = [];
                using (reader)
                {
                    while (reader.Read())
                    {
                        var category = new Category(reader["Nome"].ToString(),
                                                    reader["Descricao"].ToString(),
                                                    Convert.ToDecimal(reader["Diaria"]));

                        categories.Add(category);
                    }
                }
                return categories;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error listing categories: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error listing categories: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        public Category? FindCategoryByName(string name)
        {
            Connection.Open();
            try
            {
                var command = new SqlCommand(Category.SELECTCATEGORYBYNAME, Connection);

                command.Parameters.AddWithValue("@Name", name);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        var category = new Category(reader["Nome"].ToString(),
                                                    reader["Descricao"].ToString(),
                                                    Convert.ToDecimal(reader["Diaria"]));
                        category.SetCategoryID(Convert.ToInt32(reader["CategoriaID"]));

                        return category;
                    }
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error finding category: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error finding category: " + ex.Message);
            }
            finally
            {
                Connection.Close(); 
            }
        }
    }
}
