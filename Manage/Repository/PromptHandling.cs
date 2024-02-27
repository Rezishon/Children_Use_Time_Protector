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
        {
            bool flag = true;

            while (flag)
            {
                var Password = AnsiConsole.Ask<string>(
                    $"What's your{(IsNew ? " [green]new[/]" : "")} [bold]{passwordName}[/]? "
                );
                AnsiConsole.MarkupLine(Hash.ToSha256(Password));

                var RepeatedPassword = AnsiConsole.Ask<string>(
                    $"Repeat your{(IsNew ? " [green]new[/]" : "")} [bold]{passwordName}[/]: "
                );
                AnsiConsole.MarkupLine(Hash.ToSha256(RepeatedPassword));

                if (NeedsHint)
                {
                    var HintPassword = AnsiConsole.Ask<string>(
                        $"What's your [purple bold]hint[/] message: "
                    );
                    AnsiConsole.MarkupLine(HintPassword);
                }

                if (string.Equals(Hash.ToSha256(Password), Hash.ToSha256(RepeatedPassword)))
                {
                    AnsiConsole.MarkupLine(
                        $"Your{(IsNew ? " new" : "")} [bold]{passwordName}[/] has been set"
                    );
                    AnsiConsole.MarkupLine("Press any key to exit");
                    Console.ReadKey();
                    flag = false;
                    Console.Clear();
                }
            }
        }
    }
}
