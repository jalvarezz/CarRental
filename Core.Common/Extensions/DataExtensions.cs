using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Extensions {
    public static class DataExtensions {
        public static IEnumerable<T> ToFullyLoaded<T>(this IQueryable<T> query) {
            return query.ToArray().ToList();
        }
    }
}
