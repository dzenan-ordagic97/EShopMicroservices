var builder = WebApplication.CreateBuilder(args);

//Before build - Add services to the container
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

var app = builder.Build();

//After build - Configure pipeline
app.MapCarter();

app.Run();
