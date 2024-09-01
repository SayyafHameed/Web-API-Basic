

using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using WebAPIBasic;
using WebAPIBasic.Authentication;
using WebAPIBasic.Data;
using WebAPIBasic.Filters;
using WebAPIBasic.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddLogging(cnf => cnf.AddDebug());

//builder.Services.Configure<AttachmentsOptions>(builder.Configuration.GetSection("Attachments"));


//var attachmentsOption = builder.Configuration.GetSection("Attachments").Get<AttachmentsOptions>();
//builder.Services.AddSingleton(attachmentsOption);

// var attacheOpt = new AttachmentsOptions();
// builder.Configuration.GetSection("Attachments").Bind(attacheOpt);
// builder.Services.AddSingleton(attacheOpt);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add< LogActivityFilter>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); //trust server certificate=true

//builder.Services.AddScoped<WeatherForecastService>();

builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("basic", null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ReateLimitingMiddleware>();
app.UseMiddleware<ProfilingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();

app.Run();
