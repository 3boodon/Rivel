namespace Rivel.Models {
    internal class PaymentMethod : BaseModel {


        // Payment Method Details
        public string MethodName { get; set; }  // e.g., "Credit Card", "Cash", "Bank Transfer"
        public string Description { get; set; }  // Detailed description of the payment method

        // Additional Fees (Optional)
        public decimal AdditionalFees { get; set; }  // e.g., Transaction fees, service charges

        // Limitations and Restrictions (Optional)
        public string Limitations { get; set; }  // e.g., "Not available for international customers"


    }
}
