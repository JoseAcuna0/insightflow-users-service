
using users_service.src.Controllers;
using users_service.src.Interface;
using users_service.src.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserService, UserService>(); 



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
 
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();


app.MapControllers(); 

app.Run();