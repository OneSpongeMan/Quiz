using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quiz.BLL.Certificates;
using Quiz.BLL.Services;
using Quiz.DAL.EF;
using Quiz.DAL.EF.Loaders;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

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
            //_builder.Services.AddDbContext<ApplicationContext>();

            string connection = _builder.Configuration.GetConnectionString("DefaultConnections");

            _builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(connection),
                ServiceLifetime.Transient);

            _builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            _builder.Services.AddCors(options =>
            {
                options.AddPolicy("VuePolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            //_builder.Services.AddDefaultIdentity<User>()
            //    .AddRoles<Role>();

            //_builder.Services.AddAutoMapper(typeof(Program));

            _builder.Services.AddTransient<IQuizzLoader, QuizzLoader>();
            _builder.Services.AddTransient<IQuestionLoader, QuestionLoader>();
            _builder.Services.AddTransient<IAnswerLoader, AnswerLoader>();
            _builder.Services.AddTransient<ILogRecordLoader, LogRecordLoader>();
            _builder.Services.AddTransient<IResultLoader, ResultLoader>();
            _builder.Services.AddTransient<IRoleLoader, RoleLoader>();
            _builder.Services.AddTransient<IUserLoader, UserLoader>();

            _builder.Services.AddTransient<IQuizzService, QuizzService>();
            _builder.Services.AddTransient<IQuestionService, QuestionService>();
            _builder.Services.AddTransient<IAnswerService, AnswerService>();
            _builder.Services.AddTransient<ILogRecordService, ILogRecordService>();
            _builder.Services.AddTransient<IResultService, ResultService>();
            _builder.Services.AddTransient<IRoleService, RoleService>();
            _builder.Services.AddTransient<IUserService, UserService>();
            _builder.Services.AddTransient<IPDFCertificateService, PDFCertificateService>();
            _builder.Services.AddSingleton<ICertificateGenerator, PDFCertificateGenerator>();
        }
    }
}
