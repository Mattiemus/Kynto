using System;
using System.Collections.Generic;
using System.Text;

using KyntoLib.Interfaces.Database;
using KyntoLib.Interfaces.Database.Query;
using KyntoLib.Interfaces.Database.Tables;
using KyntoLib.Interfaces.Services;

using MySql.Data.MySqlClient;

namespace KyntoDatabase.MySQL.Query
{
    /// <summary>
    /// Represents an active-record style database query.
    /// </summary>
    public class MySQLDatabaseQuery : IDatabaseQuery
    {
        /// <summary>
        /// Stores the database handler.
        /// </summary>
        private MySQLDatabase _DatabaseHandler;

        /// <summary>
        /// Stores the query type.
        /// </summary>
        private QueryType _QueryType = QueryType.None;

        /// <summary>
        /// Stores the table name.
        /// </summary>
        private string _QueryTable = "";

        /// <summary>
        /// Stores the fields to select.
        /// </summary>
        private string[] _SelectFields;

        /// <summary>
        /// Stores the list of where fields.
        /// </summary>
        private List<string> _WhereFields = new List<string>();

        /// <summary>
        /// Stores the list of where comparisons.
        /// </summary>
        private List<DatabaseComparison> _WhereDatabaseComparisons = new List<DatabaseComparison>();

        /// <summary>
        /// Stores the list of where values.
        /// </summary>
        private List<object> _WhereValues = new List<object>();

        /// <summary>
        /// Stores the list of values fields.
        /// </summary>
        private List<string> _ValuesFields = new List<string>();

        /// <summary>
        /// Stores the list of values values.
        /// </summary>
        private List<object> _ValuesValues = new List<object>();

        /// <summary>
        /// Stores the list of set fields.
        /// </summary>
        private List<string> _SetFields = new List<string>();

        /// <summary>
        /// Stores the list of set values.
        /// </summary>
        private List<object> _SetValues = new List<object>();

        /// <summary>
        /// Stores the field to order by.
        /// </summary>
        private string _OrderByField = "";

        /// <summary>
        /// Stores the order in which to return records.
        /// </summary>
        private DatabaseOrder _OrderOrdering = DatabaseOrder.Ascending;

        /// <summary>
        /// Stores the query offset to limit by.
        /// </summary>
        private int _OrderOffset = 0;

        /// <summary>
        /// Stores the maximum number of results to return.
        /// </summary>
        private int _OrderLimit = 0;

        /// <summary>
        /// Initialises this query.
        /// </summary>
        /// <param name="DatabaseHandler">The database handler parent.</param>
        /// <param name="Table">The table name this query affects.</param>
        public MySQLDatabaseQuery(MySQLDatabase DatabaseHandler, string Table)
        {
            // Store.
            this._DatabaseHandler = DatabaseHandler;
            this._QueryTable = Table;
        }



        /// <summary>
        /// Converts this query into an insert query.
        /// </summary>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Insert()
        {
            // Set query type.
            this._QueryType = QueryType.Insert;

            return this;
        }

        /// <summary>
        /// Converts this query into an insert query.
        /// </summary>
        /// <param name="Table">The row to insert into the database.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Insert(IDatabaseTable Table)
        {
            // Create the insert query.
            return Table.Save();
        }

        /// <summary>
        /// Converts this query into a select all query.
        /// </summary>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Select()
        {
            // Set query type.
            this._QueryType = QueryType.Select;
            this._SelectFields = null;

            return this;
        }

        /// <summary>
        /// Converts this query into a select query.
        /// </summary>
        /// <param name="Field">The field name to select.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Select(string Field)
        {
            // Set query type.
            this._QueryType = QueryType.Select;
            this._SelectFields = new string[] { Field };

            return this;
        }

        /// <summary>
        /// Converts this query into a select query.
        /// </summary>
        /// <param name="Field">The field names to select.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Select(string[] Field)
        {
            // Set query type.
            this._QueryType = QueryType.Select;
            this._SelectFields = Field;

            return this;
        }

        /// <summary>
        /// Converts this query into an update query.
        /// </summary>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Update()
        {
            // Set query type.
            this._QueryType = QueryType.Update;

            return this;
        }

