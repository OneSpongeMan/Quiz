using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quiz.BLL.Certificates;
using Quiz.BLL.Services;
using Quiz.DAL.EF;
using Quiz.DAL.EF.Loaders;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;
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
            AddAutentificationToServices();
        }

        private void AddInterfaceConnectionsToService()
        {
            //_builder.Services.AddDbContext<ApplicationContext>();

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
                options.User.RequireUniqueEmail = true;
            })
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

            _builder.Services.AddAutoMapper(typeof(MappingProfiles));

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
            _builder.Services.AddTransient<ILogRecordService, LogRecordService>();
            _builder.Services.AddTransient<IResultService, ResultService>();
            _builder.Services.AddTransient<IRoleService, RoleService>();
            _builder.Services.AddTransient<IUserService, UserService>();
            _builder.Services.AddTransient<IPDFCertificateService, PDFCertificateService>();
            _builder.Services.AddTransient<ICertificateGenerator, PDFCertificateGenerator>();
        }

        private void AddAutentificationToServices()
        {
            _builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = _builder.Configuration["JWT:Issuer"],
                    ValidAudience = _builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_builder.Configuration["JWT:Key"]))
                };
            });
        }
    }
}
