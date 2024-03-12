using System.Text.RegularExpressions;

namespace Children_Use_Time_Protector.Repository
{
    #region Messages class
    /// <summary>
    /// Include messages we want to show to user
    /// </summary>
    public class Messages
    {
        public static string Say_App_Name()
        {
            return "Children Use Time Protector";
        }

        public static string Say_App_Name_In_Brief()
        {
            return Regex.Replace(Say_App_Name(), "[^A-Z]", "");
        }

        public static string Say_Database_File_Format()
        {
            return Say_App_Name_In_Brief().ToLower();
        }
    }
    #endregion
}
