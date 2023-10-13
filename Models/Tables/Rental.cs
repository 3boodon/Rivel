using System;

namespace Rivel.Models
{
    public class Rental : Timestamps
    {
        public Rental(int rentalId, int warrantyId, int userId, int carId, DateTime pickupDate, DateTime returnDate, int paymentId)
        {
            RentalId = rentalId;
            WarrantyId = warrantyId;
            UserId = userId;
            CarId = carId;
            PickupDate = pickupDate;
            ReturnDate = returnDate;
            PaymentId = paymentId;
        }
        public Rental(int warrantyId, int userId, int carId, DateTime pickupDate, DateTime returnDate, int paymentId)
        {
            WarrantyId = warrantyId;
            UserId = userId;
            CarId = carId;
            PickupDate = pickupDate;
            ReturnDate = returnDate;
            PaymentId = paymentId;
        }
        public int RentalId { get; set; }
        public int WarrantyId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int PaymentId { get; set; }
    }

}
