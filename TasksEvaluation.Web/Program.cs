using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using TaskEvaluation.InfraStructure.Data;
using TasksEvaluation.Core.Validations;
using TasksEvaluation.Core.Entities.Business;
using TasksEvaluation.Web.CustomServices;
using AutoMapper.Extensions.ExpressionMapping;
using TasksEvaluation.Core.Mapper;
using TasksEvaluation.Core.Interfaces.IServices;
using TaskEvaluation.InfraStructure.Repositories.Services;
using Microsoft.AspNetCore.Identity;
using TaskEvaluation.InfraStructure.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using TasksEvaluation.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Register Fluent Validation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(AssignmentValidator).Assembly);
// Register Auto Mapper 
builder.Services.AddAutoMapper(cfg => { cfg.AddExpressionMapping(); }, typeof(MappingProfile).Assembly);
// Register Context File 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{

	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configer Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
    })
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Rigister Fluent Validation for each entity
builder.Services.AddValidators();
// Register Repository
builder.Services.AddRepositories();
// Register My Services
builder.Services.AddServices();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

DbInitializer.Seed(app);
app.Run();

