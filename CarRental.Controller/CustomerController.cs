using CarRental.Models;
using Microsoft.Data.SqlClient;
using Utils.Databases;

namespace CarRental.Controller
{
    public class CustomerController
    {
        private readonly SqlConnection Connection = new(ConnectionDB.GetConnectionString());

        public void AddCustomer(Customer customer, Document document)
        {
            Connection.Open();
            using (SqlTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(Customer.INSERTCUSTOMER, Connection, transaction);

                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@Telephone", customer.Telephone ?? (object)DBNull.Value);

                    int customerID = Convert.ToInt32(command.ExecuteScalar());
                    customer.SetCustomerID(customerID);

                    var documentController = new DocumentController();

                    document.SetCustomerID(customerID);
                    documentController.AddDocument(document, Connection, transaction);

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error adding customer: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Unexpected error adding customer: " + ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        public List<Customer> ListCustomers()
        {
            Connection.Open();

            try
            {
                SqlCommand command = new SqlCommand(Customer.SELECTALLCUSTOMERS, Connection);

                SqlDataReader reader = command.ExecuteReader();
                
                List<Customer> customers = [];
                using (reader)
                {
                    while (reader.Read())
                    {
                        var customer = new Customer(reader["Nome"].ToString(),
                                                    reader["Email"].ToString(),
                                                    reader["Telefone"] != DBNull.Value ?
                                                    reader["Telefone"].ToString() : null
                                                    );
                        //customer.SetCustomerID(Convert.ToInt32(reader["ClienteID"]));

                        var document = new Document(reader["TipoDocumento"].ToString(),
                                                    reader["Numero"].ToString(),
                                                    DateOnly.FromDateTime(reader.GetDateTime(5)),
                                                    DateOnly.FromDateTime(reader.GetDateTime(6))
                                                    );
                        customer.SetDocument(document);

                        customers.Add(customer);
                    }
                }
                return customers;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro listing customers: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error listing customers" + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        public Customer? FindCustomerByEmail(string email)
        {
            Connection.Open();
            try
            {
                var command = new SqlCommand(Customer.SELECTCUSTOMERBYEMAIL, Connection);

                command.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var customer = new Customer(reader["Nome"].ToString(),
                                                reader["Email"].ToString(),
                                                reader["Telefone"] != DBNull.Value ?
                                                reader["Telefone"].ToString() : null
                                                );
                    customer.SetCustomerID(Convert.ToInt32(reader["ClienteID"]));
                    return customer;
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error finding customer by email: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error finding customer by email: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        public void UpdateCustomerTelephone(string telephone, string email)
        {
            var customerFound = this.FindCustomerByEmail(email);

            if (customerFound is null)
                throw new Exception("Customer not found!");

            customerFound.SetTelephone(telephone);

            Connection.Open();
            using (SqlTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(Customer.UPDATECUSTOMERTELEPHONE, Connection, transaction);
                    command.Parameters.AddWithValue("@Telephone", customerFound.Telephone);
                    command.Parameters.AddWithValue("@CustomerID", customerFound.CustomerID);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error updating customer telephone: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Unexpected error updating customer telephone: " + ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        public void DeleteCustomer(string email)
        {
            var customer = this.FindCustomerByEmail(email);

            if (customer is null)
                throw new Exception("Customer not found!");

            Connection.Open();
            using (SqlTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(Customer.DELETECUSTOMERBYEMAIL, Connection, transaction);
                    command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error deleting customer: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Unexpected error deleting customer: " + ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }
    }
}
