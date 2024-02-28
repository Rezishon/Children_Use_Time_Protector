using CommandHandling;
using ConfigHandling;
using Hashing;
using Manage.Repository;
using Spectre.Console;

namespace Children_Use_Time_Protector;

class Program
{
    static void Main(string[] args)
    {
        // User validation
        PromptHandler.UserValidation("Root");

        // Main menu loop flag
        bool flag = true;

        // Main menu loop
        while (flag)
        {
            // Create main menu
            switch (PromptHandler.MenuPrompt(["Root", "Service", "Exit"])[1])
            {
                // Root menu
                case 0:
                    // Create root menu
                    switch (
                        PromptHandler.MenuPrompt(
                            [
                                "Change the root password",
                                "Change The root recovery password",
                                "Main menu"
                            ]
                        )[1]
                    )
                    {
                        // Change the root password
                        case 0:
                            PromptHandler.PasswordPrompt("Password");
                            break;

                        // Change The root recovery password
                        case 1:
                            AnsiConsole.MarkupLine(
                                "Using memorable password recommended\nThis password should be use in emergency mood"
                            );
                            PromptHandler.PasswordPrompt("Recovery Password", true, true);
                            break;

                        // Main Menu
                        case 2:
                            PromptHandler.ReturnToMainMenu();
                            break;
                    }
                    break;
                case 1:
                    switch (
                        PromptHandler.MenuPrompt(
                            [
                                "Change service status",
                                "Change allowed use time",
                                "Change temporary allowed use time",
                                "Change allowed time of day",
                                "Main menu"
                            ]
                        )[1]
                    )
                    {
                        case 0:
                            if (ServicePart.Status())
                                Commands.TurnOffService();
                            else
                                Commands.TurnOnService();
                            break;
                        case 1:
                            PromptHandler.AllowedUseTime("Allowed use time");
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                    }
                    break;
                case 2:
                    break;
            }
        }
    }
}
