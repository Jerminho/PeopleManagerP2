using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ILoggerFactory consoleLoggerFactory
    = LoggerFactory.Create(config => { config.AddConsole(); });


var connectionString = builder.Configuration.GetConnectionString(nameof(PeopleManagerDbContext));
builder.Services.AddDbContext<PeopleManagerDbContext>(options =>
{
    options.UseLoggerFactory(consoleLoggerFactory);
    options.EnableSensitiveDataLogging();
    options.UseSqlServer(connectionString);
    //options.UseInMemoryDatabase(nameof(PeopleManagerDbContext));
});


builder.Services.AddScoped<OrganizationService>();
builder.Services.AddScoped<PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var peopleManagerDbContext = scope.ServiceProvider.GetRequiredService<PeopleManagerDbContext>();
    if (peopleManagerDbContext.Database.IsInMemory())
    {
        peopleManagerDbContext.Seed();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
