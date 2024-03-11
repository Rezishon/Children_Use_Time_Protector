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
        #endregion

        /// <summary>
        /// Runs valid commands in powershell
        /// </summary>
        /// <param name="CommandNameString">Command name comes from dictionary - Added for validation commands</param>
        /// <param name="WantResult">if command results needed this parameter should be true</param>
        /// <returns>
        ///     <term>String</term>
        ///     <description>The result of run command will be return in shape of what powershell gives</description>
        /// </returns>
        public static string CommandRunner(string CommandNameString, bool WantResult = false)
        {
            #region create process object
            var processInfo = new ProcessStartInfo
            {
                Verb = "runas", // Run as administrator
                LoadUserProfile = true,
                FileName = "powershell.exe",
                Arguments = CommandsDictionary[CommandNameString],
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            #endregion

            #region Execute process
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
            #endregion
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
