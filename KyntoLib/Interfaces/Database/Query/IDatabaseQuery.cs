using System;
using System.Collections.Generic;

using KyntoLib.Interfaces.Database.Tables;

namespace KyntoLib.Interfaces.Database.Query
{
    /// <summary>
    /// Represents an active-record style database query.
    /// </summary>
    public interface IDatabaseQuery
    {
        /// <summary>
        /// Converts this query into an insert query.
        /// </summary>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Insert();

        /// <summary>
        /// Converts this query into an insert query.
        /// </summary>
        /// <param name="Table">The row to insert into the database.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Insert(IDatabaseTable Table);

        /// <summary>
        /// Converts this query into a select all query.
        /// </summary>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Select();

        /// <summary>
        /// Converts this query into a select query.
        /// </summary>
        /// <param name="Field">The field name to select.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Select(string Field);

        /// <summary>
        /// Converts this query into a select query.
        /// </summary>
        /// <param name="Field">The field names to select.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Select(string[] Field);

        /// <summary>
        /// Converts this query into an update query.
        /// </summary>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Update();

        /// <summary>
        /// Converts this query into an update query.
        /// </summary>
        /// <param name="Table">The row to update.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Update(IDatabaseTable Table);

        /// <summary>
        /// Converts this query into a delete query.
        /// </summary>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Delete();



        /// <summary>
        /// Sets the where part of this query.
        /// </summary>
        /// <param name="Field">The field name.</param>
        /// <param name="Comparison">The comparison to do.</param>
        /// <param name="Value">The value to compare with.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Where(string Field, DatabaseComparison Comparison, object Value);

        /// <summary>
        /// Sets the where part of this query.
        /// </summary>
        /// <param name="Field">The field names.</param>
        /// <param name="Comparison">The comparisons to do.</param>
        /// <param name="Value">The values to compare with.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Where(string[] Field, DatabaseComparison[] Comparison, object[] Value);

        /// <summary>
        /// Sets the values to be inserted into the database,
        /// </summary>
        /// <param name="Field">The field name.</param>
        /// <param name="Value">The value to insert.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Values(string Field, object Value);

        /// <summary>
        /// Sets the values to be inserted into the database,
        /// </summary>
        /// <param name="Field">The field names.</param>
        /// <param name="Value">The values to insert.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Values(string[] Field, object[] Value);

        /// <summary>
        /// Sets the values to be changed in the update query.
        /// </summary>
        /// <param name="Field">The field name.</param>
        /// <param name="Value">The new value.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Set(string Field, object Value);

        /// <summary>
        /// Sets the values to be changed in the update query.
        /// </summary>
        /// <param name="Field">The field names.</param>
        /// <param name="Value">The new values.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Set(string[] Field, object[] Value);

        /// <summary>
        /// Orders the resulting array by a field in a certain order.
        /// </summary>
        /// <param name="Field">The field to order by.</param>
        /// <param name="Order">The order in which to order by.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery OrderBy(string Field, DatabaseOrder Order);

        /// <summary>
        /// Sets the maximum number of rows to return.
        /// </summary>
        /// <param name="Limit">The maximum number of rows to return.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Limit(int Limit);

        /// <summary>
        /// Sets the maximum number of rows to return.
        /// </summary>
        /// <param name="Offset">The lowwest value.</param>
        /// <param name="Limit">The maximum number of rows to return.</param>
        /// <returns>The updated query.</returns>
        IDatabaseQuery Limit(int Offset, int Limit);



        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <returns>The result of the database query.</returns>
        List<IDatabaseTable> ExecuteRead();

        /// <summary>
        /// Executes the query.
        /// </summary>
        void Execute();
    }
}
