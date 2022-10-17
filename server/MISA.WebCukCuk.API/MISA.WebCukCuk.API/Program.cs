using MISA.WebCukCuk.BL.BaseBL;
using MISA.WebCukCuk.BL.ConversionUnitBL;
using MISA.WebCukCuk.BL.MaterialBL;
using MISA.WebCukCuk.BL.StockBL;
using MISA.WebCukCuk.BL.UnitBL;
using MISA.WebCukCuk.DL.BaseDL;
using MISA.WebCukCuk.DL.ConversionUnitDL;
using MISA.WebCukCuk.DL.MaterialDL;
using MISA.WebCukCuk.DL.StockDL;
using MISA.WebCukCuk.DL.UnitDL;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

// Add services to the container.
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));

builder.Services.AddScoped<IMaterialBL, MaterialBL>();
builder.Services.AddScoped<IMaterialDL, MaterialDL>();

builder.Services.AddScoped<IUnitBL, UnitBL>();
builder.Services.AddScoped<IUnitDL, UnitDL>();

builder.Services.AddScoped<IStockBL, StockBL>();
builder.Services.AddScoped<IStockDL, StockDL>();

builder.Services.AddScoped<IConversionUnitBL, ConversionUnitBL>();
builder.Services.AddScoped<IConversionUnitDL, ConversionUnitDL>();

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();
