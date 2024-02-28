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
        // User validation
        PromptHandler.UserValidation("Root");

        // Main menu loop flag
        bool flag = true;

        // Main menu loop
        while (flag)
        {
            // Create main menu
            switch (PromptHandler.MenuPrompt(["Root", "Service", "Exit"])[1])
            {
                case 0:
                    break;
                case 1:
                case 2:
                    break;
            }
        }
    }
}
