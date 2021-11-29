using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SociologyData.Data
{
    public static class JsonProvider
    {
        public static string FileFilter { get; set; } = "*.json";

        public static string[] Tables { get; set; } = new string[] { "Таблица 1", "Вод", "Пеш" };

        public static string CalculatedSumRow { get; set; } = "Российская Федерация";

        public static int[] ReadFile(string filePath)
        {
            using TextReader tr = new StringReader(Regex.Unescape(File.ReadAllText(filePath)[9..^1].Trim('"')));
            using JsonReader r = new JsonTextReader(tr);
            JsonSerializer s = JsonSerializer.CreateDefault();
            var res = s.Deserialize<GibddJsonScheme>(r);
            if (res != null)
            {
                return new int[] { res.Cards.Length, res.GetDriverPdd(), res.GetPedestrianPdd() };
            }
            else
            {
                throw new JsonSerializationException();
            }
        }

        public static void ReadFolder(string folderPath, Repository res)
        {
            var dirs = Directory.EnumerateDirectories(folderPath);
            foreach (var item in dirs)
            {
                var files = Directory.EnumerateFiles(item, FileFilter);
                if (!files.Any()) continue;
                int year = int.Parse(item[^4..]);
                if (!res.ContainsKey(year))
                {
                    var ds = new DataSet(year.ToString());
                    for (int j = 0; j < Tables.Length; j++)
                    {
                        var dt = new DataTable(Tables[j]);
                        for (int i = 0; i < 2; i++)
                        {
                            dt.Columns.Add();
                        }
                        ds.Tables.Add(dt);
                    }
                    res.Add(year, ds);
                }
                int[] accumulator = new int[Tables.Length];
                foreach (var f in files)
                {
                    var current = ReadFile(f);
                    for (int i = 0; i < Tables.Length; i++)
                    {
                        accumulator[i] += current[i];
                        string reg = string.Join(' ', Path.GetFileNameWithoutExtension(f).Split(' ').Skip(1).SkipLast(1));
                        res[year].Tables[i].Rows.Add(reg, current[i].ToString());
                    }
                }
                int k = 0;
                foreach (DataTable table in res[year].Tables)
                {
                    table.Rows.Add(CalculatedSumRow, accumulator[k++].ToString());
                }
            }
        }
    }
}
