﻿using System;
using System.ComponentModel.Composition;
using System.Data.Common;
using System.Data.SqlClient;
using Simple.Data.Ado;
using Simple.Data.Ado.Schema;

namespace Simple.Data.SqlServer
{
    [Export("sql", typeof(IConnectionProvider))]
    public class SqlConnectionProvider : IConnectionProvider
    {
        private string _connectionString;

        public SqlConnectionProvider()
        {
            
        }

        public SqlConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public ISchemaProvider GetSchemaProvider()
        {
            return new SqlSchemaProvider(this);
        }

        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        public string GetIdentityFunction()
        {
            return "SCOPE_IDENTITY()";
        }

        public bool SupportsCompoundStatements
        {
            get { return true; }
        }

        public bool SupportsStoredProcedures
        {
            get { return true; }
        }

        public IProcedureExecutor GetProcedureExecutor(AdoAdapter adapter, ObjectName procedureName)
        {
            return new ProcedureExecutor(adapter, procedureName);
        }
    }
}
