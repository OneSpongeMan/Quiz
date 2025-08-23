using Quiz.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.AspNetCore.Identity;
using Quiz.Shared.Models;
using System.Threading.Tasks;

namespace Quiz.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); 
            var startup = new StartUp(builder);
            startup.ConfigureServices();

            var app = builder.Build();
            var seeder = new Seeder(app.Services);
            await seeder.SeedDatabase();

            //app.MapGet("/", () => "Hello World!");

            app.UseCors("VyePolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();            

            app.Run();
        }
    }
}
