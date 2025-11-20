using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Models
{
    public class Vehicle
    {
        public int VehicleID { get; private set; }
        public int CategoryID { get; private set; }
        public string LicensePlate { get; private set; }
        public string Make {  get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string VehicleStatus { get; private set; }

        public Vehicle(int categoryID, string licensePlate, 
                        string make, string model, 
                        int year, string vehicleStatus)
        {
            CategoryID = categoryID;
            LicensePlate = licensePlate;
            Make = make;
            Model = model;
            Year = year;
            VehicleStatus = vehicleStatus;
        }

        public void SetVehicleID(int vehicleID)
        {
            VehicleID = vehicleID;
        }

        public void SetVehicleStatus(string vehicleStatus)
        {
            VehicleStatus = vehicleStatus;
        }

        // TODO: Add Vehicle category and show Status correctly
        public override string ToString()
        {
            return $"Vehicle: {Make} {Model}, {Year}\n" +
                $"License Plate: {LicensePlate}\n" +
                $"Status: {VehicleStatus}\n";
        }
    }
}
