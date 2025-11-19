using CarRental.Controller;
using CarRental.Models;

Customer customer = new("New Customer with Transaction", "newemail2@uol.com.br");
// Document document = new Document(1, "RG", "123456789", new DateOnly(2020, 1, 1), new DateOnly(2030, 1, 1));

var customerController = new CustomerController();

customerController.AddCustomer(customer);

var customers = customerController.ListCustomers();

foreach (var customerRead in customers)
{
    Console.WriteLine(customerRead);
    Console.WriteLine("-------------------------------");
}