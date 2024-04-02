using System.Text.RegularExpressions;
using ConfigHandling;
using Hashing;
using Spectre.Console;

namespace Manage.Repository
{
    public class PromptHandler
    {
        #region Menu prompt maker method
        /// <summary>
        /// Make menu prompt of Spectre.Console
        /// </summary>
        /// <param name="items">List of strings which are names or rows of menu</param>
        /// <returns>List of object: [Menu row name, Menu row index]</returns>
        public static List<object> MenuPrompt(List<string> items, string message)
        {
            Header();

            Dictionary<string, int> PartsDictionary = new Dictionary<string, int>();

            items.ForEach(x => PartsDictionary.Add(x, items.IndexOf(x)));

            string chosenPartMenu = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(message)
                    .PageSize(items.Count < 3 ? (items.Count + 1) : items.Count)
                    .HighlightStyle(Style.Parse("purple bold"))
                    .AddChoices(items)
            );

            AnsiConsole.Clear();
            return [chosenPartMenu, PartsDictionary[chosenPartMenu]];
        }
        #endregion

        #region Password changer prompt maker method
        /// <summary>
        /// Make prompt for changing/adding password
        /// </summary>
        /// <param name="passwordName">Name of password which could be repeated password, main password, end etc</param>
        /// <param name="headerMessage">This message shown to the user at the first line</param>
        /// <param name="IsNew">If user status was 1 it should be true</param>
        /// <param name="NeedsHint">If password should have hint phrase it should be true</param>
        public static void PasswordChangerPrompt(
            string passwordName,
            string? headerMessage = null,
            bool IsNew = true,
            bool NeedsHint = false
        )
        // The IsNew should check the root status and then set the true|false
        {
            bool flag = true;

            while (flag)
            {
                Header();

                HeaderMessageHandler(headerMessage);

                #region Get password
                var Password = AnsiConsole.Prompt(
                    new TextPrompt<string>(
                        $"What's your{(IsNew ? " [yellow]new[/]" : "")} [bold yellow]{passwordName}[/]? "
                    ).Secret()
                );
                if (Password.Length >= 20)
                {
                    Exit(
                        "[bold red]Your input should be less than 20 characters[/]\nPress any key to Repeat"
                    );
                    continue;
                }
                #endregion

                #region Get repeated password
                var RepeatedPassword = AnsiConsole.Prompt(
                    new TextPrompt<string>(
                        $"Repeat your{(IsNew ? " [yellow]new[/]" : "")} [bold yellow]{passwordName}[/]: "
                    ).Secret()
                );
                if (!string.Equals(Password, RepeatedPassword))
                {
                    Exit($"[bold red]{passwordName}s aren't the same[/]\nPress any key to Repeat");
                    continue;
                }
                #endregion

                #region Get & set hint phrase and password
                if (NeedsHint)
                {
                    ConfigSetter.SetConfigToRoot.RootRecoveryPassword(Hash.ToSha256(Password));

                    string HintPassword = string.Empty;
                    bool localFlag = true;
                    while (localFlag)
                    {
                        HintPassword = AnsiConsole.Prompt(
                            new TextPrompt<string>($"What's your [purple bold]hint[/] message: ")
                        );
                        if (HintPassword.Contains(';'))
                        {
                            Exit(
                                "[bold red]Hint string shouldn't contains \"; { }\" characters[/]\nPress any key to Repeat"
                            );
                            continue;
                        }
                        localFlag = false;
                    }

                    ConfigSetter.SetConfigToRoot.RecoveryHintString(HintPassword);
                }
                else
                {
                    ConfigSetter.SetConfigToRoot.RootMainPassword(Hash.ToSha256(Password));
                }
                #endregion

                #region End of loop
                AnsiConsole.MarkupLine(
                    $"[green]Your{(IsNew ? " new" : "")} [bold]{passwordName}[/] has been set[/]"
                );

                Exit("Press any key to continue");
                flag = false;
                #endregion
            }
        }
        #endregion

        #region Return to main menu method
        /// <summary>
        /// Tasks for returning to main menu
        /// </summary>
        public static void ReturnToMainMenu()
        {
            AnsiConsole.Clear();
        }
        #endregion

