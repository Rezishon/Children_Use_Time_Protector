using Microsoft.Toolkit.Uwp.Notifications;

namespace FinalConsole;

class Program
{
    static void Main(string[] args)
    {
       // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Andrew sent you a picture")
                .AddText("Check this out, The Enchantments in Washington!")
                .Show();
    }
}
