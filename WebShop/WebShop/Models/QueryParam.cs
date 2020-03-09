using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class QueryParam
    {
        public QueryParam(string propertyName, QueryParamType queryParamType)
        {
            PropertyName = propertyName;
            QueryParamType = queryParamType;
        }

        public string PropertyName { get; set; }
        public QueryParamType QueryParamType { get; set; }
    }

    public enum QueryParamType
    {
        Int,
        String,
        DateTime,
        Boolean,
        Double
    }
}
