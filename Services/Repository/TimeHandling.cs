namespace TimeHandling
{
    public static class Time
    {
        public static bool? Allowed()
        {
            var UsedDurationTime = LogHandling.LogFile.LogReader().Length * 10;
        }
    }
}
