using Core.Models;
using FluentValidation.AspNetCore;
using Infrastructure.Core;
using Infrastructure.Mapper;
using Infrastructure.Requests;
using MediatR;
using static Api.Validators.InterfaceValidators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(GetDataFromEnumRequest));
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<AddInterfaceVMValidator>());
builder.Services.AddAutoMapper(typeof(HistoryVMProfile).Assembly);
builder.Services.AddDbContext<DBContext>();
builder.Services.AddCors();

// Add generic requests
builder.Services.AddTransient<IRequestHandler<UnLockEntityRequest<CTInterface>, EmptyRequestResult>, UnLockEntityRequestHandler<CTInterface>>();
builder.Services.AddTransient<IRequestHandler<RestoreOrArchiveEntityRequest<CTInterface>, EmptyRequestResult>, RestoreOrArchiveEntityRequestHandler<CTInterface>>();

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

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
