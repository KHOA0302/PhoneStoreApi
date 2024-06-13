using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Configuration.AddJsonFile(
    path: "ocelot.json",
    optional: false,
    reloadOnChange: true);

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            policy =>
            {
                policy.AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
            });
    });


    builder.Services.AddOcelot(builder.Configuration);
}

var app = builder.Build();
{
    app.MapGet("/", () => "Hello World!");
    app.MapControllers();
    app.UseCors();
    await app.UseOcelot();
    //
    app.Run();
}