        /// <summary>
        /// Converts this query into an update query.
        /// </summary>
        /// <param name="Table">The row to update.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Update(IDatabaseTable Table)
        {
            // Create the update query.
            return Table.Update();
        }

        /// <summary>
        /// Converts this query into a delete query.
        /// </summary>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Delete()
        {
            // Set query type.
            this._QueryType = QueryType.Delete;

            return this;
        }



        /// <summary>
        /// Sets the where part of this query.
        /// </summary>
        /// <param name="Field">The field name.</param>
        /// <param name="Comparison">The comparison to do.</param>
        /// <param name="Value">The value to compare with.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Where(string Field, DatabaseComparison Comparison, object Value)
        {
            // Update values.
            if (Value.GetType() == typeof(bool))
            {
                Value = (bool)Value ? 1 : 0;
            }

            // Add.
            this._WhereFields.Add(Field);
            this._WhereDatabaseComparisons.Add(Comparison);
            this._WhereValues.Add(Value);

            return this;
        }

        /// <summary>
        /// Sets the where part of this query.
        /// </summary>
        /// <param name="Field">The field names.</param>
        /// <param name="Comparison">The comparisons to do.</param>
        /// <param name="Value">The values to compare with.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Where(string[] Field, DatabaseComparison[] Comparison, object[] Value)
        {
            // Update values.
            for (int i = 0; i < Value.Length; i++)
            {
                if (Value[i].GetType() == typeof(bool))
                {
                    Value[i] = (bool)Value[i] ? 1 : 0;
                }
            }

            // Add.
            this._WhereFields.AddRange(Field);
            this._WhereDatabaseComparisons.AddRange(Comparison);
            this._WhereValues.AddRange(Value);

            return this;
        }


        /// <summary>
        /// Sets the values to be inserted into the database,
        /// </summary>
        /// <param name="Field">The field name.</param>
        /// <param name="Value">The value to insert.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Values(string Field, object Value)
        {
            // Update values.
            if (Value.GetType() == typeof(bool))
            {
                Value = (bool)Value ? 1 : 0;
            }

            // Add.
            this._ValuesFields.Add(Field);
            this._ValuesValues.Add(Value);

            return this;
        }

        /// <summary>
        /// Sets the values to be inserted into the database,
        /// </summary>
        /// <param name="Field">The field names.</param>
        /// <param name="Value">The values to insert.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Values(string[] Field, object[] Value)
        {
            // Update values.
            for (int i = 0; i < Value.Length; i++)
            {
                if (Value[i].GetType() == typeof(bool))
                {
                    Value[i] = (bool)Value[i] ? 1 : 0;
                }
            }

            // Add.
            this._ValuesFields.AddRange(Field);
            this._ValuesValues.AddRange(Value);

            return this;
        }


        /// <summary>
        /// Sets the values to be changed in the update query.
        /// </summary>
        /// <param name="Field">The field name.</param>
        /// <param name="Value">The new value.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Set(string Field, object Value)
        {
            // Update values.
            if (Value.GetType() == typeof(bool))
            {
                Value = (bool)Value ? 1 : 0;
            }

            // Add.
            this._SetFields.Add(Field);
            this._SetValues.Add(Value);

            return this;
        }

        /// <summary>
        /// Sets the values to be changed in the update query.
        /// </summary>
        /// <param name="Field">The field names.</param>
        /// <param name="Value">The new values.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Set(string[] Field, object[] Value)
        {
            // Update values.
            for (int i = 0; i < Value.Length; i++)
            {
                if (Value[i].GetType() == typeof(bool))
                {
                    Value[i] = (bool)Value[i] ? 1 : 0;
                }
            }

            // Add.
            this._SetFields.AddRange(Field);
            this._SetValues.AddRange(Value);

            return this;
        }


        /// <summary>
        /// Orders the resulting array by a field in a certain order.
        /// </summary>
        /// <param name="Field">The field to order by.</param>
        /// <param name="Order">The order in which to order by.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery OrderBy(string Field, DatabaseOrder Order)
        {
            // Set the ordering.
            this._OrderByField = Field;
            this._OrderOrdering = Order;

            return this;
        }

        /// <summary>
        /// Sets the maximum number of rows to return.
        /// </summary>
        /// <param name="Limit">The maximum number of rows to return.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Limit(int Limit)
        {
            // Set the offsets and limits.
            this._OrderLimit = Limit;

            return this;
        }

