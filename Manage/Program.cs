using CommandHandling;
using ConfigHandling;
using Hashing;
using Spectre.Console;

namespace Children_Use_Time_Protector;

class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.Prompt(
            new TextPrompt<string>("Please insert the [bold red]ROOT[/] password: ")
                .PromptStyle("green")
                .Secret(null)
                .ValidationErrorMessage("[red]That's not a valid password[/]")
    }
}
