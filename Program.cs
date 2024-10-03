using Microsoft.EntityFrameworkCore;
using TRIAL.Persistence.Repository;
using TRIAL.Services;
using TRIAL.Persistence;
using TRIAL.Services.Implementations;
using VerificationRegisterN;

var builder = WebApplication.CreateBuilder(args);

// Register the IEmailService with its implementation
builder.Services.AddScoped<IEmailService, EmailService>();

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("MyConnection");
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISubjectsRetrive, SubjectsRetrive>(); //this method registers the interface and its implementation with the DI container in ASP.NET Core.
builder.Services.AddScoped<IHomeworkTeacherService, HomeworkTeacherService>();
builder.Services.AddScoped<IStudentHomeworkService, StudentHomeworkService>();
builder.Services.AddScoped<HomeworkCleanupService>(); // Register the background service
builder.Services.AddScoped<VerificationRegister>();
//

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Detailed error pages in development
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Custom error handling for production
    app.UseHsts(); // Enforce HTTPS
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
