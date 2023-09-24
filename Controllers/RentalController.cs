using Rivel.Models;

namespace Rivel.Controllers {
    internal class RentalController : AbstractCrudController<Rental> {
        public RentalController() : base(@"..\..\Data\Rental\Rentals.txt") {
        }
    }
}
