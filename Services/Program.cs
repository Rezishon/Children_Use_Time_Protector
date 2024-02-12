using Topshelf;

namespace Services;

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
            });
        });
    }
}
