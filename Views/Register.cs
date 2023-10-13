
using Rivel.Framework;

namespace Rivel.Views
{
    internal class Register
    {
        public static void Render()
        {
            Helper.Print(Helper.DecoratedText($"Create new account", '*'));
            Auth.Register();
        }
    }
}
