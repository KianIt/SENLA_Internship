using Npgsql;

namespace WebMvcApi.DBConnections {
    public class DBConnectionException : Exception {}
    public class DBCommandExecutionException : Exception { }

    public class PostgreConnection {
        private NpgsqlDataSource dataSource { get; set; }
        public PostgreConnection(string connectionString) {
            try {
                dataSource = NpgsqlDataSource.Create(connectionString);
            }
            catch {
                throw new DBConnectionException();
            }
        }
        public NpgsqlDataReader ExecuteCommand(string commandString) {
            try {
                NpgsqlCommand command = dataSource.CreateCommand(commandString);
                return command.ExecuteReader();
            }
            catch {
                throw new DBCommandExecutionException();
            }
        }
    }
}
