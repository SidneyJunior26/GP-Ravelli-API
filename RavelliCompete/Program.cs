using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RavelliCompete.Endpoints.Atletas.Delete;
using RavelliCompete.Endpoints.Atletas.Get;
using RavelliCompete.Endpoints.Atletas.GetAll;
using RavelliCompete.Endpoints.Atletas.GetConfirmPassword;
using RavelliCompete.Endpoints.Atletas.Post;
using RavelliCompete.Endpoints.Atletas.Put;
using RavelliCompete.Endpoints.Events.Get;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services.Athletes;
using Microsoft.AspNetCore.Diagnostics;
using MySqlConnector;
using RavelliCompete.Endpoints.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/* Adding EndPoints */

// Athletes
app.MapMethods(AthletesGetAll.Template, AthletesGetAll.Methods, AthletesGetAll.Handler);
app.MapMethods(AthletesGetByCpf.Template, AthletesGetByCpf.Methods, AthletesGetByCpf.Handler);
app.MapMethods(AthletesGetById.Template, AthletesGetById.Methods, AthletesGetById.Handler);
app.MapMethods(AthleteConfirmPassword.Template, AthleteConfirmPassword.Methods, AthleteConfirmPassword.Handler);

app.MapMethods(AthletePost.Template, AthletePost.Methods, AthletePost.Handler);
app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handler);

app.MapMethods(AthletePut.Template, AthletePut.Methods, AthletePut.Handler);

app.MapMethods(AthleteDelete.Template, AthleteDelete.Methods, AthleteDelete.Handler);

// Events
app.MapMethods(EventGetAll.Template, EventGetAll.Methods, EventGetAll.Handler);
app.MapMethods(EventGetAllActives.Template, EventGetAllActives.Methods, EventGetAllActives.Handler);
app.MapMethods(EventGyById.Template, EventGyById.Methods, EventGyById.Handler);

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

app.UseAuthorization();

app.Run();

