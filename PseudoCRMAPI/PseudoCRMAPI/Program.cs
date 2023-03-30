using BusinessLogicLayer.Abstractions.Auth;
using BusinessLogicLayer.Abstractions.Email;
using BusinessLogicLayer.Abstractions.Email.Adapters;
using BusinessLogicLayer.Auth.Jwt;
using BusinessLogicLayer.Email;
using BusinessLogicLayer.Email.Adapters;
using BusinessLogicLayer.Email.Services;
using Core;
using Core.Auth.Jwt;
using Core.Auth.Jwt.Parameters;
using Core.Auth.Jwt.Results;
using Core.Email;
using Core.Email.Additional;
using DataAccessLayer;
using DataAccessLayer.Abstractions;
using MailKit.Search;
using MimeKit;
using PseudoCRMAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CrmDbContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
builder.Services.AddScoped(typeof(IStringMessageSenderAdapter<>), typeof(MessageSenderAdapter<>));
builder.Services.AddScoped(typeof(IStringMessageReceiverAdapter<,>), typeof(MessageReceiverAdapter<,>));
builder.Services.AddScoped<IEmailService<string, EmailCredentials>, EmailServiceStringAdapter>();
builder.Services.AddScoped<IEmailService<User, EmailCredentials>, EmailService>();
builder.Services
    .AddScoped<IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, ServerProtocols>, ImapAndPopMessageService>();
builder.Services
    .AddScoped<IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, SearchQuery>, ImapMessageService>();
builder.Services.AddScoped<IMessageReceiver<IReadOnlyList<MimeMessage>, User, string, int>, PopMessageService>();
builder.Services.AddScoped<IMessageSender<User, string, MimeMessage>, SmtpMessageService>();

builder.Services.AddScoped<IAuthService<JwtAuthLoginParameters, JwtAuthRegistrationParameters, JwtResult, JwtResult>, JwtAuthService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
