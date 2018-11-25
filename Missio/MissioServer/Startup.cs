using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Missio.Users;
using MissioServer.Services;
using MissioServer.Services.Services;

namespace MissioServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IUserService, UsersService>();
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<ITimeService, TimeService>();
            services.AddScoped<IWebClientService, WebClientService>();
            services.AddDbContext<MissioContext>(opt => opt.UseInMemoryDatabase("UsersList"));
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, MissioContext missioContext)
        {
            missioContext.Database.EnsureCreated(); 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseMiddleware<HandleUserCredentialsExceptions>();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
