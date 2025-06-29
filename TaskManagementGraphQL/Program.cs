using System.Text;
using Application.Commands.Auth;
using Application.Commands.Tasks;
using Application.Interfaces;
using Application.Mappers;
using Application.Queries.Tasks;
using Application.Queries.Users;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Authentication;
using Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presentation.GraphQL.Mutations;
using Presentation.GraphQL.Queries;
using SharedKernel.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AppDbContext>();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<LoginUserCommand>();
builder.Services.AddScoped<RegisterUserCommand>();
builder.Services.AddScoped<AssignTaskToUserCommand>();
builder.Services.AddScoped<CreateTaskCommand>();
builder.Services.AddScoped<DeleteTaskCommand>();
builder.Services.AddScoped<UpdateTaskCommand>();
builder.Services.AddScoped<GetTaskByIdQuery>();
builder.Services.AddScoped<GetTasksQuery>();
builder.Services.AddScoped<GetUsersQuery>();

builder.Services.AddScoped<TaskMapper>();
builder.Services.AddScoped<RoleHelper>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = 
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddGraphQLServer()
    .AddAuthorization()
    .RegisterDbContextFactory<AppDbContext>()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.MigrateAsync();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
   endpoints.MapGraphQL();
});

app.Run();

