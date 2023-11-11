using Microsoft.EntityFrameworkCore;
using Skins.BLL;
using Skins.BLL.Services;
using Skins.BLL.Services.Interfaces;
using Skins.DAL;
using Skins.DAL.Repositories;
using Skins.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SkinsContext>(options =>
    options.UseSqlite("Data Source=SkinsDatabase.db")
);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IWeaponRepository, WeaponRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IWeaponService, WeaponService>();

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = "localhost";
    options.InstanceName = "local";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
