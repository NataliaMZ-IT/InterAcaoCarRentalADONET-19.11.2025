using CarRental.Controller.Interfaces;
using CarRental.Models;
using Microsoft.Data.SqlClient;
using Utils.Databases;

namespace CarRental.Controller
{
    public class VehicleController : IVehicleController
    {
        private readonly SqlConnection Connection = new(ConnectionDB.GetConnectionString());
        public void AddVehicle(Vehicle vehicle)
        {
            //var categoryController = new CategoryController();
            //var category = categoryController.FindCategoryByName() ??
            //    throw new Exception("Category not found!");

            Connection.Open();
            using (SqlTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    var command = new SqlCommand(Vehicle.INSERTVEHICLE, Connection, transaction);
                    command.Parameters.AddWithValue("@CategoryID", vehicle.CategoryID);
                    command.Parameters.AddWithValue("@LicensePlate", vehicle.LicensePlate);
                    command.Parameters.AddWithValue("@Make", vehicle.Make);
                    command.Parameters.AddWithValue("@Model", vehicle.Model);
                    command.Parameters.AddWithValue("@Year", vehicle.Year);
                    command.Parameters.AddWithValue("@VehicleStatus", vehicle.VehicleStatus);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error adding vehicle: " + ex.Message);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Unexpected error adding vehicle: " + ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        public List<Vehicle> ListAllVehicles()
        {
            Connection.Open();

            var categoryController = new CategoryController();
            List<Vehicle> vehicles = [];
            using (var command = new SqlCommand(Vehicle.SELECTALLVEHICLES, Connection))
            {
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var vehicle = new Vehicle(
                                reader.GetInt32(0), 
                                reader.GetString(1), 
                                reader.GetString(2), 
                                reader.GetString(3), 
                                reader.GetInt32(4), 
                                reader.GetString(5)
                                );
                            vehicle.SetCategoryName(
                                categoryController.FindCategoryNameById(
                                    vehicle.CategoryID)
                                );

                            vehicles.Add(vehicle);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error listing vehicles: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unexpected error listing vehicles: " + ex.Message);
                }
                finally
                {
                    Connection.Close();
                }

                return vehicles;
            }
        }

        public Vehicle FindVehicleLicensePlate(string plate)
        {
            Connection.Open();

            Vehicle vehicle = null;
            var categoryController = new CategoryController();
            using (var command = new SqlCommand(Vehicle.SELECTVEHICLEBYLPLATE, Connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@LicensePlate", plate);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            vehicle = new Vehicle(
                                reader.GetInt32(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetInt32(5),
                                reader.GetString(6)
                                );
                            vehicle.SetCategoryName(
                                categoryController.FindCategoryNameById(
                                    vehicle.CategoryID)
                                );
                            vehicle.SetVehicleID(reader.GetInt32(0));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error finding vehicle: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unexpected error finding vehicle: " + ex.Message);
                }
                finally
                {
                    Connection.Close();
                }

                return vehicle ?? throw new Exception("Vehicle not found");
            }
        }

        public void UpdateVehicleStatus(string vehicleStatus, string plate)
        {
            Vehicle vehicle = this.FindVehicleLicensePlate(plate) ??
                throw new Exception("Vehicle not found to update status!");

            Connection.Open();

            using (SqlTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    using (var command = new SqlCommand(Vehicle.UPDATEVEHICLESTATUS, Connection, transaction))
                    {
                        command.Parameters.AddWithValue("@VehicleStatus", vehicleStatus);
                        command.Parameters.AddWithValue("@VehicleID", vehicle.VehicleID);

                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error updating vehicle: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unexpected error updating vehicle: " + ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }

        public void DeleteVehicle(int vehicleId)
        {
            Connection.Open();

            using (SqlTransaction transaction = Connection.BeginTransaction())
            {
                try
                {
                    using (var command = new SqlCommand(Vehicle.DELETEVEHICLE, Connection, transaction))
                    {
                        command.Parameters.AddWithValue("@VehicleID", vehicleId);

                        command.ExecuteNonQuery();

                        transaction.Commit();
                    }
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error deleting vehicle: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unexpected error deleting vehicle: " + ex.Message);
                }
                finally
                {
                    Connection.Close();
                }
            }
        }
    }
}
