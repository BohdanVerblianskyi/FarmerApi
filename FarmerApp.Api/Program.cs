using FarmerApp.Api.Services;
using FarmerApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(o => { });

builder.Services.AddCors(
    options =>
        options.AddPolicy("FarmerApi",
            b =>
                b
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
        ));

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<FarmerDbContext>(options =>
    options.UseNpgsql(connectionString, b =>
        b.MigrationsAssembly("FarmerApp.Api")));


builder.Services.AddScoped<ModelTypeService>();
builder.Services.AddScoped<SpendService>();
builder.Services.AddScoped<WarehouseReceptionService>();
builder.Services.AddScoped<WarehouseService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "FarmerApp", Version = "v1" })
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors("FarmerApi");
app.UseAuthorization();


app.MapControllers();

app.Run();