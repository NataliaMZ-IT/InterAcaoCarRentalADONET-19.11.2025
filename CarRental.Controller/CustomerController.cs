using CarRental.Models;
using Microsoft.Data.SqlClient;
using Utils.Databases;

namespace CarRental.Controller
{
    public class CustomerController
    {
        private readonly SqlConnection Connection = new(ConnectionDB.GetConnectionString());

        public void AddCustomer(Customer customer)
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

                    int customerId = Convert.ToInt32(command.ExecuteScalar());
                    customer.SetCustomerID(customerId);

                    //customer.SetCustomerID(Convert.ToInt32(command.ExecuteScalar()));

                    transaction.Commit();
                }
                catch (Exception ex) 
                {
                    transaction.Rollback();
                    throw new Exception("Error when adding customer " + ex.Message);
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
                        customer.SetCustomerID(Convert.ToInt32(reader["ClienteID"]));

                        customers.Add(customer);
                    }
                }
                return customers;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro when listing customers: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error when listing customers" + ex.Message);
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
                throw new Exception("Error when finding customer by email: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error when finding customer by email: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        public void UpdateCustomerTelephone(string telephone, string email)
        {
            //TODO: Create FindCustomerByEmail
            var customerFound = this.FindCustomerByEmail(email);
            
            if (customerFound is null)
                throw new Exception("No customer found!");

            customerFound.SetTelephone(telephone);

            Connection.Open();
            try
            {
                var command = new SqlCommand(Customer.UPDATECUSTOMERTELEPHONE, Connection);
                command.Parameters.AddWithValue("@Telephone", customerFound.Telephone);
                command.Parameters.AddWithValue("@CustomerID", customerFound.CustomerID);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error when updating customer's telephone: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error when updating customer's telephone: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

        }
    }
}
