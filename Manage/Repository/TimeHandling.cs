namespace TimeHandling
{
    public static class Time
    {
        public static bool? Allowed()
        {
            int UsedDurationTime = 0;
            int AllowedDurationTime = 0;

            try
            {
                UsedDurationTime = LogHandling.LogFile.LogReader().Length * 10;
                AllowedDurationTime =
                    ConfigHandling.ServicePart.AllowedDuration()
                    + ConfigHandling.ServicePart.TempAllowedDuration();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
