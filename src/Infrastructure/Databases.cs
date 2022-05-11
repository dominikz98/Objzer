using Microsoft.Data.SqlClient;

namespace Infrastructure.Core
{
    public static class Databases
    {
        public static IAsyncEnumerable<Database> All => new DatabaseEnumerable();
    }

    internal class DatabaseEnumerable : IAsyncEnumerable<Database>
    {
        public IAsyncEnumerator<Database> GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return new DatabaseEnumerator();
        }

        private class DatabaseEnumerator : IAsyncEnumerator<Database>
        {
            public const string CONNECTION_STRING = "";
            private const string _databaseQuery = "select distinct [server], [database] from databases order by 1, 2";

            private SqlConnection? _connection;
            private SqlCommand? _command;
            private SqlDataReader? _reader;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            public Database Current { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

            public async ValueTask<bool> MoveNextAsync()
            {
                if (_reader == null)
                {
                    _connection = new SqlConnection(CONNECTION_STRING);
                    await _connection.OpenAsync();
                    _command = _connection.CreateCommand();
                    _command.CommandText = _databaseQuery;
                    _reader = await _command.ExecuteReaderAsync();
                }

                var result = await _reader.ReadAsync();
                if (!result)
                    return result;

                Current = new Database(_reader.GetString(0), _reader.GetString(1));
                return result;
            }

            public async ValueTask DisposeAsync()
            {
                try
                {
                    if (_reader != null)
                        await _reader.DisposeAsync();

                    if (_command != null)
                        await _command.DisposeAsync();

                    if (_connection != null)
                        await _connection.DisposeAsync();
                }
                catch { }
                finally
                {
                    _reader = null;
                    _command = null;
                    _connection = null;
                }
            }
        }
    }
}
