using Rivel.Models;
namespace Rivel.Controllers {
    internal class StaffController : AbstractCrudController<Staff> {
        public StaffController() : base(@"..\..\Data\Staff\Staffs.txt") {
        }
    }
}

