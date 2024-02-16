using System.Diagnostics;

namespace CommandHandling
{
    public static class Commands
    {
        public static Dictionary<string, string> CommandsDic = new Dictionary<string, string>()
        {
            { "Shutdown", "shutdown.exe /s /f" },
            // ..\..\..\..\..\Services\bin\Debug\net8.0\Services.exe
            {
                "TurnOnService",
                @"rojects\Children_Use_Time_Protector\Services\bin\Debug\net8.0\Services.exe install start"
            },
            { "TurnOffService", @"..\Services\bin\Debug\net8.0\Services.exe uninstall" }
        };

        private static void CommandRunner(string CommandNameString)
        {
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    Verb = "runas", // Run as administrator
                    LoadUserProfile = true,
                    FileName = "powershell.exe",
                    Arguments = CommandsDic[CommandNameString],
                    RedirectStandardOutput = false,
                    UseShellExecute = true,
                    CreateNoWindow = true
                };

                Process.Start(processInfo);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e);
            }
        }
    }
}
