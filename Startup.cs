using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

namespace W3WPTest
{
    public class Startup
    {
        private IHostingEnvironment m_env;
        private IApplicationEnvironment m_appEnv;
        public IConfiguration Configuration;

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            m_env = env;
            m_appEnv = appEnv;

            // Get our configuration from config.json (defaults), config.env.json (env 
            // defaults), and lastly overriding any of that with specific environment
            // variables (for secure keys, passwords, etc only present on final servers).
            Configuration = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, ILogger<Startup> logger)
        {
            loggerFactory.MinimumLevel = LogLevel.Debug;
            loggerFactory.AddConsole(minLevel: LogLevel.Verbose);

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            // Serve views and the API.
            app.UseMvc();
        }
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
