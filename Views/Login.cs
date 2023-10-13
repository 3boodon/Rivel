

using Rivel.Framework;

namespace Rivel.Views
{
    internal class Login
    {
        public static void Render()
        {
            Helper.Print(Helper.DecoratedText($"Login to {App.Name}", '*'));
            Auth.Login();
        }
    }
}
