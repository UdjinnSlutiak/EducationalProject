// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace EquipmentControll.Web
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Default Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Default Main method.
        /// Builds and runs HostBuilder with default configuration.
        /// </summary>
        /// <param name="args">Arguments array.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Default CreateHostBuilder method.
        /// Creates HostBuilder with default configuration.
        /// </summary>
        /// <param name="args">Arguments array.</param>
        /// <returns>HostBuilder with default configuration.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
