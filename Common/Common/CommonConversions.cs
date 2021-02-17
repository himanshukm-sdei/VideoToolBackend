using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Common.Common
{
    public class CommonConversions
    {
        /// <summary>
        /// To read Enum description
        /// <createdby>Manoj jaswal</createdby>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static DateTime TimezoneConversion(DateTime sessionTime , string sourceTimeZone , string destinationTimeZone)
        {
            var localtime = sessionTime;
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZone);
            var dataTimeByZoneId = TimeZoneInfo.ConvertTime(localtime, TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZone), timeZoneInfo);

            return dataTimeByZoneId;
        }
       
    }
}
