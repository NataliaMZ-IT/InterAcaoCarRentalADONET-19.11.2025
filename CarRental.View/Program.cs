using CarRental.Controller;
using CarRental.Models;
using CarRental.Models.Enums;

#region Customer/Document
//Customer customer = new("New Customer with Same Document", "newemail4@uol.com.br");
//Document document = new Document("CPF", "12345678988", new DateOnly(2022, 1, 1), new DateOnly(2032, 1, 1));

//var customerController = new CustomerController();

#region Add Customer
//// Add Customer
//try
//{
//    customerController.AddCustomer(customer, document);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

#region List Customers
//// List All Customers
//try
//{
//    var customers = customerController.ListCustomers();

//    foreach (var customerRead in customers)
//    {
//        Console.WriteLine(customerRead);
//        Console.WriteLine("-------------------------------");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

#region Update Customer
//// Update Customer Telephone
//customerController.UpdateCustomerTelephone("99999-9999", "newemail@uol.com.br");
#endregion

#region Find Customer
//// Find Customer by Email
//Console.WriteLine(customerController.FindCustomerByEmail("newemail@uol.com.br"));
#endregion

#region Delete Customer
//// Delete Customer
//try
//{
//    customerController.DeleteCustomerByEmail("newemail@uol.com.br");
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

#region Update Document
//// Update Customer Document
//try
//{
//    customerController.UpdateCustomerDocument(document, "newemail3@uol.com.br");
//    Console.WriteLine(customerController.FindCustomerByEmail("newemail3@uol.com.br"));
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
#endregion

#endregion

#region Category
//Category category = new("Fusca", 60.00m);

//var categoryController = new CategoryController();

#region Add Category
//// Add Category
//try
//{
//    categoryController.AddCategory(category);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

#region List Categories
//// List All Categories
//try
//{
//    var categories = categoryController.ListCategories();

//    foreach (var categoryRead in categories)
//    {
//        Console.WriteLine(categoryRead);
//        Console.WriteLine("-------------------------------");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

#region Update Category
//// Update Category Description
//categoryController.UpdateCategory("For those that want to punch others", "Fusca");

//// Update Category Daily Rental Rate
//categoryController.UpdateCategory(75m, "Fusca");
#endregion

#region Find Category
//// Find Category by Name
//Console.WriteLine(categoryController.FindCategoryByName("Fusca"));
#endregion

#region Delete Category
//// Delete Category
//try
//{
//    categoryController.DeleteCategory("Fusca");
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

#endregion

#region Vehicle
var vehicle = new Vehicle(1, "XYZ-9876", "Chevrolet", "S10", 2025, EVehicleStatus.Disponível.ToString());

var vehicleController = new VehicleController();

#region Add Vehicle
//// Add Vehicle
//try
//{
//    vehicleController.AddVehicle(vehicle);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

#region List Vehicles
//// List All Vehicles
//try
//{
//    var vehicles = vehicleController.ListAllVehicles();

//    foreach (var vehicleRead in vehicles)
//    {
//        Console.WriteLine(vehicleRead);
//        Console.WriteLine("------------------------------------------");
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

#region Update Vehicle
// Update Vehicle
try
{
    vehicleController.UpdateVehicleStatus(EVehicleStatus.Alugado.ToString(), "XYZ-9876");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
#endregion

#region Find Vehicle
//// Find Vehicle
//try
//{
//    Console.WriteLine(vehicleController.FindVehicleLicensePlate("XYZ-9876"));
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

#region Delete Vehicle
//// Delete Vehicle
//try
//{
//    var vehicleFound = vehicleController.FindVehicleLicensePlate("1");
//    vehicleController.DeleteVehicle(vehicleFound.VehicleID);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
#endregion

#endregion