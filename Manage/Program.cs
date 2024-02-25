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
        }
    }
}
