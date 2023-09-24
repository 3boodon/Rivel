using Rivel.Models;

namespace Rivel.Controllers {
    internal class PaymentMethodController : AbstractCrudController<PaymentMethod> {
        public PaymentMethodController() : base(@"..\..\Data\PaymentMethod\PaymentMethods.txt") {
        }
    }
}

