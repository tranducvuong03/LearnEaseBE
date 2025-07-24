using LearnEase.Repository;

using LearnEase.Repository.EntityModel;
using LearnEase.Repository.IRepo;
using LearnEase.Repository.Repositories;
using LearnEase.Service;
using LearnEase.Service.IServices;
using LearnEase.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "google-key.json");

var googleKeyPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
Console.WriteLine($"🔐 GOOGLE_APPLICATION_CREDENTIALS = {googleKeyPath}");
if (!File.Exists("google-key.json"))
{
    Console.WriteLine("❌ File google-key.json không tồn tại trong thư mục làm việc.");
}
else
{
    Console.WriteLine("✅ File google-key.json đã tồn tại.");
}
var path = Path.GetFullPath("google-key.json");
Console.WriteLine($"📌 Đường dẫn tuyệt đối: {path}");
var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<IAchievementService, AchievementService>();
builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
builder.Services.AddScoped<ISpeakingAttemptService, SpeakingAttemptService>();
builder.Services.AddScoped<IDialectService, DialectService>();
builder.Services.AddScoped<ISpeakingExerciseService, SpeakingExerciseService>();
builder.Services.AddScoped<IUserSettingsService, UserSettingsService>();
builder.Services.AddScoped<IUserProgressService, UserProgressService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<IVocabularyItemService, VocabularyItemService>();
builder.Services.AddScoped<ILeaderboardRepository, LeaderboardRepository>();
builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAiLessonService, AiLessonService>();
builder.Services.AddScoped<ILearningService, LearningService>();
builder.Services.AddScoped<IUserStreakService, UserStreakService>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<IOpenAIService, OpenAIService>();
builder.Services.AddHttpClient<IOpenAIService, OpenAIService>();
builder.Services.AddScoped<ILearningService, LearningService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IUserHeartService, UserHeartService>();

//payment
builder.Services.AddScoped<IPayOSService, PayOSService>();

//---------------------send mail------------------------
builder.Services.AddScoped<IEmailService, EmailService>();
// Add services to the container.
builder.Services.AddSqlServer<LearnEaseContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "LearnEase API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Nhập token theo định dạng: Bearer {token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var config = builder.Configuration;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]))
    };
});
//------cho phép chạy api local----------------
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowLocalhost",
		policy => policy
			.WithOrigins(
			"http://127.0.0.1:5500",
			"https://learn-ease-admin-z913.vercel.app")
			.AllowAnyHeader()
			.AllowAnyMethod());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
