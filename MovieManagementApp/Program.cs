using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.Lifetime.ApplicationStarted.Register(() =>
{
    var serverAddresses = app.Urls;
    var url = serverAddresses.FirstOrDefault() +"/swagger";
    try
    {
        Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
    }
    catch { }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
