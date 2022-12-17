using System;
using Microsoft.EntityFrameworkCore;
using RavelliCompete.Domain.Athletes;
using RavelliCompete.Infra.Data;

namespace RavelliCompete.Endpoints.Atletas.Post;

public class AthletePost
{
    public static string Template => "/atletas";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    public static async Task<IResult> Action(AthletePostRequest athleteRequest, ApplicationDbContext context)
    {
        if (ValidateIfUserExists(athleteRequest, context).Result)
            return Results.Ok(athleteRequest);
        
        var newAthlete = new Athlete(athleteRequest);

        await context.Athlete.AddAsync(newAthlete);
        await context.SaveChangesAsync();

        return Results.Created($"/atletas/{newAthlete.Id}", newAthlete.Id);
    }

    private static async Task<bool> ValidateIfUserExists(AthletePostRequest athleteRequest, ApplicationDbContext context)
    {
        var athlete = await context.Athlete.FirstOrDefaultAsync(a => a.Cpf == athleteRequest.Cpf ||
                                                                     a.Rg == athleteRequest.Rg ||
                                                                     a.Email == athleteRequest.Email);

        return athlete == null ? false : true;
    }
}

