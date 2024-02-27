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
        }
    }
}
