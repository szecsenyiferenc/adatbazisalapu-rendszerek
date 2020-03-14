using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class QueryParam
    {
        public QueryParam(string propertyName, Type queryParamType)
        {
            PropertyName = propertyName;
            QueryParamType = DetermineParam(queryParamType);
        }

        public string PropertyName { get; set; }
        public QueryParamType QueryParamType { get; set; }

        private QueryParamType DetermineParam(Type type)
        {
            if(type == typeof(int))
            {
                return QueryParamType.Int;
            }
            if (type == typeof(bool))
            {
                return QueryParamType.Boolean;
            }
            if (type == typeof(DateTime))
            {
                return QueryParamType.DateTime;
            }
            if (type == typeof(double))
            {
                return QueryParamType.Double;
            }
            if (type == typeof(decimal))
            {
                return QueryParamType.Decimal;
            }
            else
            {
                return QueryParamType.String;
            }

        }
    }

    public enum QueryParamType
    {
        Int,
        String,
        DateTime,
        Boolean,
        Double,
        Decimal
    }
}
