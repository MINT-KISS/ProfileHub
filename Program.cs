using Amazon;
using Amazon.S3;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ProfileHub.Data;
using ProfileHub.Data.Repositories;
using ProfileHub.Interfaces;
using ProfileHub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
// Configure AWS S3
var awsOptions = builder.Configuration.GetSection("AWS").Get<AwsOptions>();
builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    return new AmazonS3Client(awsOptions.AccessKey, awsOptions.SecretKey, RegionEndpoint.GetBySystemName(awsOptions.Region));
});

builder.Services.AddSingleton<IS3Service>(sp =>
{
    var s3Client = sp.GetRequiredService<IAmazonS3>();
    return new S3Service(s3Client, awsOptions.BucketName);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class AwsOptions
{
    public required string AccessKey { get; set; }
    public required string SecretKey { get; set; }
    public required string Region { get; set; }
    public required string BucketName { get; set; }
}