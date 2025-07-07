using System;
using System.Data;
using System.IO;
using System.Text;

namespace ConvertSuhu.Models
{
  public static class csvExport
  {
    public static void WriteDataTableToCsv(DataTable table,string filePath,params string[] hiddenColumns)
    {
      if (table == null) throw new ArgumentNullException(nameof(table));

      var sb = new StringBuilder();

      foreach (DataColumn col in table.Columns)
      {
        if (Array.Exists(hiddenColumns, h => h == col.ColumnName)) continue;
        sb.Append(col.ColumnName).Append(',');
      }
      sb.Length--;             
      sb.AppendLine();

      foreach (DataRow row in table.Rows)
      {
        foreach (DataColumn col in table.Columns)
        {
          if (Array.Exists(hiddenColumns, h => h == col.ColumnName)) continue;
          string cell = row[col]?.ToString()?.Replace("\"", "\"\"") ?? "";
          sb.Append('"').Append(cell).Append('"').Append(',');
        }
        sb.Length--;            
        sb.AppendLine();
      }

      File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
    }
  }
}
