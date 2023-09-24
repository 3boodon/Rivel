using Rivel.Models;

namespace Rivel.Controllers {
    internal class CarModelController : AbstractCrudController<CarModel> {
        public CarModelController() : base(@"..\..\Data\CarModel\CarModels.txt") {
        }
    }
}

