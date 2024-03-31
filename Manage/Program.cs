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
        #region AnsiConsole config
        AnsiConsole.Cursor.Hide();
        #endregion

        #region Environment variables
        Environment.SetEnvironmentVariable("Testing", "true");
        #endregion

        es:

        if (File.Exists(ConfigFile.ConfigFilePath) && RootPart.Status())
        {
            /// <comment>Test phase</comment>
            if (Environment.GetEnvironmentVariable("Testing") != "true")
            {
                /// <comment>User validation</comment>
                PromptHandler.UserValidationPrompt("Root");

                Console.Clear();
            }

            /// <comment>Main menu loop flag</comment>
            bool flag = true;

            /// <comment>Main menu loop</comment>
            while (flag)
            {
                PromptHandler.Header();

                /// <comment>Create main menu</comment>
                switch (
                    PromptHandler.MenuPrompt(
                        ["Root", "Service", "Exit", "Test"],
                        "What part you want to change?"
                    )[1]
                )
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
                                ],
                                "What part you want to change?"
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
                                PromptHandler.PasswordChangerPrompt(
                                    "Recovery Password",
                                    "Using memorable password recommended\nThis password should be use in emergency situation because it resets all parts",
                                    true,
                                    true
                                );
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
                                ],
                                "What part you want to change?"
                            #endregion
                            )[1]
                        )
                        {
                            #region Change service status
                            case 0:
                                PromptHandler.Header();

                                if (ServicePart.Status())
                                {
                                    // Doesn't work:
                                    Commands.TurnOffService();
                                    ConfigSetter.SetConfigToService.Status(Hash.ToSha256("0"));
                                    PromptHandler.Exit(
                                        "[red bold]Service disabled[/]\n[gray]Press any key to exit[/]"
                                    );
                                }
                                else
                                {
                                    // Doesn't work:
                                    Commands.TurnOnService();
                                    ConfigSetter.SetConfigToService.Status(Hash.ToSha256("1"));
                                    PromptHandler.Exit(
                                        "[green bold]Service enabled[/]\n[gray]Press any key to exit[/]"
                                    );
                                }
                                break;
                            #endregion

                            #region Change allowed use time
                            case 1:
                                PromptHandler.AllowedUseTimeChangerPrompt(
                                    "Allowed use time",
                                    "Default using time is 120 minute\n[gray italic]Insert your answer in minute like [underline bold]120[/] which means 2 hour[/]"
                                );
                                break;
                            #endregion

                            #region Change temporary allowed use time
                            case 2:
                                PromptHandler.AllowedUseTimeChangerPrompt(
                                    "Temporary allowed use time",
                                    "This time will added to the main allowed using duration",
                                    IsTemp: true
                                );
                                break;
                            #endregion

                            #region Change allowed time of day
                            case 3:
                                PromptHandler.AllowedTimeOfDayChangerPrompt(
                                    "Default allowed day time is from 06:00 to 23:50"
                                );
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
                        PromptHandler.Header();

                        switch (
                            PromptHandler.MenuPrompt(
                                ["No", "Yes"],
                                "[orange3 bold]Want to exit?[/]"
                            )[1]
                        )
                        {
                            #region No
                            case 0:
                                /// <comment>Repeat the loop and start over the app</comment>
                                break;
                            #endregion

                            #region Yes
                            case 1:
                                flag = false;
                                break;
                            #endregion
                        }
                        break;
                    #endregion

                    #region Test
                    case 3:
                        if (Environment.GetEnvironmentVariable("Testing") == "true")
                        {
                            ConfigSetter.SetConfigToRoot.Status(Hash.ToSha256("0"));
                            ConfigSetter.SetConfigToService.Status(Hash.ToSha256("0"));
                        }
                        break;
                    #endregion
                }
            }
        }
        else
        {
            #region Header
            PromptHandler.Header();
            #endregion

            #region Build configuration file
            ConfigFile.ConfigFileBuilder();
            #endregion

            #region Ask for password
            PromptHandler.PasswordChangerPrompt(
                "Password",
                "[white]Welcome to[/] [bold purple]CUTP[/]\n[orange3 bold]This app will help you to manage your child's usage time of pc[/]\n",
                IsNew: false,
                NeedsHint: false
            );
            #endregion

            #region Ask for recovery password
            PromptHandler.PasswordChangerPrompt(
                "Recovery Password",
                "Using memorable password recommended\nThis password should be use in emergency situation because it resets all parts",
                false,
                true
            );
            #endregion

            #region Ask for time of day
            if (
                AnsiConsole.Confirm(
                    "Default allowed time of day is from 06:00 until 23:50, want to change it? ",
                    false
                )
            )
            {
                AnsiConsole.Clear();
                PromptHandler.AllowedTimeOfDayChangerPrompt(
                    "Default allowed day time is from 06:00 to 23:50",
                    false
                );
            }
            else
            {
                PromptHandler.AllowedTimeOfDayChangerPrompt(false);
            }
            #endregion

            #region Ask for use time
            if (
                AnsiConsole.Confirm(
                    "Default allowed use time is 120 minute, want to change it? ",
                    false
                )
            )
            {
                AnsiConsole.Clear();
                PromptHandler.AllowedUseTimeChangerPrompt(
                    "Allowed use time",
                    "Default using time is 120 minute\n[gray italic]Insert your answer in minute like [underline bold]120[/] which means 2 hour[/]",
                    false,
                    false
                );
            }
            else
            {
                PromptHandler.AllowedUseTimeChangerPrompt("Allowed use time", false, false);
            }
            #endregion

            ConfigSetter.SetConfigToRoot.Status(Hash.ToSha256("1"));
            Commands.TurnOnService();
            ConfigSetter.SetConfigToService.Status(Hash.ToSha256("1"));

            goto es;
        }
    }
}
