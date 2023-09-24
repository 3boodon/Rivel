using System;

namespace Rivel.Models {
    internal class Car : BaseModel {

        // Basic Car Information
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string LicensePlate { get; set; }
        public int CarBrandID { get; set; }  // Foreign key linking to CarBrand model
        public int CarModelID { get; set; }  // Foreign key linking to CarModel


        // Car Specifications
        public string Color { get; set; }
        public string TransmissionType { get; set; }  // e.g., Automatic, Manual
        public int Mileage { get; set; }
        public string FuelType { get; set; }  // e.g., Gasoline, Diesel, Electric

        // Rental Details
        public decimal DailyRentalRate { get; set; }
        public bool IsAvailableForRent { get; set; }
        public string CurrentRentalLocation { get; set; }

        // Maintenance Information
        public DateTime LastMaintenanceDate { get; set; }
        public string MaintenanceHistory { get; set; }  // Could be a serialized list or JSON

        // Car Condition
        public string CurrentCondition { get; set; }  // e.g., Excellent, Good, Needs Repair
    }
}
