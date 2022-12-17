using Microsoft.EntityFrameworkCore;
using RavelliCompete.Endpoints.Atletas.Delete;
using RavelliCompete.Endpoints.Atletas.Get;
using RavelliCompete.Endpoints.Atletas.GetAll;
using RavelliCompete.Endpoints.Atletas.Post;
using RavelliCompete.Endpoints.Events.Get;
using RavelliCompete.Infra.Data;
using RavelliCompete.Services.Athletes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionStrings:MySql"];

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

app.MapMethods(AthletePost.Template, AthletePost.Methods, AthletePost.Handler);

app.MapMethods(AthleteDelete.Template, AthleteDelete.Methods, AthleteDelete.Handler);

// Events
app.MapMethods(EventGetAll.Template, EventGetAll.Methods, EventGetAll.Handler);
app.MapMethods(EventGetAllActives.Template, EventGetAllActives.Methods, EventGetAllActives.Handler);
app.MapMethods(EventGyById.Template, EventGyById.Methods, EventGyById.Handler);

app.UseAuthorization();

app.MapControllers();

app.Run();

