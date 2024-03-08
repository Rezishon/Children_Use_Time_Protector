using System.Text.RegularExpressions;
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
        PromptHandler.UserValidationPrompt("Root");

        // Main menu loop flag
        bool flag = true;

        // Main menu loop
        while (flag)
        {
            // Create main menu
            switch (PromptHandler.MenuPrompt(["Root", "Service", "Exit"])[1])
            {
                #region Root menu
                case 0:
                    switch (
                        #region Create root menu
                        PromptHandler.MenuPrompt(
                            [
                                "Change the root password",
                                "Change The root recovery password",
                                "Main menu"
                            ]
                        #endregion
                        )[1]
                    )
                    {
                        #region Change the root password
                        case 0:
                            PromptHandler.PasswordChangerPrompt("Password");
                            break;
                        #endregion

                        #region Change The root recovery password
                        case 1:
                            AnsiConsole.MarkupLine(
                                "Using memorable password recommended\nThis password should be use in emergency situation because it resets all parts"
                            );
                            PromptHandler.PasswordPrompt("Recovery Password", true, true);
                            break;
                        #endregion

                        #region Main Menu
                        case 2:
                            PromptHandler.ReturnToMainMenu();
                            break;
                        #endregion
                    }
                    break;
                #endregion

                #region Service menu
                case 1:
                    switch (
                        #region Create service menu
                        PromptHandler.MenuPrompt(
                            [
                                "Change service status",
                                "Change allowed use time",
                                "Change temporary allowed use time",
                                "Change allowed time of day",
                                "Main menu"
                            ]
                        #endregion
                        )[1]
                    )
                    {
                        #region Change service status
                        case 0:
                            if (ServicePart.Status())
                                Commands.TurnOffService();
                            else
                                Commands.TurnOnService();
                            break;
                        #endregion

                        #region Change allowed use time
                        case 1:
                            AnsiConsole.MarkupLine("Default using time is 120 minute");
                            PromptHandler.AllowedUseTime("Allowed use time");
                            break;
                        #endregion

                        #region Change temporary allowed use time
                        case 2:
                            AnsiConsole.MarkupLine(
                                "This time will added to the main allowed using duration"
                            );
                            PromptHandler.AllowedUseTime("Temporary allowed use time");
                            break;
                        #endregion

                        #region Change allowed time of day
                        case 3:
                            AnsiConsole.MarkupLine(
                                "Default allowed day time is from 06:00 to 23:59"
                            );
                            PromptHandler.AllowedTimeOfDay();
                            break;
                        #endregion

                        #region Main menu
                        case 4:
                            PromptHandler.ReturnToMainMenu();
                            break;
                        #endregion
                    }
                    break;
                #endregion

                #region Exit
                case 2:
                    flag = false;
                    // Add exit message
                    break;
                #endregion
            }
        }
    }
}
