using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SociologyData.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<DataTable> AsEnumerable(this DataTableCollection c)
        {
            foreach (DataTable item in c)
            {
                yield return item;
            }
        }
    }
}
