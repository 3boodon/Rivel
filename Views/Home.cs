using System.Collections.Generic;
using Rivel.Framework;
using Rivel.Routing;

namespace Rivel.Views
{
    public class Home
    {

        public static void Render()
        {
            if (Auth.IsLoggedIn)
            {
                Helper.Print(Helper.DecoratedText(Helper.GreetingMessage(), '*'));
                Helper.Print("Please choose from the menu below", MessageType.INFO);
                // To be shown when the user is logged in
                OptionsList userOptions = new OptionsList(new List<Option> {
                    new Option("Reservations Management", () => Router.NavigateTo(RouteName.ReservationsManagement)),
                    new Option("Logout", () => Router.NavigateTo(RouteName.Logout)),
                    new Option("Exit", () => App.Close())
                }, () =>
                {
                    Helper.Print("^^Invalid Choice.. Please choose from the list", MessageType.ERROR);
                    Render();
                });
            }
            else
            {
                Helper.Print(Helper.DecoratedText($"Welcome to {App.Name}", '*'));
                Helper.Print("Please choose from the menu below", MessageType.INFO);

                // To be shown when the user is not logged in
                OptionsList guestOptions = new OptionsList(new List<Option> {
                    new Option("Login", () => Router.NavigateTo(RouteName.Login)),
                    new Option("Create a New Account", () => Router.NavigateTo(RouteName.Register)),
                    new Option("Exit", () => App.Close())
                }, () =>
                {
                    Helper.Print("^^Invalid Choice.. Please choose from the list", MessageType.ERROR);
                    Helper.SetTimeOut(() => Router.NavigateTo(RouteName.Home), 1000);
                });

            }
        }
    }
}

