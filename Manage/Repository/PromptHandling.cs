using Hashing;
using Spectre.Console;

namespace Manage.Repository
{
    public class PromptHandler
    {
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

        public static void PasswordPrompt(
            string passwordName,
            bool IsNew = true,
            bool NeedsHint = false
        )
        // The IsNew should check the root status and then set the true|false
        {
            bool flag = true;

            while (flag)
            {
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

                if (NeedsHint)
                {
                    var HintPassword = AnsiConsole.Ask<string>(
                        $"What's your [purple bold]hint[/] message: "
                    );
                    AnsiConsole.MarkupLine(HintPassword);
                }

            }
        }

        public static void ReturnToMainMenu()
        {
            // An empty method for future use
        }

        public static void AllowedUseTime(string timeName)
        {
            bool flag = true;

            while (flag)
            {
                var newUsingTime = AnsiConsole.Ask<string>(
                    $"What's your [green]new[/] [bold]{timeName}[/]? "
                );

                // if (DoesInsertValueValid(newUsingTime))
                // {
                AnsiConsole.MarkupLine($"Your new [bold]{timeName}[/] has been set");
                AnsiConsole.MarkupLine("Press any key to exit");
                Console.ReadKey();
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
            }
        }

        public static void AllowedTimeOfDay()
        {
            bool flag = true;

            while (flag)
            {
                var newTimeOfDay = AnsiConsole.Ask<string>(
                    $"What's your [green]new[/] [bold]Time Of Day[/]? "
                );

                // if (DoesInsertValueValid(newTimeOfDay))
                // {
                AnsiConsole.MarkupLine($"Your new [bold]Time Of Day[/] has been set");
                AnsiConsole.MarkupLine("Press any key to exit");
                Console.ReadKey();
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
            }
        }

        public static void UserValidation(string userName)
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
    }
}
