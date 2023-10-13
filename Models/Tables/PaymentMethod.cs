namespace Rivel.Models
{
    public class PaymentMethod : Timestamps
    {
        public PaymentMethod(int paymentMethodId, string paymentMethodName)
        {
            PaymentMethodId = paymentMethodId;
            PaymentMethodName = paymentMethodName;
        }
        public PaymentMethod(string paymentMethodName)
        {
            PaymentMethodName = paymentMethodName;
        }
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
    }

}
