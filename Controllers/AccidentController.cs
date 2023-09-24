using Rivel.Models;

namespace Rivel.Controllers {
    internal class AccidentController : AbstractCrudController<Accident> {
        public AccidentController() : base(@"..\..\Data\Accident\Accidents.txt") {
        }
    }
}
