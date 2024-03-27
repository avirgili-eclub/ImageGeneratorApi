using ImageGeneratorApi.Infrastructure.Data.Interfaces;

namespace ImageGeneratorApi.Infrastructure.Data.Services;

public static class IDatabaseServiceExtensions
{
    public static int Exec(this IApplicationDbContext db, string sql)
    {
        using var cmd = db.CreateCommand();
        cmd.CommandText = sql;
        return cmd.ExecuteNonQuery();
    }
}