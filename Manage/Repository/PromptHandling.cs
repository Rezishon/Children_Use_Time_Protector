using System.Text.RegularExpressions;
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
        public static List<object> MenuPrompt(List<string> items)
        {
            Dictionary<string, int> PartsDictionary = new Dictionary<string, int>();

            items.ForEach(x => PartsDictionary.Add(x, items.IndexOf(x)));

            string chosenPartMenu = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What part you want to change?")
                    .PageSize(items.Count)
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
            bool IsNew = true,
            bool NeedsHint = false
        )
        // The IsNew should check the root status and then set the true|false
        {
            bool flag = true;

            while (flag)
            {
                #region Get & set password
                var Password = AnsiConsole.Ask<string>(
                    $"What's your{(IsNew ? " [green]new[/]" : "")} [bold]{passwordName}[/]? "
                );
                if (Password.Length >= 20)
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine(
                        "[bold red]Your input should be less than 20 characters[/]\nPress any key to Repeat"
                    );
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                AnsiConsole.MarkupLine(Hash.ToSha256(Password));
                #endregion

                #region Get & set repeated password
                var RepeatedPassword = AnsiConsole.Ask<string>(
                    $"Repeat your{(IsNew ? " [green]new[/]" : "")} [bold]{passwordName}[/]: "
                );
                if (!string.Equals(Password, RepeatedPassword))
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine(
                        $"[bold red]{passwordName}s aren't the same[/]\nPress any key to Repeat"
                    );
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                AnsiConsole.MarkupLine(Hash.ToSha256(RepeatedPassword));
                #endregion

                #region Get & set hint phrase
                if (NeedsHint)
                {
                    var HintPassword = AnsiConsole.Ask<string>(
                        $"What's your [purple bold]hint[/] message: "
                    );
                    AnsiConsole.MarkupLine(HintPassword);
                }
                #endregion

                Console.Clear();
                #region End of loop
                AnsiConsole.MarkupLine(
                    $"[green]Your{(IsNew ? " new" : "")} [bold]{passwordName}[/] has been set[/]"
                );
                AnsiConsole.MarkupLine("Press any key to exit");
                Console.ReadKey();
                flag = false;
                Console.Clear();
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

        public static void AllowedUseTime(string timeName)
        #region Allowed use time changer prompt maker method
        /// <summary>
        /// Make prompt for changing/adding allowed use time
        /// </summary>
        /// <param name="timeName">Name of time which could be template time or other names</param>
        /// <param name="headerMessage">This message shown to user at first line</param>
        /// <param name="IsNew">If user status was 1 it should be true</param>
        {
            bool flag = true;

            while (flag)
            {
                var newUsingTime = AnsiConsole.Ask<string>(
                    $"What's your [green]new[/] [bold]{timeName}[/]? "
                #region Get & set use time
                );

                // if (DoesInsertValueValid(newUsingTime))
                // {
                AnsiConsole.MarkupLine($"Your new [bold]{timeName}[/] has been set");
                AnsiConsole.MarkupLine("Press any key to exit");
                Console.ReadKey();
                #endregion
                #region End of loop
                flag = false;
                Console.Clear();
                // }
                // else
                // {
                //     AnsiConsole.MarkupLine(
                //         $"[bold]{timeName}s[/] aren't the same\nPress any key to Repeat"
                //     );
                //     Console.ReadKey();
                //     Console.Clear();
                // }
                #endregion
            }
        }
        #endregion

        public static void AllowedTimeOfDay()
        #region Allowed time of day changer prompt maker method
        /// <summary>
        /// Make prompt for changing/adding allowed time of day
        /// </summary>
        /// <param name="headerMessage">This message shown to user at first line</param>
        /// <param name="IsNew">If user status was 1 it should be true</param>
        {
            bool flag = true;

            while (flag)
            {
                var newTimeOfDay = AnsiConsole.Ask<string>(
                    $"What's your [green]new[/] [bold]Time Of Day[/]? "
                #region Get & set start time of day
                );
                #endregion

                // if (DoesInsertValueValid(newTimeOfDay))
                // {
                AnsiConsole.MarkupLine($"Your new [bold]Time Of Day[/] has been set");
                AnsiConsole.MarkupLine("Press any key to exit");
                Console.ReadKey();
                #region Get & set end time of day
                #endregion
                #region End of loop
                flag = false;
                Console.Clear();
                // }
                // else
                // {
                //     AnsiConsole.MarkupLine(
                //         $"[bold]{timeOfDayName}s[/] aren't the same\nPress any key to Repeat"
                //     );
                //     Console.ReadKey();
                //     Console.Clear();
                // }
                #endregion
            }
        }
        #endregion

        public static void UserValidation(string userName)
        /// <summary>
        /// Make prompt for Validate the user
        /// </summary>
        /// <param name="userName">Name of the user which we want to validate</param>
        {
            int wrongPasswordCounter = 0;
            AnsiConsole.Prompt(
                new TextPrompt<string>($"Please insert the [bold red]{userName}[/] password: ")
                    .PromptStyle("green")
                    .Secret(null)
                    .ValidationErrorMessage("[red]That's not a valid password[/]")
                    .Validate(password =>
                    {
                        if (string.Equals(Hash.ToSha256(password), Hash.ToSha256("test")))
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
        #region Repeat process method
        /// <summary>
        /// Jobs we do before repeat a loop
        /// </summary>
        /// <param name="message">An string which shown to the user</param>
        /// <remarks>After calling this method, use Continue to ignore rest of loop</remarks>
        #endregion
        #region Header message handler method
        /// <summary>
        /// Handle header messages in prompts
        /// </summary>
        /// <param name="message">Shown to user if it isn't null</param>
        #endregion
    }
}
