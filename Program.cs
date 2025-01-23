using Microsoft.EntityFrameworkCore;
using TRIAL.Persistence.Repository;
using TRIAL.Services;
using TRIAL.Persistence;
using TRIAL.Services.Implementations;
// using VerificationRegisterN;
// using AssigningRoleU;
using Microsoft.Extensions.Configuration;
using EmailSending;
//***using TRIAL.Middleware;
//using exceptionHandlingMiddleware;

var builder = WebApplication.CreateBuilder(args);


// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();



// Add services to the container.
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("MyConnection");
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
//builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
//builder.Services.AddScoped<IEmailTestService, EmailTestService>();
builder.Services.AddScoped<ISubjectsService, SubjectsService>(); //this method registers the interface and its implementation with the DI container in ASP.NET Core.
builder.Services.AddScoped<IHomeworkTeacherService, HomeworkTeacherService>();
builder.Services.AddScoped<IStudentHomeworkService, StudentHomeworkService>();
builder.Services.AddScoped<HomeworkCleanupService>(); // Register the background service
builder.Services.AddScoped<Emailsending>();
// builder.Services.AddScoped<VerificationRegister>();
// builder.Services.AddScoped<AssigningRole>();
//
// Register the necessary services for RoleMiddleware
//***builder.Services.AddScoped<AppDBContext>();
//***builder.Services.AddScoped<RoleMiddleware>();

//
//builder.Services.AddScoped<ExceptionHandlingMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use middleware in the app
//***app.UseMiddleware<RoleMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });
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
app.Run();

