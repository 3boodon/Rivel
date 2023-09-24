using System;

namespace Rivel.Models {
    internal class Accident : BaseModel {

        // Reference to Rental and Customer
        public int RentalID { get; set; }  // Foreign key linking to Rental model
        public int CustomerID { get; set; }  // Foreign key linking to Customer model

        // Car Information
        public int CarID { get; set; }  // Foreign key linking to Car model

        // Accident Details
        public string AccidentType { get; set; }  // e.g., "Collision", "Vandalism"
        public string Description { get; set; }  // Detailed description of the accident
        public decimal DamageCost { get; set; }  // Estimated or actual cost of damage

        // Location and Time
        public string Location { get; set; }  // Where the accident occurred
        public DateTime TimeOfAccident { get; set; }  // When the accident occurred

        // Status
        public string Status { get; set; }  // e.g., "Pending", "Resolved", "Under Investigation"

    }
}
