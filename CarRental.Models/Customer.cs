namespace CarRental.Models
{
    public class Customer
    {
        public readonly static string INSERTCUSTOMER = "INSERT INTO tblClientes VALUES (@Name, @Email, @Telephone); " +
                                                       "SELECT SCOPE_IDENTITY();";

        public readonly static string SELECTALLCUSTOMERS = @"SELECT c.Nome, c.Email, c.Telefone, 
                                                            d.TipoDocumento, d.Numero, d.DataEmissao, d.DataValidade 
                                                            FROM tblClientes c
                                                            JOIN tblDocumentos d
                                                            ON c.ClienteID = d.ClienteID;";

        public readonly static string SELECTCUSTOMERBYEMAIL = "SELECT * FROM tblClientes WHERE Email = @Email;";

        public readonly static string UPDATECUSTOMERTELEPHONE = "UPDATE tblClientes SET Telefone = @Telephone " +
                                                                "WHERE ClienteID = @CustomerID;";

        public readonly static string DELETECUSTOMERBYEMAIL = "DELETE FROM tblClientes WHERE ClienteID = @CustomerID;";

        public int CustomerID { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string? Telephone { get; private set; } = String.Empty;
        public Document? Document { get; private set; }

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

        public void SetTelephone(string telephone)
        {
            Telephone = telephone;
        }

        public void SetDocument(Document document)
        {
            Document = document;
        }

        public override string? ToString()
        {
            return $"Name: {Name}\n" +
                   $"Email: {Email}\n" +
                   $"Telephone: {(Telephone == string.Empty ? "No telephone number" : Telephone)}\n" +
                   $"{Document}\n";
        }
    }
}
