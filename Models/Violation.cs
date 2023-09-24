using System;

namespace Rivel.Models {
    internal class Violation : BaseModel {

        // Reference to Rental Record and Customer
        public int RentalID { get; set; }  // Foreign key linking to Rental model
        public int CustomerID { get; set; }  // Foreign key linking to Customer model

        // Car Information
        public int CarID { get; set; }  // Foreign key linking to Car model

        // Violation Details
        public string ViolationType { get; set; }  // e.g., "Speeding", "Illegal Parking"
        public string Description { get; set; }  // Detailed description of the violation
        public decimal FineAmount { get; set; }  // Cost of the fine

        // Location and Time
        public string Location { get; set; }  // Where the violation occurred
        public DateTime TimeOfViolation { get; set; }  // When the violation occurred

        // Status
        public string Status { get; set; }  // e.g., "Unpaid", "Paid", "Disputed"

    }
}
