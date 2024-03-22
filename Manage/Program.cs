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
        #region Environment variables
        Environment.SetEnvironmentVariable("Testing", "true");
        #endregion

        es:
        // disappeared on reload
        var rule = new Rule("[bold italic purple]Child use time protector[/]");
        AnsiConsole.Write(rule);

        if (File.Exists(ConfigFile.ConfigFilePath) && RootPart.Status())
        {
            // Test phase

            // Main menu loop flag
            bool flag = true;

            // Main menu loop
            while (flag)
            {
                // Create main menu
                switch (PromptHandler.MenuPrompt(["Root", "Service", "Exit", "Test"])[1])
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
                                ]
                            #endregion
                            )[1]
                        )
                        {
                            #region Change service status
                            case 0:
                                if (ServicePart.Status())
                                {
                                    // Doesn't work:
                                    Commands.TurnOffService();
                                    ConfigSetter.SetConfigToService.Status(Hash.ToSha256("0"));
                                    PromptHandler.ExitProcess(
                                        "[red bold]Service disabled[/]\n[gray]Press any key to exit[/]"
                                    );
                                }
                                else
                                {
                                    // Doesn't work:
                                    Commands.TurnOnService();
                                    ConfigSetter.SetConfigToService.Status(Hash.ToSha256("1"));
                                    PromptHandler.ExitProcess(
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
                                    "Default allowed day time is from 06:00 to 23:59"
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
                        flag = false;
                        // Add exit message
                        break;
                    #endregion

                    #region Test
                    case 3:
                        ConfigSetter.SetConfigToService.Status("1");
                        ConfigSetter.SetConfigToService.StartTimeOfDay("08:33");
                        ConfigSetter.SetConfigToService.EndTimeOfDay("28:33");
                        ConfigSetter.SetConfigToService.AllowedDuration("10");
                        ConfigSetter.SetConfigToService.TempAllowedDuration("30");

                        break;
                    #endregion
                }
            }
        }
        else
        {
            ConfigFile.ConfigFileBuilder();

            AnsiConsole.MarkupLine(
                "Welcome to [bold purple]CUTP[/]\n[orange3 bold]To set default configuration blank or enter your favorite config[/]\n"
            );

            PromptHandler.PasswordChangerPrompt("Password", IsNew: false, NeedsHint: false);

            PromptHandler.PasswordChangerPrompt(
                "Recovery Password",
                "Using memorable password recommended\nThis password should be use in emergency situation because it resets all parts",
                false,
                true
            );

            PromptHandler.AllowedTimeOfDayChangerPrompt(
                "Default allowed day time is from 06:00 to 23:59",
                false
            );

            PromptHandler.AllowedUseTimeChangerPrompt(
                "Allowed use time",
                "Default using time is 120 minute\n[gray italic]Insert your answer in minute like [underline bold]120[/] which means 2 hour[/]",
                false,
                false
            );
            ConfigSetter.SetConfigToRoot.Status(Hash.ToSha256("1"));

            goto es;
        }
    }
}