        /// <summary>
        /// Sets the maximum number of rows to return.
        /// </summary>
        /// <param name="Offset">The lowest value.</param>
        /// <param name="Limit">The maximum number of rows to return.</param>
        /// <returns>The updated query.</returns>
        public IDatabaseQuery Limit(int Offset, int Limit)
        {
            // Set the offsets and limits.
            this._OrderLimit = Limit;
            this._OrderOffset = Offset;

            return this;
        }



        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <returns>The result of the database query.</returns>
        public List<IDatabaseTable> ExecuteRead()
        {
            lock (this._DatabaseHandler.Connection)
            {
                // Create an execute.
                string QueryString = BuildQueryString();

                // Create the query.
                MySqlCommand Command = null;
                MySqlDataReader CommandReader = null;
                try
                {
                    Command = this._DatabaseHandler.Connection.CreateCommand();
                    Command.CommandText = QueryString;
                    CommandReader = Command.ExecuteReader();

                    // Get the returned rows.
                    List<IDatabaseTable> DatabaseTables = new List<IDatabaseTable>();
                    if (CommandReader.HasRows)
                    {
                        List<Dictionary<string, object>> RetrivedValues = new List<Dictionary<string, object>>();

                        while (CommandReader.Read())
                        {
                            Dictionary<string, object> Record = new Dictionary<string, object>();

                            for (int i = 0; i < CommandReader.FieldCount; i++)
                            {
                                Record.Add(CommandReader.GetName(i), CommandReader[i]);
                            }

                            RetrivedValues.Add(Record);
                        }

                        for (int i = 0; i < RetrivedValues.Count; i++)
                        {
                            IDatabaseTable Table = this._DatabaseHandler.CreateBlankTable(this._QueryTable);
                            Table.FromDictionary(RetrivedValues[i]);

                            DatabaseTables.Add(Table);
                        }
                    }
                    return DatabaseTables;
                }
                catch (Exception Ex)
                {
                    this._DatabaseHandler.ServerManager.LoggingService.WriteDatabase(LogImportance.Error, "Error during ExecuteRead.", Ex);
                }
                finally
                {
                    if (CommandReader != null)
                    {
                        CommandReader.Close();
                        CommandReader.Dispose();
                    }
                    if (Command != null)
                    {
                        Command.Dispose();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        public void Execute()
        {
            lock (this._DatabaseHandler.Connection)
            {
                // Create an execute.
                string QueryString = BuildQueryString();

                // Create the query.
                MySqlCommand Command = null;
                MySqlDataReader CommandReader = null;
                try
                {
                    Command = this._DatabaseHandler.Connection.CreateCommand();
                    Command.CommandText = QueryString;
                    CommandReader = Command.ExecuteReader();
                }
                catch (Exception Ex)
                {
                    this._DatabaseHandler.ServerManager.LoggingService.WriteDatabase(LogImportance.Error, "Error during Execute.", Ex);
                }
                finally
                {
                    if (CommandReader != null)
                    {
                        CommandReader.Close();
                        CommandReader.Dispose();
                    }
                    if (Command != null)
                    {
                        Command.Dispose();
                    }
                }

                return;
            }
        }

        /// <summary>
        /// Builds the query representing this active record query generator.
        /// </summary>
        /// <returns>The string representing this query.</returns>
        public string BuildQueryString()
        {
            // Create the query string.
            StringBuilder Builder = new StringBuilder();

            // Create the query string.
            switch (this._QueryType)
            {
                case QueryType.Insert:
                    {
                        // Build query base.
                        Builder.Append("INSERT INTO `" + this._QueryTable + "`");
                        Builder.Append("(");

                        for (int i = 0; i < this._ValuesFields.Count; i++)
                        {
                            if (i == this._ValuesFields.Count - 1)
                            {
                                Builder.Append("`" + this._ValuesFields[i] + "`");
                            }
                            else
                            {
                                Builder.Append("`" + this._ValuesFields[i] + "`, ");
                            }
                        }

                        Builder.Append(") VALUES (");

                        for (int i = 0; i < this._ValuesValues.Count; i++)
                        {
                            if (i == this._ValuesValues.Count - 1)
                            {
                                Builder.Append("'" + this._ValuesValues[i] + "'");
                            }
                            else
                            {
                                Builder.Append("'" + this._ValuesValues[i] + "', ");
                            }
                        }

                        Builder.Append(") ");

                        break;
                    }

                case QueryType.Delete:
                    {
                        // Build query base.
                        Builder.Append("DELETE FROM `" + this._QueryTable + "` ");
                        Builder.Append(BuildQueryOptionsString());

                        break;
                    }

                case QueryType.Update:
                    {
                        // Build query base.
                        Builder.Append("UPDATE `" + this._QueryTable + "` SET ");

                        for (int i = 0; i < this._SetFields.Count; i++)
                        {
                            if (i == this._SetFields.Count - 1)
                            {
                                Builder.Append("`" + this._SetFields[i] + "` = '" + this._SetValues[i] + "' ");
                            }
                            else
                            {
                                Builder.Append("`" + this._SetFields[i] + "` = '" + this._SetValues[i] + "', ");
                            }
                        }

                        Builder.Append(BuildQueryOptionsString());

                        break;
                    }

                case QueryType.Select:
                    {
                        // Build query base.
                        Builder.Append("SELECT ");

                        if (this._SelectFields != null)
                        {
                            for (int i = 0; i < this._SelectFields.Length; i++)
                            {
                                if (i == this._SelectFields.Length - 1)
                                {
                                    Builder.Append("`" + this._SelectFields[i] + "` ");
                                }
                                else
                                {
                                    Builder.Append("`" + this._SelectFields[i] + "`, ");
                                }
                            }
                        }
                        else
                        {
                            Builder.Append("* ");
                        }
                        Builder.Append("FROM `" + this._QueryTable + "` ");

                        Builder.Append(BuildQueryOptionsString());

                        break;
                    }

                default:
                    {
                        throw new Exception("A query type is required to build the database query string.");
                    }
            }

            // Generate query.
            return Builder.ToString();
        }

        /// <summary>
        /// Builds the query options string.
        /// </summary>
        /// <returns>The query options string.</returns>
        public string BuildQueryOptionsString()
        {
            // Build the options string.
            StringBuilder Builder = new StringBuilder();

            // Where
            if (this._WhereFields.Count != 0)
            {
                Builder.Append("WHERE ");
                for (int i = 0; i < this._WhereFields.Count; i++)
                {
                    switch (this._WhereDatabaseComparisons[i])
                    {
                        case DatabaseComparison.Equals:
                            {
                                Builder.Append("`" + this._WhereFields[i] + "` = '" + this._WhereValues[i] + "' ");

                                break;
                            }

                        case DatabaseComparison.NotEquals:
                            {
                                Builder.Append("`" + this._WhereFields[i] + "` != '" + this._WhereValues[i] + "' ");

                                break;
                            }

                        case DatabaseComparison.EqualOrLessThan:
                            {
                                Builder.Append("`" + this._WhereFields[i] + "` <= '" + this._WhereValues[i] + "' ");

                                break;
                            }

                        case DatabaseComparison.Like:
                            {
                                Builder.Append("`" + this._WhereFields[i] + "` LIKE '%" + this._WhereValues[i] + "%' ");

                                break;
                            }
                    }

                    if (i != this._WhereFields.Count - 1)
                    {
                        Builder.Append("AND ");
                    }
                }
            }

            // Ordering.
            if (!String.IsNullOrEmpty(this._OrderByField))
            {
                Builder.Append("ORDER BY `" + this._OrderByField + "` ");
                switch (this._OrderOrdering)
                {
                    case DatabaseOrder.Ascending:
                            Builder.Append("ASC ");
                            break;

                    case DatabaseOrder.Decending:
                            Builder.Append("DESC ");
                            break;
                }
            }

            // Limits.
            if (this._OrderLimit > 0)
            {
                if (this._QueryType != QueryType.Delete && this._QueryType != QueryType.Update)
                {
                    Builder.Append("LIMIT " + this._OrderOffset + ", " + this._OrderLimit);
                }
                else
                {
                    Builder.Append("LIMIT " + this._OrderLimit);
                }
            }

            return Builder.ToString();
        }

        /// <summary>
        /// Represents the different possible query types.
        /// </summary>
        private enum QueryType
        {
            None,
            Select,
            Update,
            Delete,
            Insert
        }
    }
}
