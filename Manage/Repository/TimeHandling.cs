namespace Children_Use_Time_Protector.Repository.TimeHandling
{
    public static class Time
    {
        #region Allowed to use method
        /// <summary>
        /// User allowed to use or not.
        /// </summary>
        /// <returns>
        ///     <term>Bool?</term>
        ///     <description>True for allowed | False for not allowed | null for 10 minute remaining</description>
        /// </returns>
        public static bool? Allowed()
        {
            #region Initialization
            int UsedDurationTime = 0;
            int AllowedDurationTime = 0;
            #endregion

            #region Calculate variables
            try
            {
                UsedDurationTime = LogHandling.LogFile.LogReader().Length * 10;
                AllowedDurationTime =
                    ConfigHandling.ServicePart.AllowedDuration()
                    + ConfigHandling.ServicePart.TempAllowedDuration();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region Condition handling
            if (UsedDurationTime >= AllowedDurationTime)
                return false;
            else if (UsedDurationTime == (AllowedDurationTime - 10))
                return null;
            else
                return true;
            #endregion
        }
        #endregion
    }
}
