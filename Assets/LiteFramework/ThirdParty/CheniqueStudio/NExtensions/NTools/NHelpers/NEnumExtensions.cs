using System;
using System.ComponentModel;
using System.Reflection;

namespace CheniqueStudio.NExtensions.NTools.NHelpers
{
    public static class NEnumExtensions
    {
        public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = true) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("TEnum must be an enum type");

            return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
        }

        public static string EnumToString<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            return enumValue.ToString();
        }
        
        public static string GetEnumDescription(this Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        
        public static string GetObjectDescription(this FieldInfo field)
        {
            // Get the Description attribute value for the enum value
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return field.Name;
        }
        
        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach(var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                        typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            // Or return default(T);
        }
    }
}