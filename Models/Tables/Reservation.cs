using System;

namespace Rivel.Models
{
    public class Reservation : Timestamps
    {
        public Reservation(int reservationId, int userId, int carId, string pickupDate, string returnDate, int paymentId)
        {
            ReservationId = reservationId;
            UserId = userId;
            CarId = carId;
            PickupDate = pickupDate;
            ReturnDate = returnDate;
            PaymentId = paymentId;
        }
        public Reservation(int userId, int carId, string pickupDate, string returnDate, int paymentId)
        {
            UserId = userId;
            CarId = carId;
            PickupDate = pickupDate;
            ReturnDate = returnDate;
            PaymentId = paymentId;
        }
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public string PickupDate { get; set; }
        public string ReturnDate { get; set; }
        public int PaymentId { get; set; }
    }

}
