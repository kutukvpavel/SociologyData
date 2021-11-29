using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using System.IO;
using System.Data;
using ClosedXML.Excel;

namespace SociologyData.Data
{
    public static class ExcelProvider
    {
        public static string FileFilter { get; set; } = "*.xls*";

        public static DataSet ReadFile(string filePath)
        {
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            return reader.AsDataSet();
        }

        public static void ReadFolder(string folderPath, Repository res)
        {
            var dirs = Directory.EnumerateDirectories(folderPath);
            foreach (var item in dirs)
            {
                var files = Directory.EnumerateFiles(item, FileFilter);
                var f = files.FirstOrDefault();
                if (f != null)
                {
                    var current = ReadFile(f);
                    current.DataSetName = item[^4..];
                    res.Add(int.Parse(current.DataSetName), current);
                }
            }
        }

        public static void WriteFile(string filePath, DataSet data)
        {
            using var wb = new XLWorkbook();
            foreach (DataTable dt in data.Tables)
            {
                var ws = wb.Worksheets.Add(dt.TableName);
                ws.Cell(1, 1).InsertTable(dt);
            }
            wb.SaveAs(filePath);
        }

        public static void WriteFolder(string folderPath, Repository repo)
        {
            foreach (var item in repo)
            {
                string currentDir = Path.Combine(folderPath, $"XLSed {item.Key}");
                Directory.CreateDirectory(currentDir);
                WriteFile(Path.Combine(currentDir, $"{item.Value.DataSetName}.xlsx"), item.Value);
            }
        }
    }
}
