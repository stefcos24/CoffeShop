using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                DotNetEnv.Env.Load();
            }
            catch
            {
                // ignore (e.g. no .env file)
            }

            // Convenience: if a full connection string isn't set, build one from common `.env` pieces.
            if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")))
            {
                var password =
                    Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD") ??
                    Environment.GetEnvironmentVariable("SA_PASSWORD");

                var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "127.0.0.1";
                var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "1433";
                var db = Environment.GetEnvironmentVariable("DB_NAME") ?? "CoffeShop";
                var user = Environment.GetEnvironmentVariable("DB_USER") ?? "sa";

                if (!string.IsNullOrWhiteSpace(password))
                {
                    var cs = $"Server={host},{port};Database={db};User Id={user};Password={password};TrustServerCertificate=True;MultipleActiveResultSets=True";
                    Environment.SetEnvironmentVariable("ConnectionStrings__DefaultConnection", cs);
                }
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