        #region Allowed use time
        /// <summary>
        /// Contains methods which are related to Using time
        /// </summary>
        public class AllowedUseTime
        {
            #region Setter Prompt maker method
            /// <summary>
            /// Make prompt for changing/adding allowed use time
            /// </summary>
            /// <param name="timeName">Name of time which could be template time or other names</param>
            /// <param name="headerMessage">This message shown to user at first line</param>
            /// <param name="IsNew">If user status was 1 it should be true</param>
            public static void AllowedUseTimeSetterPrompt(
                string timeName,
                string? headerMessage = null,
                bool IsNew = true,
                bool IsTemp = false
            )
            {
                bool flag = true;

                while (flag)
                {
                    Header();

                    HeaderMessageHandler(headerMessage);

                    #region Get use time

                    var usingTime = AnsiConsole.Prompt(
                        new TextPrompt<int>(
                            $"What's your{(IsNew ? " [green]new[/]" : "")} [bold]{timeName}[/]? "
                        )
                    );
                    if (Validator(usingTime, IsTemp) == false)
                    {
                        Exit(
                            $"[red][bold]{timeName}[/] is out of range. It must be a multiple of 10 & between [underline]{(IsTemp == true ? "0" : "10")}[/] and [underline]1430[/]\nPress any key to Repeat[/]"
                        );
                        continue;
                    }
                    #endregion

                    #region Setting data and end of loop
                    if (!IsTemp)
                    {
                        ConfigSetter.SetConfigToService.AllowedDuration(usingTime.ToString());
                    }
                    else
                    {
                        ConfigSetter.SetConfigToService.TempAllowedDuration(usingTime.ToString());
                    }

                    AnsiConsole.MarkupLine(
                        $"[green]Your{(IsNew ? " new" : "")} [bold]{timeName}[/] has been set[/]"
                    );
                    Exit("Press any key to continue");
                    #endregion
            }
            #endregion

            #region Default setter method
            public static void AllowedUseTimeSetterPrompt(
                string timeName,
                bool IsTemp = false,
                bool IsNew = true
            )
            {
                if (!IsTemp)
                {
                    ConfigSetter.SetConfigToService.AllowedDuration("120");
                }
                else
                {
                    ConfigSetter.SetConfigToService.TempAllowedDuration("0");
                }

                AnsiConsole.MarkupLine(
                    $"[green]Your{(IsNew ? " new" : "")} [bold]{timeName}[/] has been set[/]"
                );
                Exit("Press any key to continue");
            }
            #endregion

            #region Validator method
            /// <summary>
            /// Validate using time inputs
            /// </summary>
            /// <param name="input">The value which user inserted</param>
            /// <param name="IsTemp">Is the time for temporary time or it's main time</param>
            /// <returns>
            ///     <term>Bool</term>
            ///     <description>Is the input valid or not</description>
            /// </returns>
            private static bool Validator(int input, bool IsTemp)
            {
                if (IsTemp == true)
                {
                    return (input % 10 != 0 || input < 0 || input > 1430) ? false : true;
                }
                else
                {
                    return (input % 10 != 0 || input < 10 || input > 1430) ? false : true;
                }
            }
            #endregion
        }
        #endregion

