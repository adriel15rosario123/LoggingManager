using System.Reflection;

namespace LoggingManagerCore.Utilities
{
    public static class EnumHandler
    {
        public static string GetValue(Enum @enum)
        {
            FieldInfo field = @enum.GetType().GetField(@enum.ToString())!;
            EnumStringValue attribute = (EnumStringValue)field.GetCustomAttribute(typeof(EnumStringValue))!;
            return attribute != null ? attribute.Value : "";
        }
    }



}
