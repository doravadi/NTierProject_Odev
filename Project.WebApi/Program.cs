using FluentValidation;
using FluentValidation.AspNetCore;
using Project.Bll.DependencyResolvers;
using Project.WebApi.MapperResolvers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// FluentValidation ekleme - Validation katmanından
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Validation assembly'sinden validator'ları yükle
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Project.Validation"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextService(); //Context class'ının middleware'e eklenmesi
builder.Services.AddRepositoryService(); //Repository servisinin middleware'e eklenmesi
builder.Services.AddManagerService(); //Manager servisinin middleware'e eklenmesi
builder.Services.AddDtoMapperService(); //Dto mapper servisinin middleware'e eklenmesi
builder.Services.AddVmMapperService(); //Vm Mapper servisinin middleware'e eklenmesi

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();