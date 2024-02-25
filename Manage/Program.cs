using CommandHandling;
using ConfigHandling;
using Hashing;
using Spectre.Console;

namespace Children_Use_Time_Protector;

class Program
{
    static void Main(string[] args)
    {
        int wrongPasswordCounter = 0;
        AnsiConsole.Prompt(
            new TextPrompt<string>("Please insert the [bold red]ROOT[/] password: ")
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

        bool flag = true;

        while (flag)
        {
            Dictionary<string, int> MainMenuPartsDictionary =
                new()
                {
                    { "Root", 0 },
                    { "Service", 1 },
                    { "Exit", 2 },
                    { "Timing", 3 }
                };
            var chosenPartOnMainMenu = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What part you want to change?")
                    .PageSize(5)
                    .HighlightStyle(Style.Parse("purple bold"))
                    .AddChoices(["Root", "Service", "Timing", "Exit"])
            );
            switch (MainMenuPartsDictionary[chosenPartOnMainMenu])
            {
                case 0:
                    Dictionary<string, int> RootMenuPartsDictionary =
                        new()
                        {
                            { "Change the root password", 0 },
                            { "Change The root recovery password", 1 },
                            { "Main Menu", 2 }
                        };
                    var chosenPartOnRootMenu = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("What part you want to change?")
                            .PageSize(3)
                            .HighlightStyle(Style.Parse("purple bold"))
                            .AddChoices(
                                [
                                    "Change the root password",
                                    "Change The root recovery password",
                                    "Main Menu"
                                ]
                            )
                    );
                    switch (RootMenuPartsDictionary[chosenPartOnRootMenu])
                    {
                        case 0:

                            bool flagRoot = true;
                            while (flagRoot)
                            {
                                var newPassword = AnsiConsole.Ask<string>(
                                    "What's your [green]new[/] password? "
                                );
                                AnsiConsole.MarkupLine(Hash.ToSha256(newPassword));
                                var newRepeatedPassword = AnsiConsole.Ask<string>(
                                    "Repeat your [green]new[/] password: "
                                );
                                AnsiConsole.MarkupLine(Hash.ToSha256(newRepeatedPassword));
                                if (
                                    string.Equals(
                                        Hash.ToSha256(newPassword),
                                        Hash.ToSha256(newRepeatedPassword)
                                    )
                                )
                                {
                                    AnsiConsole.MarkupLine("Your new password has been set");
                                    AnsiConsole.MarkupLine("Press any key to exit");
                                    Console.ReadKey();
                                    flagRoot = false;
                                    Console.Clear();
                                }
                                else
                                {
                                    AnsiConsole.MarkupLine(
                                        "Passwords aren't the same\nPress any key to Repeat"
                                    );
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                            }

                            break;
                        case 1:

                            bool flagRecoveryRoot = true;
                            while (flagRecoveryRoot)
                            {
                                AnsiConsole.MarkupLine("choosing a simple password recommended ");
                                var newRecoveryPassword = AnsiConsole.Ask<string>(
                                    "What's your [green]new[/] recovery password? "
                                );
                                AnsiConsole.MarkupLine(Hash.ToSha256(newRecoveryPassword));
                                var newRepeatedRecoveryPassword = AnsiConsole.Ask<string>(
                                    "Repeat your [green]new[/] recovery password: "
                                );
                                AnsiConsole.MarkupLine(Hash.ToSha256(newRepeatedRecoveryPassword));
                            }

                            break;
                        case 2:
                            break;
                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
    }
}
