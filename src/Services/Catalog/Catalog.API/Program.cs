var builder = WebApplication.CreateBuilder(args);

//Before build - Add services to the container


var app = builder.Build();

//After build - Configure pipeline

app.Run();
