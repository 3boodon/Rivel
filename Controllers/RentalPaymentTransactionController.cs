using Rivel.Models;

namespace Rivel.Controllers {
    internal class RentalPaymentTransactionController : AbstractCrudController<RentalPaymentTransaction> {
        public RentalPaymentTransactionController() : base(@"..\..\Data\RentalPaymentTransaction\RentalPaymentTransactions.txt") {
        }
    }
}
