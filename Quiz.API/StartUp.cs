//using Quiz.Shared.Settings;
//using Microsoft.OpenApi.Models;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using FluentMigrator.Runner;
//using Quiz.DAL.EF.Migrations;
using Quiz.BLL;
using Quiz.DAL.EF;
using Quiz.DAL.EF.Loaders;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;
using System.Runtime;
using System.Text;

namespace Quiz.API
{
    internal class StartUp
    {
        WebApplicationBuilder _builder;

        public StartUp(WebApplicationBuilder builder)
        {
            _builder = builder;
        }

        public void ConfigureServices()
        {
            _builder.Services.AddControllers();
            _builder.Services.AddEndpointsApiExplorer();
            _builder.Services.AddAuthorization();

            AddInterfaceConnectionsToService();            
        }

        private void AddInterfaceConnectionsToService()
        {
            string connection = _builder.Configuration.GetConnectionString("DefaultConnections");

            _builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(connection),
                ServiceLifetime.Transient);

            _builder.Services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Stores.ProtectPersonalData = true;
            })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            //_builder.Services.AddAutoMapper(typeof(Program));

            _builder.Services.AddTransient<IQuizzLoader, QuizzLoader>();
            _builder.Services.AddTransient<IQuestionLoader, QuestionLoader>();
            _builder.Services.AddTransient<IAnswerLoader, AnswerLoader>();
            _builder.Services.AddTransient<ILogRecordLoader, LogRecordLoader>();
            _builder.Services.AddTransient<IResultLoader, ResultLoader>();
            _builder.Services.AddTransient<IRoleLoader, RoleLoader>();
            _builder.Services.AddTransient<IUserLoader, UserLoader>();
        }
    }
}
