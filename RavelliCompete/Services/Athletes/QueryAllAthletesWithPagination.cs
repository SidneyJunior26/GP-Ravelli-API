using System;
using Dapper;
using MySqlConnector;
using RavelliCompete.Domain.Athletes;

namespace RavelliCompete.Services.Athletes;

public class QueryAllAthletesWithPagination
{
    private readonly IConfiguration _configuration;

    public QueryAllAthletesWithPagination(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<Athlete>> Execute(int page, int rows)
    {
        var db = new MySqlConnection(_configuration["ConnectionStrings:MySql"]);
        var query = $@"SELECT *
                FROM atleta
                ORDER BY Nome
                LIMIT @rows OFFSET @page";

        return await db.QueryAsync<Athlete>(
            query,
            new { rows, page }
            );
    }
}

