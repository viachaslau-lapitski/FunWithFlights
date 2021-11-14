using FunWithFlights.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRoutesAggregator, RoutesAggregator>();
builder.Services.AddTransient<IRoutesDataSource, RoutesDataSourceCached>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

//Add base endpoint/healthcheck
app.MapGet("/", () => "FunWithFlights Api");

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
