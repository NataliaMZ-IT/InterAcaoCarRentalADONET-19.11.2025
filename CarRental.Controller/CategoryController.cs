using CarRental.Models;
using Microsoft.Data.SqlClient;
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

                List<Category> categories = [];
                using (SqlDataReader reader = command.ExecuteReader())
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

                using (SqlDataReader reader = command.ExecuteReader())
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

        public string FindCategoryNameById(int id)
        {
            Connection.Open();
            try
            {
                var command = new SqlCommand(Category.SELECTCATEGORYNAMEBYID, Connection);
                command.Parameters.AddWithValue("@CategoryID", id);

                string categoryName = String.Empty;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        categoryName = reader["Nome"].ToString() ?? string.Empty;
                    }
                    return categoryName;
                }
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

        public void UpdateCategory(string description, string name)
        {
            var category = this.FindCategoryByName(name) ??
                throw new Exception("Category not found!");

            category.SetDescription(description);

            Connection.Open();
            using (SqlTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(Category.UPDATECATEGORYBYNAME, Connection, transaction);
                    command.Parameters.AddWithValue("@Description", category.Description);
                    command.Parameters.AddWithValue("@DailyRate", category.DailyRate);
                    command.Parameters.AddWithValue("@Name", category.Name);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error updating category: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Unexpected error updating category: " + ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        public void UpdateCategory(decimal dailyRate, string name)
        {
            var category = this.FindCategoryByName(name) ??
                throw new Exception("Category not found!");

            category.SetDailyRate(dailyRate);

            Connection.Open();
            using (SqlTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(Category.UPDATECATEGORYBYNAME, Connection, transaction);
                    command.Parameters.AddWithValue("@Description", category.Description);
                    command.Parameters.AddWithValue("@DailyRate", category.DailyRate);
                    command.Parameters.AddWithValue("@Name", category.Name);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error updating category: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Unexpected error updating category: " + ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        public void DeleteCategory(string name) 
        {
            var category = this.FindCategoryByName(name) ??
                throw new Exception("Category not found!");

            Connection.Open();
            using (SqlTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(Category.DELETECATEGORYBYID, Connection, transaction);
                    command.Parameters.AddWithValue("@CategoryID", category.CategoryID);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error deleting category: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Unexpected error deleting category: " + ex.Message);
                }
                finally
                {
                    Connection.Close(); 
                }
            }
        }
    }
}
