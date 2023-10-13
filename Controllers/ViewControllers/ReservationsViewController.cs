using System;
using System.Collections.Generic;
using Rivel.Framework;
using Rivel.Models;
using Rivel.Routing;
using Rivel.Services;

namespace Rivel.Controllers.ViewControllers
{
    internal class ReservationsViewController
    {

        public ReservationsViewController()
        {
            // show the list of available cars
            CarController carController = new CarController();
            List<Car> _cars = carController.GetAll();
            List<CarView> cars = CarView.Parse(_cars);
            Helper.Print(Helper.DecoratedText("Available Cars", '-'), MessageType.INFO);

            // render the list of cars in a table
            Table<CarView>.Render(cars, 90, "available_cars");

            // ask the user to choose a car
            int chosenCarId = GetChosenCar(carController);

            // ask the user to enter the number of days
            int numberOfDays = Helper.GetUserInputAsInt("How many days do you need for this reservation:");

            // ask the user to enter the date of the pickup as a MySql Date Format
            string pickupDate = GetPickupDate();

            // calculate the return date using pickup date and number of days 
            string returnDate = DateService.CalcDate(DateTime.Parse(pickupDate), numberOfDays);

            // calculate the total price using the number of days and the car's price per day
            decimal totalPrice = numberOfDays * carController.GetById(chosenCarId).PricePerDay;

            // ask the user to confirm the reservation
            string confirm = ConfirmReservation(chosenCarId, pickupDate, returnDate, totalPrice);


            // if confirmed, ask the user to pay 
            // if not confirmed, go back to the reservations menu

            if (confirm.ToLower() == "y")
            {
                Helper.Print(Helper.DecoratedText("One last step to complete the reservation", '-'), MessageType.INFO);
                ConfirmPayment(chosenCarId, pickupDate, returnDate, totalPrice);

                Helper.SetTimeOut(() => Router.NavigateTo(RouteName.ReservationsManagement), 3000);
            }
            else
            {
                Helper.Print(Helper.DecoratedText("Reservation Canceled", '-'), MessageType.WARNING);
                Helper.SetTimeOut(() => Router.NavigateTo(RouteName.ReservationsManagement), 2000);
            }

        }

        /// <summary>
        /// Displays the reservation details and prompts the user to confirm the reservation.
        /// </summary>
        /// <param name="chosenCarId">The ID of the chosen car.</param>
        /// <param name="pickupDate">The pickup date of the reservation.</param>
        /// <param name="returnDate">The return date of the reservation.</param>
        /// <param name="totalPrice">The total price of the reservation.</param>
        /// <returns>A string indicating whether the user confirmed the reservation or not.</returns>
        private static string ConfirmReservation(int chosenCarId, string pickupDate, string returnDate, decimal totalPrice)
        {
            Helper.Print(Helper.DecoratedText("This is Your Reservation Details", '-'));
            Helper.Print($"Car ID: {chosenCarId}");
            Helper.Print($"Pickup Date: {pickupDate}");
            Helper.Print($"Return Date: {returnDate}");
            Helper.Print($"Total Price: {totalPrice.AsCurrency()}");
            string confirm = Helper.GetUserInput("Confirm Reservation? (Y/N):");
            return confirm;
        }

        /// <summary>
        /// Gets the ID of the chosen car from the user input and validates it using the CarController.
        /// </summary>
        /// <param name="carController">The CarController instance used to validate the chosen car ID.</param>
        /// <returns>The ID of the chosen car.</returns>
        private static int GetChosenCar(CarController carController)
        {
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

            return chosenCarId;
        }

        /// <summary>
        /// Displays a list of payment methods to the user and prompts them to choose a payment method.
        /// Saves the reservation and payment information to the database upon successful payment.
        /// </summary>
        /// <param name="chosenCarId">The ID of the car chosen by the user for reservation.</param>
        /// <param name="pickupDate">The pickup date of the reservation.</param>
        /// <param name="returnDate">The return date of the reservation.</param>
        /// <param name="totalPrice">The total price of the reservation.</param>
        private static void ConfirmPayment(int chosenCarId, string pickupDate, string returnDate, decimal totalPrice)
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
                    Payment payment = SavePaymentRecord(totalPrice, paymentMethod);

                    // Add a reservation record to the database
                    SaveReservation(chosenCarId, pickupDate, returnDate, payment);
                    Helper.Print(Helper.DecoratedText("Your reservation has been confirmed"), MessageType.SUCCESS);
                }));
            }
            // show the list of payment methods
            // ask the user to choose a payment method
            OptionsList paymentOptions = new OptionsList(paymentOptionsList, () =>
            {
                Helper.Print("^^Invalid Choice.. Please choose a valid Method Id from the list", MessageType.ERROR);
                ConfirmPayment(chosenCarId, pickupDate, returnDate, totalPrice);
            });
        }

        /// <summary>
        /// Saves a payment record with the given total price and payment method.
        /// </summary>
        /// <param name="totalPrice">The total price of the payment.</param>
        /// <param name="paymentMethod">The payment method used for the payment.</param>
        /// <returns>The saved payment record.</returns>
        private static Payment SavePaymentRecord(decimal totalPrice, PaymentMethod paymentMethod)
        {
            PaymentController paymentController = new PaymentController();
            Payment payment = new Payment(
                paymentMethodId: paymentMethod.PaymentMethodId,
                userId: Auth.LoggedInUser.UserId,
                paymentAmount: totalPrice
                );
            paymentController.Create(payment);
            payment = paymentController.GetLastPayment();
            return payment;
        }

        /// <summary>
        /// Saves a new reservation to the database.
        /// </summary>
        /// <param name="chosenCarId">The ID of the car chosen for the reservation.</param>
        /// <param name="pickupDate">The pickup date for the reservation.</param>
        /// <param name="returnDate">The return date for the reservation.</param>
        /// <param name="payment">The payment information for the reservation.</param>
        private static void SaveReservation(int chosenCarId, string pickupDate, string returnDate, Payment payment)
        {
            IController<Reservation> reservationController = new ReservationController();
            Reservation reservation = new Reservation(
                carId: chosenCarId,
                userId: Auth.LoggedInUser.UserId,
                pickupDate: pickupDate,
                returnDate: returnDate,
                paymentId: payment.PaymentId
            );
            reservationController.Create(reservation);
        }

        /// <summary>
        /// Prompts the user to enter a pickup date in the format of "YYYY-MM-DD", validates the input, and returns the date in MySQL format.
        /// </summary>
        /// <returns>The pickup date in MySQL format.</returns>
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
