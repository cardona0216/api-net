using CustomersApi.CasosDeUsos;
using CustomersApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);

builder.Services.AddDbContext<CustomerDatabaseContext>(mysqlBuilder =>
{
    //Conxion a la base de datos
    //builder.UseMySQL("Server=localhost;Port=3306;Database=net_system;Uid=root;pwd=''"); //esta es muy mala practica
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("conexionDb"));//esta es la conexion a la base de datos
});

builder.Services.AddScoped<IUpdateCustomerUseCase,UpdateCustomerUseCase>();
builder.Services.AddCors(options =>{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();
app.UseCors("NuevaPolitica");
app.UseAuthorization();

app.MapControllers();


app.Run();
