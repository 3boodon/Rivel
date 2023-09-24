using System;

namespace Rivel.Models {
    internal class Rental : BaseModel {


        public int WarrantyID { get; set; }  // Foreign key linking to Warranty model

        // Reference to Reservation and Customer
        public int ReservationID { get; set; }  // Foreign key linking to Reservation model
        public int CustomerID { get; set; }  // Foreign key linking to Customer model

        // Car Information
        public int CarID { get; set; }  // Foreign key linking to Car model

        // Actual Rental Period
        public DateTime ActualPickupDate { get; set; }
        public DateTime ActualReturnDate { get; set; }

        // Rental Locations
        public string ActualPickupLocation { get; set; }
        public string ActualReturnLocation { get; set; }

        // Payment and Charges
        public decimal FinalTotalCost { get; set; }
        public decimal AdditionalCharges { get; set; }  // e.g., Late return fees, damage charges

        // Rental Status and Notes
        public string RentalStatus { get; set; }  // e.g., Completed, In Progress, Closed
        public string Notes { get; set; }  // e.g., Any damage reports, customer feedback

    }
}
