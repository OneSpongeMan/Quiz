using Quiz.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Quiz.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args); 
            var startup = new StartUp(builder);
            startup.ConfigureServices();

            var app = builder.Build();

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
