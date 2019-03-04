using System.Collections.Generic;
using System.Linq;

namespace EFCore.BulkExtensions
{
    public static class SqlQueryBuilder
    {
        public static string CreateTableCopy(string existingTableName, string newTableName)
        {
#pragma warning disable EF1000 // Possible SQL injection vulnerability.
            var q = $"SELECT TOP 0 Source.* INTO {newTableName} FROM {existingTableName} ";
            q += $"LEFT JOIN {existingTableName} AS Source ON 1 = 0;"; // removes Identity constrain and makes all columns nullable
#pragma warning restore EF1000 // Possible SQL injection vulnerability.
            return q;
        }

        public static string SelectFromTable(string tableName, string orderByColumnName)
        {
#pragma warning disable EF1000 // Possible SQL injection vulnerability.
            return $"SELECT * FROM {tableName} ORDER BY {orderByColumnName};";
#pragma warning restore EF1000 // Possible SQL injection vulnerability.
        }

        public static string DropTable(string tableName)
        {
#pragma warning disable EF1000 // Possible SQL injection vulnerability.
            return $"DROP TABLE {tableName};";
#pragma warning restore EF1000 // Possible SQL injection vulnerability.
        }

        public static string SelectIsIdentity(string tableName, string idColumnName)
        {
#pragma warning disable EF1000 // Possible SQL injection vulnerability.
            return $"SELECT columnproperty(object_id('{tableName}'),'{idColumnName}','IsIdentity');";
#pragma warning restore EF1000 // Possible SQL injection vulnerability.
        }

        public static string MergeTable(TableInfo tableInfo, OperationType operationType)
        {
            string targetTable = tableInfo.FullTableName;
            string sourceTable = tableInfo.FullTempTableName;
            List<string> primaryKeys = tableInfo.PrimaryKeys;
            List<string> columnsNames = tableInfo.PropertyColumnNamesDict.Values.ToList();
            List<string> nonIdentityColumnsNames = columnsNames.Where(a => !primaryKeys.Contains(a)).ToList();
            List<string> insertColumnsNames = tableInfo.HasIdentity ? nonIdentityColumnsNames : columnsNames;

#pragma warning disable EF1000 // Possible SQL injection vulnerability.
            if (tableInfo.BulkConfig.PreserveInsertOrder)
                sourceTable = $"(SELECT TOP {tableInfo.NumberOfEntities} * FROM {sourceTable} ORDER BY {GetCommaSeparatedColumns(primaryKeys)})";

            var q = $"MERGE {targetTable} WITH (HOLDLOCK) AS T " +
                    $"USING {sourceTable} AS S " +
                    $"ON {GetANDSeparatedColumns(primaryKeys, "T", "S")}";

            if (operationType == OperationType.Insert || operationType == OperationType.InsertOrUpdate)
            {
                q += $" WHEN NOT MATCHED THEN INSERT ({GetCommaSeparatedColumns(insertColumnsNames)})";
                q += $" VALUES ({GetCommaSeparatedColumns(insertColumnsNames, "S")})";
            }
            if (operationType == OperationType.Update || operationType == OperationType.InsertOrUpdate)
            {
                q += $" WHEN MATCHED THEN UPDATE SET {GetCommaSeparatedColumns(nonIdentityColumnsNames, "T", "S")}";
            }
            if (operationType == OperationType.Delete)
            {
                q += " WHEN MATCHED THEN DELETE";
            }

            if (tableInfo.BulkConfig.SetOutputIdentity)
            {
                q += $" OUTPUT INSERTED.* INTO {tableInfo.FullTempOutputTableName}";
            }
#pragma warning restore EF1000 // Possible SQL injection vulnerability.

            return q + ";";
        }

        public static string GetCommaSeparatedColumns(List<string> columnsNames, string prefixTable = null, string equalsTable = null)
        {
            string commaSeparatedColumns = "";
            foreach (var columnName in columnsNames)
            {
                commaSeparatedColumns += prefixTable != null ? $"{prefixTable}.[{columnName}]" : $"[{columnName}]";
                commaSeparatedColumns += equalsTable != null ? $" = {equalsTable}.[{columnName}]" : "";
                commaSeparatedColumns += ", ";
            }
            commaSeparatedColumns = commaSeparatedColumns.Remove(commaSeparatedColumns.Length - 2, 2); // removes last excess comma and space: ", "
            return commaSeparatedColumns;
        }

        public static string GetANDSeparatedColumns(List<string> columnsNames, string prefixTable = null, string equalsTable = null)
        {
            string commaSeparatedColumns = GetCommaSeparatedColumns(columnsNames, prefixTable, equalsTable);
            string andSeparatedColumns = commaSeparatedColumns.Replace(",", " AND");
            return andSeparatedColumns;
        }
    }
}
