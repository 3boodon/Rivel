using Rivel.Models;

namespace Rivel.Controllers {
    internal class ViolationController : AbstractCrudController<Violation> {
        public ViolationController() : base(@"..\..\Data\Violation\Violations.txt") {
        }
    }
}

