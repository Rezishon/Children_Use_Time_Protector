namespace TimeHandling
{
    public static class Time
    {
        public static bool? Allowed()
        {
            var UsedDurationTime = LogHandling.LogFile.LogReader().Length * 10;
            var AllowedDurationTime =
                ConfigHandling.ServicePart.AllowedDuration()
                + ConfigHandling.ServicePart.TempAllowedDuration();

            if (UsedDurationTime >= AllowedDurationTime)
            {
                return false;
            }
        }
    }
}
