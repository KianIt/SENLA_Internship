using System.Linq.Expressions;

namespace WebMvcApi.CustomFramework {
    public class CustomSQLParseVisitor : ExpressionVisitor {
        private List<string> Conditions { get; init; } = new List<string>();
        private string TableName { get; init; }
        private string Query { get; set; } = string.Empty;
        public CustomSQLParseVisitor(string tableName) {
            TableName = tableName;
        }
        public string GetQuery(Expression? node) {
            if (Query.Length == 0) {
                Visit(node);
            }

            string? query = Query;

            if (Conditions.Count > 0) {
                query += " WHERE(";

                foreach (string? condition in Conditions)
                    query += $" {condition}";

                query += ")";
            }

            return query;
        }
        public override Expression? Visit(Expression? node) => 
            node switch {
                MethodCallExpression methodCallExpression => methodCallExpression.Method.Name switch {
                    "Select" => parseSelect(methodCallExpression),
                    "Where" => parseWhere(methodCallExpression),
                    "Insert" => parseInsert(methodCallExpression),
                    "Delete" => parseDelete(methodCallExpression),
                    "Update" => parseUpdate(methodCallExpression),
                    _ => base.Visit(methodCallExpression)
                },
                _ => base.Visit(node)
            };
        private Expression parseSelect(MethodCallExpression methodCallExpression) {
            Query += "SELECT";

            string? fields = ExtractArg(methodCallExpression.Arguments[0]);

            if (fields != null)
                Query += $" {fields}";
            else
                Query += $" *";

            Query += $" FROM \"{TableName}\"";

            return base.Visit(methodCallExpression);
        }
        private Expression parseWhere(MethodCallExpression methodCallExpression) {
            string? condition = ExtractArg(methodCallExpression.Arguments[0]);

            if (condition != null)
                Conditions.Add(condition);

            return base.Visit(methodCallExpression);
        }
        private Expression parseInsert(MethodCallExpression methodCallExpression) {
            Query += $"INSERT INTO \"{TableName}\"";

            string? fields = ExtractArg(methodCallExpression.Arguments[0]);
            string? values = ExtractArg(methodCallExpression.Arguments[1]);

            Query += $"({fields!}) VALUES({values!})";

            return base.Visit(methodCallExpression);
        }
        private Expression parseDelete(MethodCallExpression methodCallExpression) {
            Query += $"DELETE FROM \"{TableName}\"";

            string? condition = ExtractArg(methodCallExpression.Arguments[0]);

            Query += $" WHERE({condition!})";

            return base.Visit(methodCallExpression);
        }
        private Expression parseUpdate(MethodCallExpression methodCallExpression) {
            Query += $"UPDATE \"{TableName}\"";

            string? changes = ExtractArg(methodCallExpression.Arguments[0]);
            string ? condition = ExtractArg(methodCallExpression.Arguments[1]);

            Query += $" SET {changes}";
            Query += $" WHERE({condition})";

            return base.Visit(methodCallExpression);
        }
        private string? ExtractArg(Expression argument) {
            if (argument is MethodCallExpression) {
                return Expression.Lambda(argument).Compile().DynamicInvoke()?.ToString();
            }
            else {
                return ((ConstantExpression) argument).Value?.ToString();
            }
        }
    }
}
