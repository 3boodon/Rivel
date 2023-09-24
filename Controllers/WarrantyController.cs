using Rivel.Models;

namespace Rivel.Controllers {
    internal class WarrantyController : AbstractCrudController<Warranty> {
        public WarrantyController() : base(@"..\..\Data\Warranty\Warranties.txt") {
        }
    }
}
