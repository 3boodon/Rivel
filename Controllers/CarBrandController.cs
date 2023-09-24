using Rivel.Models;

namespace Rivel.Controllers {
    internal class CarBrandController : AbstractCrudController<CarBrand> {
        public CarBrandController() : base(@"..\..\Data\CarBrand\CarBrands.txt") {
        }
    }
}
