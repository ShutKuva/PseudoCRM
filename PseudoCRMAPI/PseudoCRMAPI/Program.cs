using BusinessLogicLayer.Abstractions.Auth;
using BusinessLogicLayer.Abstractions.Chat;
using BusinessLogicLayer.Abstractions.Email;
using BusinessLogicLayer.Abstractions.Email.Adapters;
using BusinessLogicLayer.Auth.Jwt;
using BusinessLogicLayer.Chat;
using BusinessLogicLayer.Email;
using BusinessLogicLayer.Email.Adapters;
using BusinessLogicLayer.Email.Services;
using Core;
using Core.Abstractions.Shared;
using Core.Auth.Jwt;
using Core.Auth.Jwt.Parameters;
using Core.Auth.Jwt.Results;
using Core.ChatEntities;
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
using PseudoCRMAPI.Hubs;
using System.Text;
using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.Abstractions.Chat.Facades;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Chat.Facades;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();
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
builder.Services.AddHangfire(config =>
{
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnectionString"));
});
builder.Services.AddHangfireServer();

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<EmailCredentials>, EmailCredentialsRepository>();
builder.Services.AddScoped<IRepository<Chat>, ChatRepository>();
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
builder.Services.AddScoped<IUserService<User>, UserService>();
builder.Services.AddScoped<IMessageService<Message>, MessageService>();
builder.Services.AddScoped<IOrganizationService<Organization>, OrganizationService>();
builder.Services.AddScoped<IAuthService<JwtAuthLoginParameters, JwtAuthRegistrationParameters, JwtResult, JwtResult>, JwtAuthService>();
builder.Services.AddScoped<IMessageFacade, MessageFacade>();

builder.Services.AddScoped<IClock, Clock>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtOptions"));

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
    config.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://127.0.0.1:5173");
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();
