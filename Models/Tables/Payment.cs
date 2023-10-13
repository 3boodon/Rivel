using System;

namespace Rivel.Models
{
    public class Payment : Timestamps
    {
        public Payment(int paymentId, int paymentMethodId, int userId, decimal paymentAmount)
        {
            PaymentId = paymentId;
            PaymentMethodId = paymentMethodId;
            UserId = userId;
            PaymentAmount = paymentAmount;
        }
        public Payment(int paymentMethodId, int userId, decimal paymentAmount)
        {
            PaymentMethodId = paymentMethodId;
            UserId = userId;
            PaymentAmount = paymentAmount;
        }
        public int PaymentId { get; set; }
        public int PaymentMethodId { get; set; }
        public int UserId { get; set; }
        public decimal PaymentAmount { get; set; }
    }

}
