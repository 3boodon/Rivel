using System;

namespace Rivel.Models {
    internal class Reservation : BaseModel {


        // Customer and Car Information
        public int CustomerID { get; set; }  // Foreign key linking to Customer model
        public int CarID { get; set; }  // Foreign key linking to Car model

        // Rental Period
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }

        // Rental Location
        public string PickupLocation { get; set; }
        public string ReturnLocation { get; set; }

        // Payment Information
        public decimal TotalCost { get; set; }
        public string PaymentMethod { get; set; }  // e.g., Credit Card, Cash

        // Reservation Status
        public string Status { get; set; }  // e.g., Confirmed, Cancelled, Completed

        // Optional Extras
        public string OptionalExtras { get; set; }  // e.g., GPS, Child Seat; could be a serialized list or JSON

    }
}
