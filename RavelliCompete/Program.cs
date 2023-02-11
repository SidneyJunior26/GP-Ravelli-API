using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services.Athletes;
using Microsoft.AspNetCore.Diagnostics;
using MySqlConnector;
using RavelliCompete.Endpoints.Security;
using RavelliCompete.Endpoints.Subcategory.Get;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GP Ravelli API", Version = "v1" });

    var security = new Dictionary<string, IEnumerable<string>>
    {
        {"Bearer", new string[] { }},
    };

    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme {
            Description = @"Cabeçalho de autorização JWT usando o esquema Bearer. \r\n\r\n 
                      Digite 'Bearer' [espaço] e, em seguida, seu token na entrada de texto abaixo.
                      \r\n\r\nExemplo: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var connectionString = builder.Configuration["ConnectionStrings:MySql"];

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.MapInboundClaims = false;
    options.TokenValidationParameters = new TokenValidationParameters() {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
});

var serverVersion = new MariaDbServerVersion(new Version(10, 4, 12));
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseMySql(connectionString, serverVersion));

builder.Services.AddScoped<QueryAllAthletesWithPagination>();

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//        builder =>
//        {
//            builder.AllowAnyOrigin()
//                                .AllowAnyHeader()
//                                .AllowAnyMethod();
//        });
//});

builder.WebHost.UseUrls("http://localhost:3031");

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GP Ravelli V1");
    });
}



/* Adding EndPoints */

//app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handler);

//// Medical Records
//app.MapMethods(MedicalRecordGetByAthleteId.Template, MedicalRecordGetByAthleteId.Methods, MedicalRecordGetByAthleteId.Handler);

//// Events
//app.MapMethods(EventGetAll.Template, EventGetAll.Methods, EventGetAll.Handler);
//app.MapMethods(EventGetAllActives.Template, EventGetAllActives.Methods, EventGetAllActives.Handler);
//app.MapMethods(EventGetAllComing.Template, EventGetAllComing.Methods, EventGetAllComing.Handler);
//app.MapMethods(EventGyById.Template, EventGyById.Methods, EventGyById.Handler);
//app.MapMethods(EventDeleteById.Template, EventDeleteById.Methods, EventDeleteById.Handler);

//// Subcategory
//app.MapMethods(SubcategoryGetAllFiltered.Template, SubcategoryGetAllFiltered.Methods, SubcategoryGetAllFiltered.Handler);

//// Regulation
//app.MapMethods(RegulationGetByEventId.Template, RegulationGetByEventId.Methods, RegulationGetByEventId.Handler);

app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext http) => {
    var error = http.Features?.Get<IExceptionHandlerFeature>()?.Error;

    if (error != null) {
        switch (error) {
            case MySqlException:
                return Results.Problem(title: "Databse out", statusCode: 500);
            case FormatException:
                return Results.Problem(title: "Error to convert data to other type. Confirm all information sent", statusCode: 500);
        }
    }

    return Results.Problem(title: "An error ocurred", statusCode: 500);
});

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();

