using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
namespace APIFiMag
{
    /// <summary>
    /// Classe utilitaire permettant la création d'une table dans une base de données SQL calqué sur le modèle d'une
    /// DataTable d'un DataSet
    /// </summary>
    class SqlTableCreator
    {

        #region Instance Variables

        private SqlConnection _connection;

        public SqlConnection Connection {
            get { return _connection; }
            set { _connection = value; }
        }

        private SqlTransaction _transaction;

        public SqlTransaction Transaction {
            get { return _transaction; }
            set { _transaction = value; }
        }

        private string _tableName;

        public string DestinationTableName {
            get { return _tableName; }
            set { _tableName = value; }
        }
        #endregion

        #region Constructor

        public SqlTableCreator(SqlConnection connection, SqlTransaction transaction) {
            _connection = connection;
            _transaction = transaction;
        }

        #endregion

        #region Instance Methods

        public object CreateFromDataTable() {

            string sql = "CREATE TABLE [Historique] (";
            sql += "[DATE] DATETIME, [SYMBOL] VARCHAR(255), [COLUMN] VARCHAR(255), [VALUE] REAL, PRIMARY KEY ([DATE], [SYMBOL], [COLUMN]))";
            SqlCommand cmd;

            if (_transaction != null && _transaction.Connection != null)
                cmd = new SqlCommand(sql, _connection, _transaction);
            else
                cmd = new SqlCommand(sql, _connection);

            return cmd.ExecuteNonQuery();
        }
        #endregion

    }
}
