using Topshelf;

namespace LogHandling;

class Program
{
    static void Main(string[] args)
    {
        // Look at Topshelf documents on their website for more details
        // https://topshelf.readthedocs.io/en/latest/index.html

        var exitCode = HostFactory.Run(x =>
        {
            x.Service<CUTP>(s =>
            {
                s.ConstructUsing(cutp => new CUTP());
                s.WhenStarted(ctup => ctup.Start());
                s.WhenStopped(ctup => ctup.Stop());
            });

            x.RunAsLocalSystem();

            x.SetServiceName("ChildrenUseTimeProtector");
            x.SetDisplayName("Children Use Time Protector");
            x.SetDescription(
                "This service is CUTP's service which controls the computer using time for children"
            );
        });

        int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
        Environment.ExitCode = exitCodeValue;
    }
}
