using Rivel.Models;

namespace Rivel.Controllers {
    internal class CarController : AbstractCrudController<Car> {
        public CarController() : base(@"..\..\Data\Car\Cars.txt") {
        }
    }
}
