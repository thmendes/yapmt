using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace YAPMT.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {

                })
                .UseUrls("http://0.0.0.0:5000")
                .Build();
    }
}
