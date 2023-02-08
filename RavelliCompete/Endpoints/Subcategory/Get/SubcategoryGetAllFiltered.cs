using RavelliCompete.Infra.Data;

namespace RavelliCompete.Endpoints.Subcategory.Get;

public class SubcategoryGetAllFiltered
{
    public static string Template => "/subcategories/{idEvento:int}/{category:int}/{age:int}/{gender:int}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(int idEvento, int category, int age, int gender, ApplicationDbContext context) {
        var response = context.Subcategoria.Where(s => s.IdEvento == idEvento
                                                       && s.Categoria == category
                                                       && s.IdadeAte >= age
                                                       && s.IdadeDe <= age
                                                       && s.FiltroSexo == gender
                                                       && s.Ativo == true).ToList();

        if (response == null)
            return Results.NotFound();

        return Results.Ok(response);
    }
}

