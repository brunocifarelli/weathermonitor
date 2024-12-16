using APIWeather.WebAPP;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var connectionStringMySql = builder.Configuration.GetConnectionString("ConnectionMySql");

builder.Services
    .AddConnections()
    .AddServiceDbContext(connectionStringMySql)
    .AddHttpContextAccessor()
    .AddApplicationServices()
    .AddServiceRepositories()
    .AddJwtAuthorization(jwtSettings)
    .AddCorsConfiguration();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

