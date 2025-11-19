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
    }
}
