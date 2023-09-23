using System.Linq.Expressions;
using Npgsql;

using WebMvcApi.Models;
using WebMvcApi.DBConnections;
using WebMvcApi.CustomFramework;

namespace WebMvcApi.Repositories {
    public class UserVisitorRepository : AbstractRepository<User> {
        private readonly PostgreConnection _postgresConnection;
        private readonly CustomSQLParseVisitor _commandParseVisitor;
        private CustomDBEnumerable Users { get; init; } = new CustomDBEnumerable();
        public UserVisitorRepository(PostgreConnection postgresConnection, 
                                    CustomSQLParseVisitor commandParseVisitor) {
            _postgresConnection = postgresConnection;
            _commandParseVisitor = commandParseVisitor;
        }
        public override IEnumerable<User> GetItems() {
            Expression expression = () => Users.Select("\"Id\", \"Name\"");
            NpgsqlDataReader result = HandleExpression(expression);

            List<User> users = new List<User>();
            while (result.Read()) {
                int id = result.GetInt32(0);
                string name = result.GetString(1);
                users.Add(new User { Id = id, Name = name });
            }
            return users;
        }
        public override User? GetItem(int id) {
            Expression expression = () => Users!.Select("\"Name\"").Where($"\"Id\" = '{id}'");
            NpgsqlDataReader result = HandleExpression(expression);

            if (result.Read()) {
                string name = result.GetString(0);
                return new User { Id = id, Name = name };
            }
            else 
                return null;
        }

        public override void Add(User item) {
            Expression expression = () => Users.Insert("\"Id\", \"Name\"", $"'{item.Id}', '{item.Name}'");
            HandleExpression(expression);
        }
        public override void Delete(int id) {
            Expression expression = () => Users.Delete($"\"Id\" = '{id}'");
            HandleExpression(expression);
        }
        public override void Update(User item) {
            Expression expression = () => Users.Update($"\"Name\" = '{item.Name}'", $"\"Id\" = '{item.Id}'");
            HandleExpression(expression);
        }
        public override void Save() {
        }

        private NpgsqlDataReader HandleExpression(Expression expression) {
            string command = _commandParseVisitor.GetQuery(expression);
            NpgsqlDataReader result = _postgresConnection.ExecuteCommand(command);
            return result;
        }
    }
}
