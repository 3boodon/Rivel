using Rivel.Models;

namespace Rivel.Controllers {
    internal class MaintenanceController : AbstractCrudController<Maintenance> {
        public MaintenanceController() : base(@"..\..\Data\Maintenance\Maintenances.txt") {
        }
    }
}
