using System.Text.RegularExpressions;

namespace Children_Use_Time_Protector.Repository
{
    #region Messages class
    /// <summary>
    /// Include messages we want to show to user
    /// </summary>
    public class Messages
    {
        #region Application name method
        /// <summary>
        /// App Name
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>The main name of application</description>
        /// </returns>
        public static string Say_App_Name()
        {
            return "Children Use Time Protector";
        }
        #endregion

        #region Application brief name method
        /// <summary>
        /// Brief name of application
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>Upper case letters of application name</description>
        /// </returns>
        public static string Say_App_Name_In_Brief()
        {
            return Regex.Replace(Say_App_Name(), "[^A-Z]", "");
        }
        #endregion

        #region Application database file format name method
        /// <summary>
        /// Database file format name
        /// </summary>
        /// <returns>Lower case of first letters of application name</returns>
        public static string Say_Database_File_Format()
        {
            return Say_App_Name_In_Brief().ToLower();
        }
        #endregion

        #region Time of day - Error value
        /// <summary>
        /// Error message which should be shown to user when entered wrong value
        /// </summary>
        /// <returns>
        ///     <term>String</term>
        ///     <description>The message</description>
        /// </returns>
        public static string TimeOfDayErrorMessage()
        {
            return "[red]Inserted time has wrong format. It must be like [[two digit number]][bold]:[/][[two digit number]][/]\nPress any key to repeat";
        }
        #endregion

        #region Prompt text
        /// <summary>
        /// Text of prompts while asking
        /// </summary>
        /// <param name="name">The name of prompt which you are asking user</param>
        /// <param name="IsNew">Is this config should be change or will set for the first time</param>
        /// <returns>
        ///     <term>String</term>
        ///     <description>The message</description>
        /// </returns>
        public static string PromptText(string name, bool IsNew)
        {
            return $"What's your{(IsNew ? " [green]new[/]" : "")} [bold]{name}[/]? ";
        }
        #endregion

        #region Finished setting config
        /// <summary>
        ///
        /// </summary>
        /// <param name="name">The name of prompt which you asked user</param>
        /// <param name="IsNew">Is this config should be change or will set for the first time</param>
        /// <returns>
        ///     <term>String</term>
        ///     <description>The message</description>
        /// </returns>
        public static string FinishedSettingConfig(string name, bool IsNew)
        {
            return $"[green]Your{(IsNew ? " new" : "")} [bold]{name}[/] has been set[/]\nPress any key to continue";
        }
        #endregion
    }
    #endregion
}
