using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Infrastructure;

public class Database
{
    public string Server { get; }
    public string InitialCatalog { get; }
    public string ConnectionString { get; }

    public Database(string server, string database)
    {
        Server = server;
        InitialCatalog = database;
        ConnectionString = CreateConnectionString(server, database);
    }

    protected virtual string CreateConnectionString(string server, string database)
        => new SqlConnectionStringBuilder
        {
            InitialCatalog = database,
            Password = "gYX5TPrqpAkb3rsPRFu9",
            UserID = "ServerAdmin",
            DataSource = $"{server}.database.windows.net,1433",
            MultipleActiveResultSets = true,
            Encrypt = true,
            PersistSecurityInfo = true
        }.ToString();

    public async Task<int> ExecuteNonQuery(string sql, CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandText = sql;

        return await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async IAsyncEnumerable<Dictionary<string, object>> Query(string select, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync(cancellationToken);

        using var command = connection.CreateCommand();
        command.CommandText = select;

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var dict = new Dictionary<string, object>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var key = reader.GetName(i);
                var value = reader.GetValue(i);
                if (dict.ContainsKey(key))
                    continue;

                dict.Add(key, value);
            }
            yield return dict;
        }
    }

    public override string ToString()
        => $"[{Server}][{InitialCatalog}]";
}
