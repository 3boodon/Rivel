using System;
using System.Collections.Generic;
using Rivel.Views;
using Rivel.Framework;

namespace Rivel.Routing {
    internal class Router {
        /// <summary>
        /// List of all available routes in the application.
        /// </summary>
        public static List<Route> routes = new List<Route> {
            // Home Routes
            DefineNewRoute(RouteName.Home,Home.Render),
            DefineNewRoute(RouteName.ReservationsManagement,ReservationsManagement.Render),
            // Auth Routes 
            DefineNewRoute(RouteName.Login, Login.Render),
            DefineNewRoute(RouteName.Register,Register.Render),
            DefineNewRoute(RouteName.Logout,Auth.Logout),
    };

        /// <summary>
        /// Defines a new route with the given name and callback action.
        /// </summary>
        /// <param name="name">The name of the route.</param>
        /// <param name="callback">The callback action to execute when the route is accessed.</param>
        /// <returns>A new Route object representing the defined route.</returns>
        public static Route DefineNewRoute(RouteName name, Action callback = null) {
            return new Route(name, callback);
        }

        /// <summary>
        /// Navigates to the specified route by its name.
        /// </summary>
        /// <param name="routeName">The name of the route to navigate to.</param>
        public static void NavigateTo(RouteName routeName) {
            // Find the route by its name
            Route route = routes.Find(theRoute => theRoute.name == routeName);

            // Check if the route is found
            if (route != null) {
                // If the route is found, clear the console and run the callback
                Console.Title = $"{routeName} - {App.Name}";
                Console.Clear();
                route.callback();
            }
            else {
                // If the route is not found, redirect to home page
                Console.Title = $"{routeName} - 404 NOT FOUND";
                Helper.Print("Route Not Found, You will be redirected to Home Page ..", MessageType.ERROR);
                Helper.SetTimeOut(() => NavigateTo(RouteName.Home), 1000);
            }
        }
    }


    /// <summary>
    /// Represents a route in the router.
    /// </summary>
    class Route {
        public RouteName name;
        public Action callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="Route"/> class.
        /// </summary>
        /// <param name="name">The name of the route.</param>
        /// <param name="callback">The callback function to execute when the route is matched.</param>
        public Route(RouteName name, Action callback) {
            this.name = name;
            this.callback = callback;
        }
    }

    /// <summary>
    /// Represents the available routes in the application. <br/>
    /// This enum is created for better readability and to avoid typos
    /// </summary>
    enum RouteName {
        // Home Routes
        Home,
        // Reservations Routes
        ReservationsManagement,
        NewReservation,
        Reservations,
        ReservationDetails,
        CancelReservation,

        // Cars Routes
        CarsManagement,
        NewCar,
        Cars,
        RemoveCar,


        // Authentication Routes
        Login,
        Register,
        Logout
    }
}
