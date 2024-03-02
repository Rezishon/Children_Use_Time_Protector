namespace TimeHandling
{
    public static class Time
    {
        public static bool? Allowed()
        {
            int UsedDurationTime = 0;
            int AllowedDurationTime = 0;
                UsedDurationTime = LogHandling.LogFile.LogReader().Length * 10;
        }
    }
}