        #region Allowed time of day changer prompt maker method
        /// <summary>
        /// Make prompt for changing/adding allowed time of day
        /// </summary>
        /// <param name="headerMessage">This message shown to user at first line</param>
        /// <param name="IsNew">If user status was 1 it should be true</param>
        public static void AllowedTimeOfDayChangerPrompt(
            string? headerMessage = null,
            bool IsNew = true
        )
        {
            bool flag = true;

            while (flag)
            {
                Header();

                HeaderMessageHandler(headerMessage);

                #region Get start time of day
                var startTimeOfDay = AnsiConsole.Prompt(
                    new TextPrompt<string>(
                        $"What's your{(IsNew ? " [green]new[/]" : "")} [bold]Start Time Of Day[/]? "
                    )
                );
                if (!Regex.IsMatch(startTimeOfDay, @"\d{2}:\d{2}"))
                {
                    Exit(
                        "[red]Inserted time has wrong format. It must be like [[two digit number]][bold]:[/][[two digit number]][/]\nPress any key to repeat"
                    );
                    continue;
                }
                #endregion

                #region Get end time of day
                var endTimeOfDay = AnsiConsole.Prompt(
                    new TextPrompt<string>(
                        $"What's your{(IsNew ? " [green]new[/]" : "")} [bold]End Time Of Day[/]? "
                    )
                );
                // end time should be after the start time in a day
                if (!Regex.IsMatch(endTimeOfDay, @"\d{2}:\d{2}"))
                {
                    Exit("[red]Inserted time has wrong format[/]");
                    continue;
                }
                #endregion

                #region Setting data & end of loop
                ConfigSetter.SetConfigToService.StartTimeOfDay(startTimeOfDay);
                ConfigSetter.SetConfigToService.EndTimeOfDay(endTimeOfDay);

                AnsiConsole.MarkupLine(
                    $"[green]Your{(IsNew ? " new" : "")} [bold]Allowed time of day[/] has been set[/]"
                );

                Exit("Press any key to continue");
                flag = false;
                #endregion
            }
        }

        public static void AllowedTimeOfDayChangerPrompt(bool IsNew = true)
        {
            ConfigSetter.SetConfigToService.StartTimeOfDay("06:00");
            ConfigSetter.SetConfigToService.EndTimeOfDay("23:50");

            AnsiConsole.MarkupLine(
                $"[green]Your{(IsNew ? " new" : "")} [bold]Allowed time of day[/] has been set[/]"
            );
            Exit("Press any key to continue");
        }
        #endregion

        #region User validation prompt maker method
        /// <summary>
        /// Make prompt for Validate the user
        /// </summary>
        /// <param name="userName">Name of the user which we want to validate</param>
        public static void UserValidationPrompt(string userName)
        {
            #region Header
            Header();
            #endregion

            int wrongPasswordCounter = 0;
            AnsiConsole.Prompt(
                new TextPrompt<string>($"Please insert the [bold red]{userName}[/] password: ")
                    .PromptStyle("green")
                    .Secret()
                    .ValidationErrorMessage("[red]That's not a valid password[/]")
                    .Validate(password =>
                    {
                        if (string.Equals(Hash.ToSha256(password), RootPart.RootMainPassword()))
                        {
                            return ValidationResult.Success();
                        }
                        else
                        {
                            wrongPasswordCounter++;
                            if (wrongPasswordCounter >= 3)
                            {
                                AnsiConsole.MarkupLine(
                                    "[gray italic]For exit insert[/] [bold gray]ctrl + c[/]"
                                );
                            }
                            return ValidationResult.Error("[bold red]Wrong password[/]");
                        }
                    })
            );
            AnsiConsole.Clear();
        }
        #endregion

        #region Exit process method
        /// <summary>
        /// Jobs we do before repeat a loop
        /// </summary>
        /// <param name="message">An string which shown to the user</param>
        /// <remarks>After calling this method, use Continue to ignore rest of loop</remarks>
        public static void Exit(string? message = null)
        {
            #region Jobs
            if (message != null)
                AnsiConsole.MarkupLine(message);
            #endregion

            #region Wait for the user
            Console.ReadKey();
            #endregion

            #region End method
            AnsiConsole.Clear();
            #endregion
        }
        #endregion

        #region Header message handler method
        /// <summary>
        /// Handle header messages in prompts
        /// </summary>
        /// <param name="message">Shown to user if it isn't null</param>
        public static void HeaderMessageHandler(string? message)
        {
            if (message != null)
                AnsiConsole.MarkupLine($"[gray italic]{message}[/]");
        }
        #endregion

        #region Header prompt method
        /// <summary>
        /// This method Will do the main app header jobs
        /// </summary>
        public static void Header()
        {
            #region Clear the console
            AnsiConsole.Clear();
            #endregion

            #region Create the header
            var rule = new Rule("[bold italic purple]Child use time protector[/]");
            #endregion

            #region Print the header
            AnsiConsole.Write(rule);
            #endregion
        }
        #endregion

        // repeat method
    }
}
