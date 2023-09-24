using System;

namespace Rivel.Models {
    internal class Maintenance : BaseModel {


        // Car Information
        public int CarID { get; set; }  // Foreign key linking to Car model

        public int RentalID { get; set; }  // Foreign key linking to Rental model


        // Maintenance Details
        public string MaintenanceType { get; set; }  // e.g., "Oil Change", "Tire Rotation"
        public string Description { get; set; }  // Detailed description of the maintenance
        public decimal Cost { get; set; }  // Cost of the maintenance

        // Maintenance Date and Location
        public DateTime MaintenanceDate { get; set; }  // When the maintenance occurred
        public string Location { get; set; }  // Where the maintenance was performed

        // Status
        public string Status { get; set; }  // e.g., "Completed", "Scheduled", "In Progress"

    }
}
