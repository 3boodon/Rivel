
using System.Collections.Generic;
using Rivel.Controllers.ViewControllers;
using Rivel.Framework;
using Rivel.Routing;

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
                    new Option("Make a New Reservation", () => new ReservationsViewController()),
                    new Option("Back Home", () => Router.NavigateTo(RouteName.Home)),
                }, () =>
                {
                    Helper.Print("^^Invalid Choice.. Please choose from the list", MessageType.ERROR);
                    Render();
                });
        }

    }
}
