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

        public static string Say_Database_File_Format()
        {
            return Say_App_Name_In_Brief().ToLower();
        }
    }
    #endregion
}
