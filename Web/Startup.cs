// <copyright file="Startup.cs" company="Eugene Slutiak">
//     Equipment Controller Project.
// </copyright>

namespace EquipmentControll.Web
{
    using Domain.Models;
    using EquipmentControll.Domain.Models;
    using EquipmentControll.Domain.Repositories;
    using EquipmentControll.Logic;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Default Stratup class to configure application and services.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">IConfiguration instance with default configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets IConfiguration property necessary to configure application and services.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime.
        /// Uses to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection instance that contains services descriptors.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ProjectContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("ProjectDB")));

            services.AddTransient<IRepository<User>, Repository<User>>();
            services.AddTransient<IRepository<Equipment>, Repository<Equipment>>();
            services.AddTransient<IRepository<Record>, Repository<Record>>();

            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<IEquipmentLogic, EquipmentLogic>();
            services.AddTransient<IRecordLogic, RecordLogic>();
        }

        /// <summary>
        /// This method gets called by the runtime.
        /// Uses to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder instance that provides the mechanisms to configure pipeline.</param>
        /// <param name="env">IWebHostEnvironment instance that provides information about the web hosting environments.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedData.EnsureDataPopulated(app.ApplicationServices.CreateScope()
                    .ServiceProvider.GetRequiredService<ProjectContext>());
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
