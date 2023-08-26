using Dapper;
using FreeCourse.Services.Discount.Services;
using FreeCourse.SharedCore7.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Npgsql;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

// Add authentication
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_discount";
    options.RequireHttpsMetadata = false;
});

// Add services to the container.

AuthorizationPolicy requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Seed table
using(var scope = app.Services.CreateScope())
{
    IDbConnection connection = new NpgsqlConnection(builder.Configuration.GetConnectionString("PostgreSql"));
    var isTableExist = connection.QuerySingle<bool>("select exists(select from pg_tables where schemaname = 'public' and tablename = 'discount')");
    if (!isTableExist)
    {
        var createTable = "create table Discount(Id serial primary key, UserId varchar(200) not null, Rate smallint not null, Code varchar(50) not null, CreateDate timestamp not null default CURRENT_TIMESTAMP)";
        connection.Query(createTable);
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
