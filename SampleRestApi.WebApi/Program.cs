using Mediator;
using SampleRestApi.Application.Infrastructure;
using SampleRestApi.Application.Infrastructure.Extensions;
using SampleRestApi.WebApi.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
