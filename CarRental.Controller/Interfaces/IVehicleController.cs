using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Controller.Interfaces
{
    public interface IVehicleController
    {
        public void AddVehicle(Vehicle vehicle);

        public List<Vehicle> ListAllVehicles();

        public Vehicle FindVehicleLicensePlate(string plate);

        public void UpdateVehicleStatus(string vehicleStatus, string plate);

        public void DeleteVehicle(int vehicleId);
    }
}
