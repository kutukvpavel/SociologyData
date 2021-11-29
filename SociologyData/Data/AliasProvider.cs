using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociologyData.Data
{
    public static class AliasProvider
    {
        public static string[][] TableAliasGroups = new string[][]
        {
            new string[] { "Водитель (ПДД)", "Вод", "Водит" },
            new string[] { "Пешеход (ПДД)", "Пеш", "Пешеход" },
            new string[] { "Все", "Таблица 1", "ДТП" }
        };

        public static string[][] RegionAliasGroups = new string[][]
        {
            new string[] { "г. Санкт-Петербург", "г. С.-Петербург" },
            new string[] { "г. Москва", "Москва", "гор. Москва" }
        };

        public static string[] GetAlias(string[][] group, string name)
        {
            try
            {
                return group.First(x => x.Contains(name)).ToArray();
            }
            catch (InvalidOperationException)
            {
                return new string[] { name };
            }
        }
    }
}
