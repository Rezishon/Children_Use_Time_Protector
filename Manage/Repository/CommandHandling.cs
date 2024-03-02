using System.Diagnostics;

namespace CommandHandling
{
    public static class Commands
    {
        public static Dictionary<string, string> CommandsDic = new Dictionary<string, string>()
        {
            { "Shutdown", "shutdown.exe /s /f" },
            // ..\..\..\..\..\Services\bin\Debug\net8.0\Services.exe
            { "TurnOnService", @"..\Services\bin\Debug\net8.0\Services.exe install start" },
            { "TurnOffService", @"..\Services\bin\Debug\net8.0\Services.exe uninstall" },
            { "pwd", "pwd" }
        };

        private static void CommandRunner(string CommandNameString)
        {
            var processInfo = new ProcessStartInfo
            {
                Verb = "runas", // Run as administrator
                LoadUserProfile = true,
                FileName = "powershell.exe",
                Arguments = CommandsDic[CommandNameString],
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                {
                Process.Start(processInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Shutdown()
        {
            CommandRunner("Shutdown");
        }

        public static void TurnOnService()
        {
            CommandRunner("TurnOnService");
        }

        public static void TurnOffService()
        {
            CommandRunner("TurnOffService");
        }
    }
}
