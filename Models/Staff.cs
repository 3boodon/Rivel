using System;

namespace Rivel.Models {
    internal class Staff : BaseModel {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Position and Role
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public int RoleID { get; set; }  // Foreign key linking to Role model


        // Employment Details
        public DateTime DateOfHire { get; set; }
        public DateTime? DateOfTermination { get; set; }  // Nullable for active employees
        public string EmploymentStatus { get; set; }

        // Address Details
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // Identification
        public string EmployeeNumber { get; set; }


        // Account Details
        public string Username { get; set; }
        public string Password { get; set; }  // Store securely

    }
}
