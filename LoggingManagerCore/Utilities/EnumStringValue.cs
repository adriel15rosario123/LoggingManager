namespace LoggingManagerCore.Utilities
{
    public class EnumStringValue : Attribute
    {
        public string Value { get; } = string.Empty;

        public EnumStringValue(string @value)
        {
            Value = @value;
        }
    }
}
