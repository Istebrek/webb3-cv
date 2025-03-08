
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using SQL_API_Istebrek_Web.Data;
using SQL_API_Istebrek_Web.Models;

namespace SQL_API_Istebrek_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TechnologyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["DefaultConnection"]);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerUI();
                app.UseSwagger();
            }
            app.UseSwaggerUI();
            app.UseSwagger();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapPost("/technology", async (Technology technology, TechnologyDbContext db) =>
            {
                var techToAdd = db.Technologies.Add(technology);
                await db.SaveChangesAsync();
                return Results.Ok(techToAdd.Entity);
            });

            app.MapGet("/technologies", (TechnologyDbContext db) =>
            {
                try
                {
                    var list = db.Technologies.ToList();
                    return Results.Ok(list);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("/technology/{id}", async (string id, TechnologyDbContext db, Technology technology) =>
            {
                try
                {
                    var tech = await db.Technologies.FindAsync(id);
                    if (tech == null) return Results.NotFound("Technology not found.");

                    db.Entry(tech).CurrentValues.SetValues(technology);
                    await db.SaveChangesAsync();
                    return Results.Ok($"{technology.TechName} details were successfully updated");
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("/technology/{id}", async (string id, TechnologyDbContext db) =>
            {
                try
                {
                    var tech = await db.Technologies.FindAsync(id);
                    if (tech == null) return Results.NotFound("Technology not found.");
                    return Results.Ok(tech);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapDelete("/technology/{id}", async (TechnologyDbContext db, string id) =>
            {
                try
                {
                    var tech = await db.Technologies.FindAsync(id);
                    if (tech == null) return Results.NotFound("Technology not found.");
                    db.Technologies.Remove(tech);
                    await db.SaveChangesAsync();
                    return Results.Ok($"{tech.TechName} was successfully removed from database.");
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("/project", async (Project project, TechnologyDbContext db) =>
            {
                var techToAdd = db.Projects.Add(project);
                await db.SaveChangesAsync();
                return Results.Ok(techToAdd.Entity);
            });

            app.MapGet("/projects", (TechnologyDbContext db) =>
            {
                try
                {
                    var list = db.Projects.ToList();
                    return Results.Ok(list);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("/project/{id}", async (string id, TechnologyDbContext db, Project project) =>
            {
                try
                {
                    var tech = await db.Projects.FindAsync(id);
                    if (tech == null) return Results.NotFound("Project not found.");

                    db.Entry(tech).CurrentValues.SetValues(project);
                    await db.SaveChangesAsync();
                    return Results.Ok($"{project.ProjectName} details were successfully updated");
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("/¨project/{id}", async (string id, TechnologyDbContext db) =>
            {
                try
                {
                    var tech = await db.Projects.FindAsync(id);
                    if (tech == null) return Results.NotFound("Project not found.");
                    return Results.Ok(tech);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapDelete("/project/{id}", async (TechnologyDbContext db, string id) =>
            {
                try
                {
                    var tech = await db.Projects.FindAsync(id);
                    if (tech == null) return Results.NotFound("Project not found.");
                    db.Projects.Remove(tech);
                    await db.SaveChangesAsync();
                    return Results.Ok($"{tech.ProjectName} was successfully removed from database.");
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.Run();
        }
    }
}