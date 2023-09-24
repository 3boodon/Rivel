using System;

namespace Rivel.Models {
    internal class Customer : BaseModel {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Address Details
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        // Identification
        public string DriverLicenseNumber { get; set; }
        public DateTime DriverLicenseExpiry { get; set; }
        public string PassportNumber { get; set; }  // Optional

        // Payment Information
        public string CreditCardNumber { get; set; }  // Store securely
        public DateTime CreditCardExpiry { get; set; }
        public int CVV { get; set; }  // Store securely

        // Rental History (Optional)
        public string PastRentals { get; set; }  // Could be a list or array
        public int LoyaltyPoints { get; set; }

        // Additional Information
        public DateTime DateOfBirth { get; set; }
        public string EmergencyContact { get; set; }
        public string Preferences { get; set; }  // Could be a list or array

        // Account Details
        public string Username { get; set; }
        public string Password { get; set; }  // Store securely

        // Status
        public bool IsActive { get; set; }
        public string VerificationStatus { get; set; }


    }
}

