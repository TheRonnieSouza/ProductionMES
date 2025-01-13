using MediatR;
using ProductionMES.Application.Command.AddProduction;
using ProductionMES.Core.Interfaces;
using ProductionMES.Infrastructure.ProductionRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<AddProductionCommand>();
});
builder.Services.AddTransient<IPipelineBehavior<AddProductionCommand, bool>, AddProduction_ValidateTraceabilityBehavior>();
builder.Services.AddTransient<IPipelineBehavior<AddProductionCommand, bool>, AddProduction_ValidateMaskBehavior>();
builder.Services.AddTransient<IPipelineBehavior<AddProductionCommand, bool>, AddProduction_ValidateOpAvaliableBehavior>();
builder.Services.AddTransient<IPipelineBehavior<AddProductionCommand, bool>, AddProduction_ValidateProductOfPartExistBehavior>();
builder.Services.AddTransient<IPipelineBehavior<AddProductionCommand, bool>, AddProduction_ValidateWorkflowBehavior>();


builder.Services.AddScoped<IProductionRepository,ProductionRepository>();
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
