using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

/* her vores program starter */

namespace AAOAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /* her bliver funktionen createHostBuilder kaldt */
            CreateHostBuilder(args).Build().Run();
        }

        /* funktionen createHostBuilder som returnerer en IHostBuilder. det er denne kode som opretter vores 
         webapplikation */
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
