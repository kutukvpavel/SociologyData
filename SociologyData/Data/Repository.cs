using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SociologyData.Extensions;
using MoreLinq;

namespace SociologyData.Data
{
    public class Repository : SortedDictionary<int, DataSet>
    {
        public static char[] TrimRowHeaders { get; set; } = new char[] { ' ', '*' };

        public Repository() : base()
        {

        }

        public string[] GetTables()
        {
            return Values.Select(x => x.Tables.AsEnumerable().Select(x => x.TableName)).Flatten().Cast<string>()
                .Distinct().Except(AliasProvider.TableAliasGroups.Flatten().Cast<string>()).OrderBy(x => x)
                .Insert(AliasProvider.TableAliasGroups.Select(x => x[0]), 0).ToArray();
        }

        public Tuple<double[], double[]> GetTimePlot(IEnumerable<string> sheetAliases, IEnumerable<string> rowAliases, int columnIndex)
        {
            List<double> xs = new(Count);
            List<double> ys = new(Count);
            foreach (var item in this)
            {
                DataTable t = null;
                foreach (var sheetName in sheetAliases)
                {
                    t = item.Value.Tables[sheetName];
                    if (t != null)
                    {
                        DataRow r = null;
                        foreach (var rowHeader in rowAliases)
                        {
                            r = t.AsEnumerable().FirstOrDefault(x => x[0].ToString().Trim(TrimRowHeaders) == rowHeader);
                            if (r != null)
                            {
                                xs.Add(item.Key);
                                ys.Add(double.Parse(r[columnIndex].ToString()));
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return new Tuple<double[], double[]>(xs.ToArray(), ys.ToArray());
        }
    }
}
