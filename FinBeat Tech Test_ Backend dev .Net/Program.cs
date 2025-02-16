using FinBeat_Tech_Test__Backend_dev_.Net.logger.DBLogger;
using FinBeat_Tech_Test__Backend_dev_.Net.logger.FileLogger;

namespace FinBeat_Tech_Test__Backend_dev_.Net
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // устанавливаем файл для логгирования
            //builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            builder.Logging.AddDB();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
