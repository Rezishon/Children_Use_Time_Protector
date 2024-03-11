using System.Diagnostics;

namespace CommandHandling
{
    /// <summary>
    /// Class will handle commands which should be run in powershell
    /// </summary>
    public static class Commands
    {
        #region Commands dictionary
        /// <summary>
        /// Contains each command name with its actual command string
        /// </summary>
        public static Dictionary<string, string> CommandsDictionary = new Dictionary<
            string,
            string
        >()
        {
            { "Shutdown", "shutdown.exe /s /f" },
            // ..\..\..\..\..\Services\bin\Debug\net8.0\Services.exe
            { "TurnOnService", @"..\Services\bin\Debug\net8.0\Services.exe install start" },
            { "TurnOffService", @"..\Services\bin\Debug\net8.0\Services.exe uninstall" },
            { "pwd", "pwd" }
        };

        public static string CommandRunner(string CommandNameString, bool WantResult = false)
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
                using (Process process = new Process { StartInfo = processInfo })
                {
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    if (WantResult)
                    {
                        return output;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "";
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

        public static string Pwd()
        {
            return CommandRunner("pwd", true).Split("\n")[3];
        }
    }
}
