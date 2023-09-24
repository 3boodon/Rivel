using Rivel.Models;

namespace Rivel.Controllers {
    internal class ReservationController : AbstractCrudController<Reservation> {
        public ReservationController() : base(@"..\..\Data\Reservation\Reservations.txt") {
        }
    }
}

