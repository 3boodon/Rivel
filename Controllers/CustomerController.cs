using Rivel.Models;

namespace Rivel.Controllers {
    internal class CustomerController : AbstractCrudController<Customer> {
        public CustomerController() : base(@"..\..\Data\Customer\Customers.txt") {
        }
    }
}

