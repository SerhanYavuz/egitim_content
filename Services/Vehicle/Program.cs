using Microsoft.EntityFrameworkCore;
using Vehicle;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
if(connectionString == null){
    connectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=admin";
}
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment()){
    app.UseSwagger(); 
    app.UseSwaggerUI();
//}
// TODO - canlı ortamda https açılmalı
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using(var scope= app.Services.CreateScope()){
    var dataContext= scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dataContext.Database.Migrate();
}

app.Run();
