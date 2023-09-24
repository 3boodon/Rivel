using System;

namespace Rivel.Models {
    internal class RentalPaymentTransaction : BaseModel {


        // Reference to Rental and Customer
        public int RentalID { get; set; }  // Foreign key linking to Rental model
        public int CustomerID { get; set; }  // Foreign key linking to Customer model

        // Payment Details
        public decimal AmountPaid { get; set; }  // Amount collected
        public string PaymentMethod { get; set; }  // e.g., "Credit Card", "Cash", "Bank Transfer"

        public int PaymentMethodID { get; set; }  // Foreign key linking to PaymentMethod model

        public DateTime PaymentDate { get; set; }  // When the payment was made

        // Additional Charges (Optional)
        public decimal AdditionalCharges { get; set; }  // e.g., Late fees, damage charges

        // Receipt Information (Optional)
        public string ReceiptNumber { get; set; }  // Receipt or transaction number

        // Payment Status
        public string PaymentStatus { get; set; }  // e.g., "Paid", "Pending", "Overdue"

        public string TransactionType { get; set; }  // "Deposit", "Full Payment", "Refund"


    }
}
