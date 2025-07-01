using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Application.Abstractions;
using RepositoryPatternWithUOW.Domain.Interfaces;
using RepositoryPatternWithUOW.Infrastructure.Persistence;
using RepositoryPatternWithUOW.Infrastructure.Repositories;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;
using RepositoryPatternWithUOW.Application;
using FluentValidation;
using RepositoryPatternWithUOW.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// dbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, 
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
    )
);

// mapster
var mappingConfig = TypeAdapterConfig.GlobalSettings;
mappingConfig.Scan(typeof(ApplicationAssemblyMarker).Assembly);

builder.Services.AddSingleton<IMapper>(new Mapper(mappingConfig));

// Configure FluentValidation
builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
// register services
//builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthorService, AuthorService>();

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
