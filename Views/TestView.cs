using System;
using System.Collections.Generic;
using Rivel.Controllers;
using Rivel.Models;

namespace Rivel.Views {
    internal class TestView {
        public static void RunTests() {
            CarController carController = new CarController();

            // Test Create
            Car newCar = new Car { Id = 1, Make = "Toyota", Model = "Camry" };
            carController.Create(newCar);

            // Test Read
            List<Car> cars = carController.Read();
            Console.WriteLine("Cars Count: " + cars.Count);  // Should be 1

            // Test Update
            newCar.Model = "Corolla";
            carController.Update(1, newCar);

            // Test Delete
            //carController.Delete(1);

            // Final Read to confirm deletion
            cars = carController.Read();
            Console.WriteLine("Cars Count after delete: " + cars.Count);  // Should be 0
            Console.ReadKey();
        }
    }
}
