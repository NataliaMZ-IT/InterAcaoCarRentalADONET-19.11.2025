namespace CarRental.Models
{
    public class Customer
    {
        public readonly static string INSERTCUSTOMER = "INSERT INTO tblClientes (Nome, Email, Telefone) " +
                                                       "VALUES (@Name, @Email, @Telephone); " +
                                                       "SELECT SCOPE_IDENTITY();";

        public readonly static string SELECTALLCUSTOMERS = "SELECT * FROM tblClientes;";

        public int CustomerID { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Telephone { get; private set; } = String.Empty;

        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public Customer(string name, string email, string? telephone) : this(name, email)
        {
            Telephone = telephone;
        }

        public void SetCustomerID(int customerID)
        {
            CustomerID = customerID;
        }
        public override string? ToString()
        {
            return $"Name: {Name}\n" +
                   $"Email: {Email}\n" +
                   $"Telephone: {(Telephone == string.Empty ? "No telephone number" : Telephone)}\n";
        }
    }
}
