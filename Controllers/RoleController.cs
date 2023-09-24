using Rivel.Models;

namespace Rivel.Controllers {
    internal class RoleController : AbstractCrudController<Role> {
        public RoleController() : base(@"..\..\Data\Role\Roles.txt") {
        }
    }
}
