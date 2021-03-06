using Simple.Data.Ado.Schema;

namespace Simple.Data.Ado
{
	internal class AggregationHelper
	{
		private readonly DatabaseSchema _schema;
		private readonly ICommandBuilder _commandBuilder;
		private readonly IExpressionFormatter _expressionFormatter;

        public AggregationHelper(DatabaseSchema schema)
        {
            _schema = schema;
            _commandBuilder = new CommandBuilder(schema.SchemaProvider);
            _expressionFormatter = new ExpressionFormatter(_commandBuilder, _schema);
        }

		public ICommandBuilder GetMaxCommand(string tableName, string columnName, SimpleExpression criteria)
		{
			_commandBuilder.Append(GetMaxClause(tableName, columnName));

			if (criteria != null)
			{
				_commandBuilder.Append(" where ");
				_commandBuilder.Append(_expressionFormatter.Format(criteria));
			}

			return _commandBuilder;
		}

		private string GetMaxClause(string tableName, string columnName)
		{
			return string.Format("select max({0}.{1}) from {0}", _schema.FindTable(tableName).QualifiedName, columnName);
		}

		public ICommandBuilder GetMinCommand(string tableName, string columnName, SimpleExpression criteria)
		{
			_commandBuilder.Append(GetMinClause(tableName, columnName));

			if (criteria != null)
			{
				_commandBuilder.Append(" where ");
				_commandBuilder.Append(_expressionFormatter.Format(criteria));
			}

			return _commandBuilder;
		}

		private string GetMinClause(string tableName, string columnName)
		{
			return string.Format("select min({0}.{1}) from {0}", _schema.FindTable(tableName).QualifiedName, columnName);
		}
	}
}