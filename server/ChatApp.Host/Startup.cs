using ChatApp.Data;
using ChatApp.Data.Configuration;
using ChatApp.Host.Hubs;
using ChatApp.Host.Infrastructure;
using ChatApp.Services;
using ChatApp.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ChatApp.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ChatDatabaseSettings>(Configuration.GetSection(nameof(ChatDatabaseSettings)));

            services.AddSingleton<IChatDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ChatDatabaseSettings>>().Value);

            services.AddSingleton<ChatContext>();

            services.AddTransient<IMessagesService, MessagesService>();

            services.AddControllers();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMiddleware<ExceptionMiddleware>();
            }
            else
            {
                app.UseMiddleware<ExceptionMiddleware>();
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(builder => builder.WithOrigins("http://localhost:4300").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat-ws");
            });
        }
    }
}
