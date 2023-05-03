using BusinessLogicLayer.Abstractions.Auth;
using BusinessLogicLayer.Abstractions.Email;
using BusinessLogicLayer.Abstractions.Email.Adapters;
using BusinessLogicLayer.Auth.Jwt;
using BusinessLogicLayer.Email;
using BusinessLogicLayer.Email.Adapters;
using BusinessLogicLayer.Email.Services;
using Core;
using Core.Abstractions.Shared;
using Core.Auth.Jwt;
using Core.Auth.Jwt.Parameters;
using Core.Auth.Jwt.Results;
using Core.Dtos.Email;
using Core.Email;
using Core.Email.Additional;
using Core.Shared;
using DataAccessLayer;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Repositories;
using MailKit.Search;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using PseudoCRMAPI.Extensions;
using System.Text;
using BusinessLogicLayer.Abstractions.Database;
using BusinessLogicLayer.Database.Services;
using Core.Pagination;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
    };
});

builder.Services.AddDbContext<CrmDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CrmConnectionString")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<EmailCredentials>, EmailCredentialsRepository>();
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
builder.Services.AddScoped(typeof(IStringMessageSenderAdapter<>), typeof(MessageSenderAdapter<>));
builder.Services.AddScoped(typeof(IStringMessageReceiverAdapter<,>), typeof(MessageReceiverAdapter<,>));
builder.Services.AddScoped<IEmailService<string, EmailCredentialsDto, ServerInformation>, EmailServiceStringAdapter>();
builder.Services.AddScoped<IEmailService<User, EmailCredentials, ServerInformation>, EmailService>();
builder.Services
    .AddScoped<IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, ServerProtocols>, ImapAndPopMessageService>();
builder.Services
    .AddScoped<IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, SearchQuery>, ImapMessageService>();
builder.Services.AddScoped<IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, int>, PopMessageService>();
builder.Services.AddScoped<IMessageSender<User, string, MimeMessage>, SmtpMessageService>();
builder.Services.AddScoped<IDatabaseService, SqlServerServices>();

builder.Services.AddScoped<IAuthService<JwtAuthLoginParameters, JwtAuthRegistrationParameters, JwtResult, JwtResult>, JwtAuthService>();

builder.Services.AddScoped<IClock, Clock>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.Configure<PaginationConfiguration>(builder.Configuration.GetSection("Pagination"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMigration();
app.UseCors(config =>
{
    config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
