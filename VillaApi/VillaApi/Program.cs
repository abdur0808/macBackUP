using System;
using Microsoft.EntityFrameworkCore;
using Serilog;
using VillaAPI.Data;
using VillaAPI.Logger;
using VillaAPI.Mapper;
using VillaAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Create serilog and in the builder host
//Log.Logger = new LoggerConfiguration().
//    MinimumLevel.
//    Debug().
//    WriteTo.
//    File("log/VillaApi").CreateLogger();

//builder.Host.UseSerilog();

// Add services to the container.

// if we want to set only application/json then we will set this options option => { option.ReturnHttpNotAcceptable = true;
// and if you want to allow the xml as well then do this-AddXmlDataContractSerializerFormatters()
builder.Services.AddControllers(
    //option => { option.ReturnHttpNotAcceptable = true; }
    ).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("VillaAPI"));


// you have to register the Other Interface  Impleentation & custom log 
builder.Services.AddSingleton<ILogging, Logging>();
builder.Services.AddScoped<IVillaRepository, VillaRepositoty>();

var app = builder.Build();
ApplicationDbContext.AddTestData(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
