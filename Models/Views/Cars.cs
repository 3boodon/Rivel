
using Rivel.Controllers;
using System.Collections.Generic;

namespace Rivel.Models
{
    /// <summary>
    /// Represents a view model for a car, containing information about its brand, model, color, status, and price per day.
    /// </summary>
    public class CarView
    {

        public static List<CarView> Parse(List<Car> _cars)
        {
            CarModelController carModelController = new CarModelController();
            BrandController brandsController = new BrandController();
            ColorController colorController = new ColorController();
            CarStatusController carStatusController = new CarStatusController();
            List<CarView> cars = new List<CarView>();
            foreach (Car car in _cars)
            {
                cars.Add(new CarView(
                    carId: car.CarId,
                    brandName: brandsController.Find(car.BrandId).BrandName,
                    modelName: carModelController.Find(car.ModelId).ModelName,
                    colorName: colorController.Find(car.ColorId).ColorName,
                    carStatusName: carStatusController.Find(car.CarStatusId).CarStatusName,
                    pricePerDay: car.PricePerDay
                    ));
            }

            return cars;
        }

        public CarView(int carId, string brandName, string modelName, string carStatusName, string colorName, decimal pricePerDay)
        {
            CarId = carId;
            BrandName = brandName;
            ModelName = modelName;
            CarStatusName = carStatusName;
            ColorName = colorName;
            PricePerDay = pricePerDay;
        }
        public int CarId { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string CarStatusName { get; set; }
        public string ColorName { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
