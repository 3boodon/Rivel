
using System;
using System.Collections.Generic;
using Rivel.Controllers;
using Rivel.Framework;
using Rivel.Models;
using Rivel.Routing;
using Rivel.Services;

namespace Rivel.Views
{
    internal class ReservationsManagement
    {
        public static void Render()
        {
            Helper.Print(Helper.DecoratedText($"Rivel >  Reservations Management", '*'));
            Helper.Print("Please choose from the menu below", MessageType.INFO);

            // To be shown when the user is logged in
            OptionsList userOptions = new OptionsList(new List<Option> {
                    new Option("Make a New Reservation", () => Reserve()),
                    new Option("Back Home", () => Router.NavigateTo(RouteName.Home)),
                }, () =>
                {
                    Helper.Print("^^Invalid Choice.. Please choose from the list", MessageType.ERROR);
                    Render();
                });
        }

        // Make a new Reservation 
        private static void Reserve()
        {
            // show the list of available cars
            CarController carController = new CarController();
            List<Car> _cars = carController.GetAll();
            List<CarView> cars = CarView.Parse(_cars);
            Helper.Print(Helper.DecoratedText("Available Cars", '-'));
            Table<CarView>.Render(cars, 90, "available_cars");



            // ask the user to choose a car
            int chosenCarId = Helper.GetUserInputAsInt("Please choose a car by its ID:");

            if (!carController.Find(chosenCarId))
            {
                do
                {
                    Console.WriteLine(carController.Find(chosenCarId));
                    Helper.Print(Helper.DecoratedText("Invalid Car Id"), MessageType.WARNING);
                    chosenCarId = Helper.GetUserInputAsInt("Please choose a valid car ID:");
                } while (!carController.Find(chosenCarId));
            }

            // ask the user to enter the number of days
            int numberOfDays = Helper.GetUserInputAsInt("How many days do you need for this reservation:");

            // ask the user to enter the date of the pickup as a MySql Date Format
            string pickupDate = GetPickupDate();

            // calculate the return date using pickup date and number of days 
            string returnDate = DateService.CalcDate(DateTime.Parse(pickupDate), numberOfDays);

            // calculate the total price using the number of days and the car's price per day
            decimal totalPrice = numberOfDays * carController.GetById(chosenCarId).PricePerDay;
            // ask the user to confirm the reservation
            Helper.Print(Helper.DecoratedText("This is Your Reservation Details", '-'));
            Helper.Print($"Car ID: {chosenCarId}");
            Helper.Print($"Pickup Date: {pickupDate}");
            Helper.Print($"Return Date: {returnDate}");
            Helper.Print($"Total Price: {totalPrice.AsCurrency()}");
            string confirm = Helper.GetUserInput("Confirm Reservation? (Y/N):");


            // if confirmed, ask the user to pay 
            // if not confirmed, go back to the reservations menu


            if (confirm.ToLower() == "y")
            {
                // please pay
                Helper.Print(Helper.DecoratedText("One last step to complete the reservation", '-'));
                ShowPaymentMethodsList(chosenCarId, pickupDate, returnDate, totalPrice);

                Helper.SetTimeOut(() => Router.NavigateTo(RouteName.ReservationsManagement), 3000);
            }
            else
            {
                Router.NavigateTo(RouteName.Home);
            }


            // if confirmed, add the reservation to the list of reservations
            // if not confirmed, go back to the reservations menu
        }

        private static void ShowPaymentMethodsList(int chosenCarId, string pickupDate, string returnDate, decimal totalPrice)
        {
            // ask the user to choose a payment method using options list
            // get all payment methods from the database
            PaymentMethodController paymentMethodController = new PaymentMethodController();
            List<PaymentMethod> paymentMethods = paymentMethodController.GetAll();
            // convert the payment methods to a list of options
            List<Option> paymentOptionsList = new List<Option>();
            foreach (PaymentMethod paymentMethod in paymentMethods)
            {
                paymentOptionsList.Add(new Option(paymentMethod.PaymentMethodName,
                () =>
                {
                    // save reservation to the database
                    Helper.Print("Thank you for your payment");

                    // Add a payment record to the database
                    PaymentController paymentController = new PaymentController();
                    Payment payment = new Payment(
                        paymentMethodId: paymentMethod.PaymentMethodId,
                        userId: Auth.LoggedInUser.UserId,
                        paymentAmount: totalPrice
                        );
                    paymentController.Create(payment);
                    payment = paymentController.GetLastPayment();

                    ReservationController reservationController = new ReservationController();
                    Reservation reservation = new Reservation(
                        carId: chosenCarId,
                        userId: Auth.LoggedInUser.UserId,
                        pickupDate: pickupDate,
                        returnDate: returnDate,
                        paymentId: payment.PaymentId
                    );
                    reservationController.Create(reservation);
                    Helper.Print("Your reservation has been confirmed", MessageType.SUCCESS);
                }));
            }
            // show the list of payment methods
            // ask the user to choose a payment method
            OptionsList paymentOptions = new OptionsList(paymentOptionsList, () =>
            {
                Helper.Print("^^Invalid Choice.. Please choose a valid Method Id from the list", MessageType.ERROR);
                ShowPaymentMethodsList(chosenCarId, pickupDate, returnDate, totalPrice);
            });
        }

        private static string GetPickupDate()
        {
            string _pickupDate = Helper.GetUserInput("Please enter the date of the pickup (YYYY-MM-DD):");
            string pickupDate = "";
            do
            {
                try
                {
                    if (!_pickupDate.IsValidDate())
                        throw new Exception("Date format must be yyyy-mm-dd");

                    else if (DateTime.Parse(_pickupDate).IsPast())
                        throw new Exception("Date must be in the future");

                    else
                        pickupDate = DateTime.Parse(_pickupDate).ToMySqlDate();
                }
                catch (Exception e)
                {
                    Helper.Print(Helper.DecoratedText(e.Message), MessageType.WARNING);
                    _pickupDate = Helper.GetUserInput("Please enter the date of the pickup (YYYY-MM-DD):");
                }
            } while (!_pickupDate.IsValidDate() || DateTime.Parse(_pickupDate).IsPast());
            return pickupDate;
        }
    }
}
