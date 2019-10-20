using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DVDpro.StarUML.FileFormat
{
    internal class EnumHelper<TEnum>
    {
        private static string GetEnumDisplayName(TEnum enumMember)
        {
            Type genericEnumType = enumMember.GetType();
            var memberInfo = genericEnumType.GetMember(enumMember.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return enumMember.ToString();
        }


        internal static bool TryParse(string value, out TEnum resolved)
        {
            var enumType = typeof(TEnum);
            var names = enumType.GetEnumNames();
            foreach (var name in names)
            {
                var member = (TEnum)Enum.Parse(enumType, name, true);
                var dispName = GetEnumDisplayName(member);
                if (dispName.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    resolved = member;
                    return true;
                }
            }
            
            resolved = default;
            return false;
        }

        internal static string ToString(TEnum enumValue)
        {
            return GetEnumDisplayName(enumValue);
        }
    }
}
